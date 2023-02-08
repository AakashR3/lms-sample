using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Helpers
{
    public static class ExtensionHelpers
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
        }
        public static bool IsNullOrEmptyAndWhiteSpace(this string input)
        {
            return string.IsNullOrEmpty(input) && string.IsNullOrWhiteSpace(input);
        }

    }
}
