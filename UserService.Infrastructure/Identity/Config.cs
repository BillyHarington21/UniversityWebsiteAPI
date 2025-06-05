using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Infrastructure.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId(),   // включает sub (user id)
            new IdentityResources.Profile(),  // включает name, family_name, given_name
            new IdentityResource("roles", new[] { JwtClaimTypes.Role }) // добавим роли
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
            new ApiScope("userservice.api", "User Service API")
            {
                UserClaims = { JwtClaimTypes.Role, JwtClaimTypes.Email, JwtClaimTypes.Name }
            }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
            new ApiResource("userservice", "User Service Resource")
            {
                Scopes = { "userservice.api" },
                UserClaims = { JwtClaimTypes.Role, JwtClaimTypes.Email, JwtClaimTypes.Name }
            }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
            new Client
            {
                ClientId = "frontend-client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedScopes = {
                    "openid",
                    "profile",
                    "roles",
                    "userservice.api"
                },

                AccessTokenLifetime = 3600, // токен живёт 1 час
                AllowOfflineAccess = true  // позволяет получать refresh token
            }
            };
    }
}
