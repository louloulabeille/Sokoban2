using System;
using Sokoban;
namespace TestDeplace
{
    class Program
    {
        static private void Test_OnUpchanged(object sender, EventArgs e)
        {
            
        }

        static void Main(string[] args)
        {
             for (int i= 0; i<10; i++)
            {
                if (i % 2 == 0) continue;
                Console.WriteLine(i);
            }
        }
    }
}
