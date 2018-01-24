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
                // db.InitData();
                PivotShow Model = PivotContext.GeneratePivotShowModel(db, 1, 2, 4);
                foreach (var item in Model.Columns)
                {
                    Console.Write("<" + item.Name + ">");
                }
                Console.WriteLine();
                foreach (var row in Model.Rows)
                {
                    Console.Write("<" + row.Name + ">");
                    foreach (var d in row.Data)
                    { Console.Write("<" + d.ToString("#.00") + ">"); }
                    Console.WriteLine();
                }

                foreach (var R in db.Farms)
                {
                    Console.WriteLine(R.Name);
                }
                Console.ReadLine();
            }
        }
    }
}
