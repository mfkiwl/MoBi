using System.Globalization;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;

namespace MoBi.Core.Extensions
{
   public static class PathAndValueEntityExtensions
   {
      public static string GetValueAsDisplayString(this PathAndValueEntity pathAndValueEntity)
      {
         return $"{pathAndValueEntity.ConvertToDisplayUnit(pathAndValueEntity.Value).ToString(CultureInfo.InvariantCulture)} {pathAndValueEntity.DisplayUnit}";
      }   
   }
}