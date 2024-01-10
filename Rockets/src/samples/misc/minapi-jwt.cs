using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder (args);

// Add JWT configuration
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// A method to generate a JWT token
static string GenerateToken(string userName, string issuer, string audience, string key)
{
    // Create a list of claims for the user
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, userName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    // Create a security key from the key string
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

    // Create a signing credentials using the security key and algorithm
    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    // Create a JWT token descriptor with the claims, issuer, audience, and signing credentials
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddMinutes(15),
        Issuer = issuer,
        Audience = audience,
        SigningCredentials = signingCredentials
    };

    // Create a JWT token handler
    var tokenHandler = new JwtSecurityTokenHandler();

    // Create a JWT token from the descriptor
    var token = tokenHandler.CreateToken(tokenDescriptor);

    // Return the token as a string
    return tokenHandler.WriteToken(token);
}

// A method to read a JWT token
static string ReadToken(string token, string issuer, string audience, string key)
{
    // Create a security key from the key string
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

    // Create a token validation parameters with the key, issuer, and audience
    var tokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = securityKey,
        ValidIssuer = issuer,
        ValidAudience = audience
    };

    // Create a JWT token handler
    var tokenHandler = new JwtSecurityTokenHandler();

    // Validate the token and get the principal
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

    // Return the principal as a JSON string
    return JsonSerializer.Serialize(principal);
}

// A route to create a JWT token
app.MapPost("/create", (HttpContext context) =>
{
    // Get the user name from the request body
    var userName = context.Request.ReadFromJsonAsync<string>().Result;

    // Get the issuer, audience, and key from the configuration
    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];
    var key = builder.Configuration["Jwt:Key"];

    // Generate a token for the user name
    var token = GenerateToken(userName, issuer, audience, key);

    // Return the token as a plain text response
    return Results.Text(token);
});

// A route to read a JWT token
app.MapPost("/read", (HttpContext context) =>
{
    // Get the token from the request body
    var token = context.Request.ReadFromJsonAsync<string>().Result;

    // Get the issuer, audience, and key from the configuration
    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];
    var key = builder.Configuration["Jwt:Key"];

    // Read the token and get the principal
    var principal = ReadToken(token, issuer, audience, key);

    // Return the principal as a JSON response
    return Results.Json(principal);
});

app.Run();
