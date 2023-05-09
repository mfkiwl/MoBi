﻿using OSPSuite.Core.Commands.Core;
using MoBi.Core.Domain.Model;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;

namespace MoBi.Core.Commands
{
   public class ChangeParameterValueNameCommand : ChangePathAndValueEntityNameCommand<ParameterValuesBuildingBlock, ParameterValue>
   {
      public ChangeParameterValueNameCommand(ParameterValuesBuildingBlock buildingBlock, ObjectPath path, string newValue) : base(buildingBlock, path, newValue)
      {
      }
      protected override ICommand<IMoBiContext> GetInverseCommand(IMoBiContext context)
      {
         return new ChangeParameterValueNameCommand(_buildingBlock, new ObjectPath(_path), _oldValue).AsInverseFor(this);
      }
   }
}