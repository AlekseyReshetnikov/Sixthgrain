using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Grain.Models
{
    public class GrainContext : DbContext
    {
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Agriculture> Agricultures { get; set; }
        public DbSet<Region> Regions { get; set; }

        public GrainContext() : base("GrainContext")
        {
        }

        static GrainContext()
        {
            Database.SetInitializer<GrainContext>(new GrainContextInitializer());
        }

        public void InitData()
        {
            var db = this;
            if (Agricultures.Take(1).Count() == 0)
            {
                Agricultures.AddRange(new Agriculture[] {
                new Agriculture() { Id = 1, Name = "Пшеница" },
                    new Agriculture() { Id = 2, Name = "Кукуруза" },
                    new Agriculture() { Id = 3, Name = "Подсолнух" },
                    new Agriculture() { Id = 4, Name = "Рис" },
                    new Agriculture() { Id = 5, Name = "Овес" },
                    new Agriculture() { Id = 6, Name = "Ячмень" }
                });
                db.SaveChanges();
            }
            if (db.Regions.Take(1).Count() == 0)
            {
                db.Regions.AddRange(new Region[] {
                new Region() { Id = 1, Name = "Московская" },
                    new Region() { Id = 2, Name = "Краснодар" },
                    new Region() { Id = 3, Name = "Турция" },
                    new Region() { Id = 4, Name = "Япония" },
                    new Region() { Id = 5, Name = "Африка" },
                    new Region() { Id = 6, Name = "Пермский край" }
                });
                db.SaveChanges();
            }

            if (db.Farms.Take(1).Count() == 0)
            {
                var r = new Random();
                var Names = "Заря Уралец Солнышко Успешная".Split(' ');
                var Farmers = "Вася Пупкин,Володя,Иван Иваныч,Максим,Пал Палыч".Split(',');
                var AgriculturesCount = Agricultures.Count();
                var RegionsCount = db.Regions.Count();
                for (int i = 0; i < 20; i++)
                {
                    Farm farm = new Farm()
                    {
                        Name = Names[r.Next(Names.Length - 1)],
                        FarmerName = Farmers[r.Next(Farmers.Length - 1)],
                        AgricultureId = r.Next(1, AgriculturesCount),
                        RegionId = r.Next(1, RegionsCount),
                        Area = r.Next(10, 100)
                    };
                    farm.HarvestLastYear = r.Next(10, 20) * farm.Area;
                    db.Farms.Add(farm);
                    db.SaveChanges();
                }
            }

        }

    }

}
