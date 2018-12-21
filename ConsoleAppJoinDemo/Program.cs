using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJoinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            JoinDemo app = new JoinDemo();

            app.InnerJoin();
            app.GroupJoin();
            app.GroupInnerJoin();
            app.GroupJoin3();
            app.LeftOuterJoin();
            app.LeftOuterJoin2();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
