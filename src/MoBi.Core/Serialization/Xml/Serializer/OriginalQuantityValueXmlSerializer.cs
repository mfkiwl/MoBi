﻿using System.Xml.Linq;
using MoBi.Core.Domain;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Serialization.Xml;
using OSPSuite.Core.Serialization.Xml.Extensions;

namespace MoBi.Core.Serialization.Xml.Serializer
{
   public class OriginalQuantityValueXmlSerializer : OSPSuiteXmlSerializer<OriginalQuantityValue>
   {
      public override void PerformMapping()
      {
         Map(x => x.Path);
         Map(x => x.Value);
         Map(x => x.ValueOrigin);
         Map(x => x.Dimension);
         Map(x => x.Type);
      }

      protected override void TypedDeserialize(OriginalQuantityValue originalQuantityValue, XElement element, SerializationContext serializationContext)
      {
         base.TypedDeserialize(originalQuantityValue, element, serializationContext);

         if (originalQuantityValue.Dimension == null)
            originalQuantityValue.Dimension = Constants.Dimension.NO_DIMENSION;

         element.UpdateDisplayUnit(originalQuantityValue);
      }

      protected override XElement TypedSerialize(OriginalQuantityValue originalQuantityValue, SerializationContext serializationContext)
      {
         var element = base.TypedSerialize(originalQuantityValue, serializationContext);
         return element.AddDisplayUnitFor(originalQuantityValue);
      }
   }
}