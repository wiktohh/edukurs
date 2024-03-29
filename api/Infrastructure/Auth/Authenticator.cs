using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTO;
using Application.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

public class Authenticator : IAuthenticator
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _signingKey;
    private readonly TimeSpan? _expiry;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new JwtSecurityTokenHandler();
    
    
    public Authenticator(IOptions<AuthOptions> options)
    {
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _signingKey = options.Value.SigningKey;
        _expiry = options.Value.Expiry;
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_signingKey)),
            SecurityAlgorithms.HmacSha256);
    }
    
    
    public JwtDto CreateToken(Guid userId,string role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, role)
        };
        
        var expires = DateTime.UtcNow.Add(_expiry ?? TimeSpan.FromHours(1));
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, DateTime.UtcNow, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);
        
        return new JwtDto
        {
            AccessToken = token
        };
    }
}