using Grain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new GrainContext())
            {
                db.InitData();

                foreach (var R in db.Farms)
                {
                    Console.WriteLine(R.Name);
                }
                Console.ReadLine();
            }
        }
    }
}
