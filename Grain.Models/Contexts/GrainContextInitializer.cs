using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    class GrainContextInitializer : DropCreateDatabaseIfModelChanges<GrainContext>
    {
        protected override void Seed(GrainContext db)
        {
            db.InitData();
        }

    }
}
