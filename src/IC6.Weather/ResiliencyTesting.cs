using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IC6.Weather
{
    public static class ResiliencyTesting
    {
        public static bool ServiceDown = false;

        public static int SecondsAddedOfDelay = 1;
    }
}
