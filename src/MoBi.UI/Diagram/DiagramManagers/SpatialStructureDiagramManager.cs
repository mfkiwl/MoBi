﻿using System.Collections.Generic;
using System.Linq;
using OSPSuite.Utility.Extensions;
using MoBi.Core.Domain.Model;
using MoBi.Core.Domain.Model.Diagram;
using OSPSuite.Core.Diagram;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Presentation.Diagram;
using OSPSuite.UI.Diagram.Elements;

namespace MoBi.UI.Diagram.DiagramManagers
{
   public class SpatialStructureDiagramManager : BaseDiagramManager<SimpleContainerNode, SimpleNeighborhoodNode, MoBiSpatialStructure>, ISpatialStructureDiagramManager
   {
      // complement and update ViewModel from PkModel and couple ViewModel and PkModel
      protected override void UpdateDiagramModel(MoBiSpatialStructure spatialStructure, IDiagramModel diagramModel, bool coupleAll)
      {
         var unusedNodeIds = new HashSet<string>();
         diagramModel.GetAllChildren<IBaseNode>().Each(x => unusedNodeIds.Add(x.Id));

         if (spatialStructure != null)
         {
            // create neighborhoodsContainerNode, because entities are only added 
            // for available parentContainerNodes
            var neighborhoodsContainerNode = AddAndCoupleNode<IContainer, SimpleContainerNode>(diagramModel, spatialStructure.NeighborhoodsContainer, coupleAll);
            neighborhoodsContainerNode.IsVisible = false;
            neighborhoodsContainerNode.Visible = false; // to avoid visibility in PrintPreview - seems not to be sufficient
            unusedNodeIds.Remove(spatialStructure.NeighborhoodsContainer.Id);

            foreach (var topContainer in spatialStructure.TopContainers)
            {
               if (containerShouldBeDisplayed(topContainer))
               {
                  AddObjectBase(diagramModel, topContainer, recursive: true, coupleAll: coupleAll);
               }
               if(topContainer.ParentPath != null)
                  removeByPathRecursively(topContainer.ParentPath, unusedNodeIds);
               topContainer.GetAllContainersAndSelf<IContainer>().Each(x => unusedNodeIds.Remove(x.Id));
            }

            foreach (var neighborhoodBuilder in spatialStructure.Neighborhoods)
            {
               neighborhoodBuilder.ResolveReference(spatialStructure);
               AddNeighborhood(neighborhoodBuilder);
               unusedNodeIds.Remove(neighborhoodBuilder.Id);
            }
         }

         // remove all unused container and neighborhood nodes
         removeNodesById(unusedNodeIds);

         DiagramModel.ClearUndoStack();
      }

      private void removeByPathRecursively(ObjectPath parentPath, HashSet<string> unusedNodeIds)
      {
         unusedNodeIds.Remove(parentPath);
         if (parentPath.Count <= 1) 
            return;

         var path = parentPath.Clone<ObjectPath>();
         path.RemoveAt(path.Count - 1);
         removeByPathRecursively(path, unusedNodeIds);
      }

      private void removeNodesById(IEnumerable<string> ids) => ids.Each(DiagramModel.RemoveNode);

      private static bool containerShouldBeDisplayed(IContainer topContainer)
      {
         return topContainer.ContainerType == ContainerType.Organism
                || topContainer.ContainerType == ContainerType.Organ
                || topContainer.ContainerType == ContainerType.Compartment;
      }

      // removes all eventHandler which are references to this presenter
      protected override void DecoupleModel()
      {
         foreach (var topContainer in PkModel.TopContainers)
         {
            DecoupleObjectBase(topContainer, recursive: true);
         }

         foreach (var neighborhoodBuilder in PkModel.Neighborhoods)
         {
            DecoupleObjectBase(neighborhoodBuilder, recursive: true);
         }

         Decouple<IContainer, IContainerNode>(PkModel.NeighborhoodsContainer);
      }

      protected override bool MustHandleNew(IObjectBase obj)
      {
         if (obj == null || PkModel == null)
            return false;

         if (obj.IsAnImplementationOf<NeighborhoodBuilder>() && PkModel.Contains(((IEntity)obj).RootContainer))
            return true;

         if (obj.GetType() == typeof(Container) && PkModel.Contains(((IEntity)obj).RootContainer))
            return true;

         return false;
      }

      public override IDiagramManager<MoBiSpatialStructure> Create()
      {
         return new SpatialStructureDiagramManager();
      }

      protected override (string, string) PathsForNeighborhood(INeighborhoodBase neighborhoodBase)
      {
         if (!(neighborhoodBase is NeighborhoodBuilder builder))
         {
            return base.PathsForNeighborhood(neighborhoodBase);
         }

         return (builder.FirstNeighbor == null ? builder.FirstNeighborPath : builder.FirstNeighbor.EntityPath(),
            builder.SecondNeighbor == null ? builder.SecondNeighborPath : builder.SecondNeighbor.EntityPath());

      }

      protected override (IContainerNode firstNeighborContainerNode, IContainerNode secondNeighborContainerNode) GetNeighborHoodNodes(INeighborhoodBase neighborhoodBase)
      {
         var (firstNeighborNode, secondNeighborNode) = base.GetNeighborHoodNodes(neighborhoodBase);

         if (neighborhoodBase is NeighborhoodBuilder neighborhoodBuilder)
         {
            if (firstNeighborNode == null)
               firstNeighborNode = DiagramModel.GetNode<IContainerNode>(neighborhoodBuilder.FirstNeighborPath);
            if (secondNeighborNode == null)
               secondNeighborNode = DiagramModel.GetNode<IContainerNode>(neighborhoodBuilder.SecondNeighborPath);
         }
           
         return (firstNeighborNode, secondNeighborNode);
      }
   }
}
