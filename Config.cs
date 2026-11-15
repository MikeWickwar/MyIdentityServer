using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace MyIdentityServer
{
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
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client_credentials_client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim("custom_claim_from_config", "custom_value")
                    },
                    RedirectUris = { "https://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "api1" }
                },
                new Client
                {
                    ClientId = "ghostdot-bff",
                    ClientName = "GhostDot local BFF",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = true,
                    ClientSecrets =
                    {
                        new Secret(GetRequiredGhostDotBffClientSecret().Sha256())
                    },
                    RedirectUris = { "https://localhost:7240/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7240/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "api1" }
                }
            };

        private static string GetRequiredGhostDotBffClientSecret()
        {
            string? secret = Environment.GetEnvironmentVariable("GHOSTDOT_BFF_CLIENT_SECRET");
            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException("Set GHOSTDOT_BFF_CLIENT_SECRET before starting the local IdentityServer.");
            }

            return secret;
        }
    }
}
