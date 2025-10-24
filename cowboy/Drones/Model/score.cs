using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CowBoy
{


    public partial class Score
    {
        private static int _score = 0;

        public int score { get => _score; set => _score = value; }

        public static void add()
        {
            _score++;
            Console.WriteLine($"Score incrémenté : {_score}");
        }
    }


}
