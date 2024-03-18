using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellDesignedAPI.Common.Helpers
{
    public static class DateTimeHelper
    {
      
        public static bool IsValidDateTimeString(string dateTimeString)
        {
            return DateTime.TryParse(dateTimeString, out _);
        }
        
    }
}
