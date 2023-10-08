using IdentityServer4.Models;

namespace Shelfie.Identity.DataAccess;

public class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
              Name = "role",
              UserClaims = new List<string> {"role"}
            }
        };
    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("shelfie-api.read"),
            new ApiScope("shelfie-api.write"),
        };

    public static IEnumerable<ApiResource> ApiResources => 
        new[]
        {
            new ApiResource("shelfie-api")
            {
                Scopes = new List<string> { "shelfie-api.read", "shelfie-api.write"},
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedScopes = { "shelfie-api.read", "shelfie-api.write" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:5444/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},

                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile", "shelfie-api.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
        };
}
