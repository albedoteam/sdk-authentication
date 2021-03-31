namespace AlbedoTeam.Sdk.Authentication.Abstractions
{
    using System.Collections.Generic;

    public interface IAuthenticationOptions
    {
        string AuthServerUrl { get; set; }
        string AuthServerId { get; set; }
        string Audience { get; set; }
        List<string> AllowedOrigins { get; set; }
    }
}