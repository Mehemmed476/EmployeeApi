using EmployeeApp.Core.Entities.Base;
using EmployeeApp.DAL.Contexts;
using EmployeeApp.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.DAL.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var exitingEntity = await GetByIdAsync(entity.Id);
        _context.Entry(exitingEntity).CurrentValues.SetValues(entity);
    }

    public void Delete(TEntity entity)
    {
        Table.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExist(int id)
    {
        var isExist = await Table.AnyAsync(x => x.Id == id);
        return isExist;
    }

    public void SoftDelete(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public void Restore(TEntity entity)
    {
        entity.IsDeleted = false;
    }
}