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
                    options.Authority = keycloakSettings["Authority"];
                    options.Audience = keycloakSettings["Audience"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
