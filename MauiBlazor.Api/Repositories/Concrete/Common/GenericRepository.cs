using System;
using MauiBlazor.Api.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq.Expressions;
using MauiBlazor.Api.Repositories.Abstract.Common;
using MauiBlazor.Shared.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MauiBlazor.Api.Repositories.Concrete.Common;

public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : BaseModel
{
    private readonly MyDbContext _context;
    public DbSet<TSource> Table => _context.Set<TSource>();

    public GenericRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<TSource>?> GetAllAsync() => await Table.AsNoTracking().ToListAsync();

    public async Task<List<TSource>?> GetAllAsync(Expression<Func<TSource, bool>> filter) =>
        await Table.AsNoTracking().Where(filter).ToListAsync();

    public async Task<int> GetCountAsync() => await Table.CountAsync();

    public async Task<int> GetCountAsync(Expression<Func<TSource, bool>> filter) =>
        await Table.CountAsync(filter);

    public async Task<TSource?> GetSingleAsync(Expression<Func<TSource, bool>> filter) =>
        await Table.FirstOrDefaultAsync(filter);

    public async Task<bool> InsertAsync(TSource item)
    {
        try
        {
            EntityEntry<TSource> entityEntry = await Table.AddAsync(item);
            await SaveAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveAsync(int id)
    {
        try
        {
            var model = await Table.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                Table.Remove(model);
                Save();
                return true;
            }
            else
            {
                return false; // Or handle the case where the item is not found
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveRangeAsync(List<int> ids)
    {
        try
        {
            var models = await Table.AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync();
            Table.RemoveRange(models);
            Save();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public int Save() => _context.SaveChanges();

    public async Task<bool> UpdateAsync(TSource item)
    {
        try
        {
            EntityEntry<TSource> entityEntry = Table.Update(item);
            await SaveAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    Task<int> IGenericRepository<TSource>.Save()
    {
        throw new NotImplementedException();
    }
}
