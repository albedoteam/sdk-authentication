namespace AlbedoTeam.Sdk.Authentication
{
    using System;
    using Abstractions;
    using Internals;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Okta.AspNetCore;

    public static class Setup
    {
        private const string AllowSpecificOrigins = "_allowSpecificOrigins";

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            Action<IAuthenticationConfigurator> configure)
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));
            
            services.AddScoped<IAuthenticationConfigurator, AuthenticationConfigurator>();
            var provider = services.BuildServiceProvider();
            var authConfiguration = provider.GetService<IAuthenticationConfigurator>();
            configure.Invoke(authConfiguration);
            
            services
                .AddCors(options => options
                    .AddPolicy(AllowSpecificOrigins, builder => builder
                        .WithOrigins(authConfiguration.Options.AllowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
                })
                .AddOktaWebApi(new OktaWebApiOptions
                {
                    OktaDomain = authConfiguration.Options.AuthServerUrl,
                    AuthorizationServerId = authConfiguration.Options.AuthServerId,
                    Audience = authConfiguration.Options.Audience
                });

            services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseCors(AllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}