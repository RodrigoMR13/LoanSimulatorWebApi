using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var keycloakSettings = configuration.GetSection("Keycloak");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://keycloak_server:8081/realms/hackathon";
                    options.Audience = keycloakSettings["Audience"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:8081/realms/hackathon",
                        ValidateAudience = true,
                        ValidAudience = keycloakSettings["Audience"],
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
