﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    public interface IGrainRepository
    {
        IDbSet<Farm> Farms { get; }
        IDbSet<Agriculture> Agricultures { get; }
        IDbSet<Region> Regions { get; }
        int SaveChanges();
        void SetEntryEntityState<TEntity>(TEntity entity, EntityState State) where TEntity : class;
        Task SaveFarmAsync(Farm farm);
        Task<Farm> FarmsFindAsync(int? id);
        Task<List<Farm>> FarmsList();
    }
}