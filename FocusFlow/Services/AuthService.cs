using FocusFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FocusFlow.Services;

public class AuthService
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;
    public AppUser? CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;
    private readonly ProtectedLocalStorage _localStorage;
    public event Action? OnChange;

    public AuthService(IDbContextFactory<AppDbContext> dbFactory,
                       ProtectedLocalStorage localStorage)
    {
        _dbFactory = dbFactory;
        _localStorage = localStorage;
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }

    public async Task InitializeAsync()
    {
        try
        {
            var stored = await _localStorage.GetAsync<int?>("userId");
            if (stored.Success && stored.Value is int id)
            {
                using var db = await _dbFactory.CreateDbContextAsync();
                CurrentUser = await db.Users.FindAsync(id);
                NotifyStateChanged();
            }
        }
        catch (Exception e)
        {

        }
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        using var db = await _dbFactory.CreateDbContextAsync();

        var user = await db.Users.FirstOrDefaultAsync(u =>
            u.Username == username &&
            u.Password == password);

        if (user == null)
        {
            return false;
        }

        await _localStorage.SetAsync("userId", user.Id);
        await _localStorage.SetAsync("userName", user.Name);
        await _localStorage.SetAsync("username", user.Username);

        CurrentUser = user;

        return true;
    }

    public async Task Logout()
    {
        CurrentUser = null;
        
        await _localStorage.DeleteAsync("userId");
        await _localStorage.DeleteAsync("userName");
        await _localStorage.DeleteAsync("username");

        NotifyStateChanged();
    }

    public async Task<bool> RegisterAsync(
        string name,
        string username,
        string password)
    {
        using var db = await _dbFactory.CreateDbContextAsync();

        bool exists =
            await db.Users.AnyAsync(u =>
                u.Username == username);

        if (exists)
        {
            return false;
        }

        db.Users.Add(new AppUser
        {
            Name = name,
            Username = username,
            Password = password
        });

        await db.SaveChangesAsync();

        return true;
    }
}