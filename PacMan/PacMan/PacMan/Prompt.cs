using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    class TimeConstant
    {
        public static int num, t_FMC, t_response, t_onscene, t_DTN_CSC, t_DTN_PSC, t_NTDO, t_PSC_CSC, t_DTP_MS, t_DTP_DnS;

        public static void Prompt()
        {
            Console.WriteLine("Please select your screening method by typing the number beside the method: LAMS [1], RACE [2], CSTAT [3]");
            Int32.TryParse(Console.ReadLine(), out num);

            Console.WriteLine("Please enter FMC time");
            Int32.TryParse(Console.ReadLine(), out t_FMC);

            Console.WriteLine("Please enter response time");
            Int32.TryParse(Console.ReadLine(), out t_response);

            Console.WriteLine("Please enter onscene time");
            Int32.TryParse(Console.ReadLine(), out t_onscene);

            Console.WriteLine("Please enter DTN_CSC time");
            Int32.TryParse(Console.ReadLine(), out t_DTN_CSC);

            Console.WriteLine("Please enter DTN_PSC time");
            Int32.TryParse(Console.ReadLine(), out t_DTN_PSC);

            Console.WriteLine("Please enter NTDO time");
            Int32.TryParse(Console.ReadLine(), out t_NTDO);

            Console.WriteLine("Please enter PSC-CSC time");
            Int32.TryParse(Console.ReadLine(), out t_PSC_CSC);

            Console.WriteLine("Please enter door to puncture MS time");
            Int32.TryParse(Console.ReadLine(), out t_DTP_MS);

            Console.WriteLine("Please enter door to puncture DnS time");
            Int32.TryParse(Console.ReadLine(), out t_DTP_DnS);
        }
    }
}
