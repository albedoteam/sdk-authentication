namespace AlbedoTeam.Sdk.Authentication.Internals
{
    using System;
    using System.Linq;
    using Abstractions;

    internal class AuthenticationConfigurator : IAuthenticationConfigurator
    {
        public IAuthenticationOptions Options { get; private set; }
        
        public IAuthenticationConfigurator SetOptions(Action<IAuthenticationOptions> configureOptions)
        {
            IAuthenticationOptions options = new AuthenticationOptions();
            configureOptions.Invoke(options);

            if (string.IsNullOrWhiteSpace(options.AuthServerUrl))
                throw new InvalidOperationException("Can not setup the authentication without a valid AuthServer URL");
            
            if (string.IsNullOrWhiteSpace(options.AuthServerId))
                throw new InvalidOperationException("Can not setup the authentication without a valid AuthServer ID");
            
            if (string.IsNullOrWhiteSpace(options.Audience))
                throw new InvalidOperationException("Can not setup the authentication without a valid Audience");
            
            if (options.AllowedOrigins is null || !options.AllowedOrigins.Any())
                throw new InvalidOperationException("Can not setup the authentication without allowed origins");

            Options = options;
            return this;
        }
    }
}