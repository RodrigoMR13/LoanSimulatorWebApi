using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class KeycloakConfig
    {
        public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var keycloakSettings = configuration.GetSection("Keycloak");

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = keycloakSettings["Audience"];
                    options.MetadataAddress = keycloakSettings["MetadataAddress"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = keycloakSettings["ValidIssuer"],
                    };
                });


            return services;
        }
    }
}
