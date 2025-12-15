namespace Ticket_ManagementAPI.Common
{
    using Duende.IdentityServer.Models;

    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("api.read"),
            new ApiScope("api.write"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            new Client
            {
                ClientId = "angular_client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RedirectUris = { "http://localhost:4200/signin-callback" },
                PostLogoutRedirectUris = { "http://localhost:4200/signout-callback" },
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowedScopes = { "openid", "profile", "api.read" },
                AllowAccessTokensViaBrowser = true
            }
            };
    }
}
