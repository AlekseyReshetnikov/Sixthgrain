using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    public class GrainRepository : IGrainRepository, IDisposable
    {
        private GrainContext Db = new GrainContext();

        public IDbSet<Farm> Farms => Db.Farms;

        public IDbSet<Agriculture> Agricultures => Db.Agricultures;

        public IDbSet<Region> Regions => Db.Regions;

        public IEnumerable<DataField> PivotHeaderFields => PivotContext.HeaderFields;

        public IEnumerable<DataField> PivotDataFields => PivotContext.DataFields;

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<Farm> FarmsFindAsync(int? id)
        {
            return await Db.Farms.FindAsync(id);
        }

        public async Task<List<Farm>> FarmsList()
        {
            var farms = Db.Farms.Include(f => f.Agriculture).Include(f => f.Region);
            var ret = await farms.ToListAsync();
            return ret;
        }

        public PivotShow GeneratePivotShowModel(int colId, int rowId, int dataId)
        {
            PivotContext c = new PivotContext();
            return c.GeneratePivotShowModel(Db, colId, rowId, dataId);
        }

        public int SaveChanges() { return Db.SaveChanges(); }

        public async Task SaveFarmAsync(Farm farm)
        {
            Db.Farms.Add(farm);
            await Db.SaveChangesAsync();
        }

        public void SetEntryEntityState<TEntity>(TEntity entity, EntityState State) where TEntity : class
        {
            Db.Entry(entity).State = State;
        }
    }
}
