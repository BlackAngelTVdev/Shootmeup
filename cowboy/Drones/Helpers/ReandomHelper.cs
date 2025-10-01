using System;

namespace Drones.Helpers
{
    internal class RandomHelper
    {
        public static int NbrRandom(int nbrMin, int nbrMax, bool borne)
        {
            Random r = new Random();

            int middleMin = nbrMin + (nbrMax - nbrMin) / 4;
            int middleMax = nbrMax - (nbrMax - nbrMin) / 4;


            if (r.NextDouble() < 0.8 && borne)
            {
                return r.Next(middleMin, middleMax + 1);
            }


            return r.Next(nbrMin, nbrMax + 1);
        }

    }
}
