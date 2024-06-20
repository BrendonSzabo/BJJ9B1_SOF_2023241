using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class ModelLogic<T> where T : class, IModelBase
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ModelLogic(ApplicationDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AddItem(T newItem)
    {
        _db.Set<T>().Add(newItem);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveItem(int id)
    {
        var item = await _db.Set<T>().FindAsync(id);
        if (item == null)
            return false;

        _db.Set<T>().Remove(item);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateItem(T updatedItem)
    {
        _db.Entry(updatedItem).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<T> GetItem(int id)
    {
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetItems()
    {
        return await _db.Set<T>().ToListAsync();
    }

    public async Task<User> GetUser(string id)
    {
        return await _db.Set<User>().FindAsync(id);
    }
}