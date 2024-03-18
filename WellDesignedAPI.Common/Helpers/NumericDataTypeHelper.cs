using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellDesignedAPI.Common.Helpers
{
    public static class NumericDataTypeHelper
    {
        public static bool StringCanBeParsedToDecimal(string input)
        {
            return decimal.TryParse(input, out _);
        }

        public static bool StringCanBeParsedToInteger(string input)
        {
            return int.TryParse(input, out _);
        }
    }
}
