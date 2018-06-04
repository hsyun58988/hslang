using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hslang
{
    /// <summary>
    /// hslang#0003解释器
    /// c#版，以后都将是c#版
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string command = Console.ReadLine();
                List<string> code = parser.Parser(command);
                Console.WriteLine(string.Join(",", code.ToArray()));
                script pro = new script();
                run.Run(code, pro, cfg.RuntimeMode.command);
            }
        }
    }
}
