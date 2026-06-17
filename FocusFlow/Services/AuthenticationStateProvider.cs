using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using FocusFlow.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _auth;

    public CustomAuthStateProvider(AuthService auth)
    {
        _auth = auth;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_auth.IsLoggedIn && _auth.CurrentUser != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, _auth.CurrentUser.Name ?? ""),
                new Claim(ClaimTypes.NameIdentifier, _auth.CurrentUser.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
    }

    public void NotifyAuthChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}