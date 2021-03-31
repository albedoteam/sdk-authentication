namespace AlbedoTeam.Sdk.Authentication.Internals
{
    using System.Collections.Generic;
    using Abstractions;

    internal class AuthenticationOptions: IAuthenticationOptions
    {
        public string AuthServerUrl { get; set; }
        public string AuthServerId { get; set; }
        public string Audience { get; set; }
        
        public List<string> AllowedOrigins { get; set; }
    }
}