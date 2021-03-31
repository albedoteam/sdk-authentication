namespace AlbedoTeam.Sdk.Authentication.Abstractions
{
    using System;

    public interface IAuthenticationConfigurator
    {
        IAuthenticationOptions Options { get; }
        IAuthenticationConfigurator SetOptions(Action<IAuthenticationOptions> configureOptions);
    }
}