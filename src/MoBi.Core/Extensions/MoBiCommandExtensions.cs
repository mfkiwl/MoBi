﻿using OSPSuite.Core.Commands.Core;

namespace MoBi.Core.Extensions
{
   public static class MoBiCommandExtensions
   {
      public static T RunCommand<T, TExecutionContext>(this T command, TExecutionContext context) where T : ICommand<TExecutionContext>
      {
         command.Run(context);
         return command;
      }
   }
}
