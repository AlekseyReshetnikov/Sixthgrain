using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    public interface IGrainRepository
    {
        IEnumerable<Farm> Farms { get; }
        IEnumerable<Agriculture> Agricultures { get; }
        IEnumerable<Region> Regions { get; }
        IEnumerable<DataField> PivotHeaderFields { get;}
        IEnumerable<DataField> PivotDataFields { get; }

        int SaveChanges();
        void SetEntryEntityState<TEntity>(TEntity entity, EntityState State) where TEntity : class;
        Task SaveFarmAsync(Farm farm);
        Task<Farm> FarmsFindAsync(int? id);
        Task<List<Farm>> FarmsList();
        Task<PivotView> GeneratePivotShowModel(int colId, int rowId, int dataId);
        Task FarmRemoveAsync(int id);
        Task<Farm> FarmsFind(int? id);
    }
}
