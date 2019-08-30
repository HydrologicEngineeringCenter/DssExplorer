using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dss_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            CMD c = new CMD();
            if(args.Length ==1)
            {
                c = new CMD(args[0]);
            }
            c.Run();
        }
    }
}
