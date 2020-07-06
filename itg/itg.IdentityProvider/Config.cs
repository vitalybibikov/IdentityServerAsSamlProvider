// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.Saml;
using IdentityServer4.Saml.Models;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                
                    // where to redirect to after login
                    RedirectUris = { "http://host.docker.internal:7002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:7002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AllowOfflineAccess = true
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:7003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:7003/index.html" },
                    //AllowedCorsOrigins =     { "http://host.docker.internal:7003", "http://localhost:5001" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },

                new Client {
                    ClientId = "http://localhost:5001/saml",
                    ClientName = "mitp Identity Server",
                    ProtocolType = IdentityServerConstants.ProtocolTypes.Saml2p,
                    AllowedScopes = { "openid", "profile", "api1" }
                }
            };


        public static IEnumerable<ServiceProvider> GetServiceProviders()
        {
            return new[]
            {
                new ServiceProvider
                {
                    EntityId = "http://localhost:5001/saml",
                    SigningCertificates = { GetOwnCert() },
                    AssertionConsumerServices =
                        { new Service(SamlConstants.BindingTypes.HttpPost, "http://localhost:5001/signin-saml") }
                }
            };
        }

        private static X509Certificate2 GetOwnCert()
        {
            var key =
                "MIIKtwIBAzCCCmMGCSqGSIb3DQEHAaCCClQEggpQMIIKTDCCBlIGCSqGSIb3DQEHAaCCBkMEggY" +
                "/MIIGOzCCBjcGCyqGSIb3DQEMCgECoIIFQTCCBT0wVwYJKoZIhvcNAQUNMEowKQYJ" +
                "KoZIhvcNAQUMMBwECH5+R67KPxGYAgIH0DAMBggqhkiG9w0CCQUAMB0GCWCGSAFlAwQBKg" +
                "QQ4zmlnIPC2cAEmO7bmOuNHASCBOCIZ1ZNorF3x58nKnTu4/XHNNBYYE4fU44WsKlJybToRfd" +
                "Rl2290vNIAHN2b8yGIuDgtzdHtS/iUk42biYZpYxyLxxPy9GEGElYwJDqMzzcCDbYKEPyCGvWo0Xo" +
                "bw7OiGaB7z1+WSwFetZU6otDEbZ+oKwX69v7pT2FTFzZQd+j6JaBRHZ5Gecv6DYg3PavvGi/MO9g" +
                "f4PXnogSM4YYM1x4qcmRhM1S1+Xn30yZ3tgMwyKBZE1GtB8h2g8OkmKgFlYi6NZ59WbaV1R+jL/D" +
                "BtxJZe7zLwn7tp1Xuy2QYT4ClO43gFlpw0YeTvKuNTWddZ7k1qtl70zF6kbnToG2GDc+r0nJM8BjHo" +
                "Db8ajad4gUI6LRE8IXYLalYpa+0uGx6o1ybE4aSaMVhxVFN/kilik3/0ni7DaTwnt2bPYGC8w9wm0GT" +
                "+2vWuogeuFiL7M5d/92qW5pUtr/qpKan/Qd17ttMTqvibbZrjOdeCBA66WwTacJM0DM3ByjuJyzOF+Z" +
                "UPTArKEBQf2fVoZjT/E0kihrkt4x9N60Ero30elJPTpHodx2M6AYIuF7CpYu/4kmgvgfnJMvndN0VUL" +
                "Wv/DMlDHA8066oppxretgLNxeWgbUVKVxN7JIxhQkHAEEUHEAPzQi8B99NWChKBa7lOz+9I7RCXuHIIx" +
                "b4O/FdFWzHKnONS1AIb56taLeUy6nQFryljkulawG091k9d0DutODBQrTWg2Zp3r97P5jmdsK99+zyiX" +
                "LodzVYuI8gc30KNY8y78Rf3sv8Hp/Aj4WwYTxww61bhKOozQIf05S43HgZL2FIRrIQpMVnavVyJUeTUbq" +
                "0VYFbaoxJUTtm/3JwPDDnlEIv4a7v/wJjWAACwnsJany+KJrL3BXSmde/oD5po/d/prM3VMw+k/NzL0Ur" +
                "I0Bpl03W/I9Xej/lAic2Wf1i2dF4ziD0cooNMRBhozQaHw5/+3+qlu2ZCdE08I53RYMKhT2pPsn7gTiX5oa" +
                "HId1PAXCkUvclpKFFfRkcrf4qwsM3kS7jyF1do6KxE6o8yG3++GCgmWtqKMNooBTyzTRBmwCgcNjMU30" +
                "MafIL3GBynkzAAt1OYIjuxLL2WKS4Eku/MtgykSuFSOwerx2y83u7jpByHxb4SMIVpwFEHBVN7P65PsWhdw" +
                "b1rbWXIGulPACiq5gjDxOfM12HJKuK8y+aRxa6bCJuq68vogvxZb5/G5atWTmGUzcdcZ2hWghMsSCNPulM734" +
                "VD3+pPoOTAKMUxBhjNQZbvm/Z5ShJ6Rx/K56N0UXGn2TvnCtcrDk120aqPjg6HndkP2FHpBqQqDOX1XdHtRJ4z" +
                "ClYdReSz54F6jGcXaNO/r7vbUKYvD6yJfBK7kAFsch9VkUbL/UjVgGhu/FxpQSn1H71qcE+t5w2VWRvwrMSEPch" +
                "ZIJRG2jF/ZjOxW+0BwJPp30E7rlwKAUAzTRGZUrDKiL8EBeQxyPoFZKJRkgw9b9oYjjdlNlYWczNpsMN037j6/A" +
                "FLHmAtFyloCpZAsIbh8DwXU47G1ntWVt7DZhTJvCNg/nsz/0FtXgA1JiCOpXMjyBJD0SZdX6sDDpZ0dId5LwS3NVqDO9p" +
                "4aJJ+GW+CuCkBoQrIIjRAc/rbpDyC/8YOZpt+K/u9kz3siiUyK5lv4XgPb4usEK4XJwwUMxgeIwDQYJKwYBBAGCNxECMQAwEw" +
                "YJKoZIhvcNAQkVMQYEBAEAAAAwXQYJKoZIhvcNAQkUMVAeTgB0AGUALQA3AGUAZQA3AGUAOAA5ADgALQA2AGYAMwA0AC0ANAAyA" +
                "DAAOQAtADgAZAA4ADcALQBiADEAYQBiAGQAYwA2ADUAYQAwADEAMzBdBgkrBgEEAYI3EQExUB5OAE0AaQBjAHIAbwBzAG8AZgB" +
                "0ACAAUwBvAGYAdAB3AGEAcgBlACAASwBlAHkAIABTAHQAbwByAGEAZwBlACAAUAByAG8AdgBpAGQAZQByMIID8gYJKoZIhvcNAQc" +
                "GoIID4zCCA98CAQAwggPYBgkqhkiG9w0BBwEwVwYJKoZIhvcNAQUNMEowKQYJKoZIhvcNAQUMMBwECJyb/dgY2NvQAgIH0DAMBggq" +
                "hkiG9w0CCQUAMB0GCWCGSAFlAwQBKgQQQYby/713bcL9FAHshLdMz4CCA3AAuQAPd3aL+c9A1zzAyUAe4dZWHFZjYuEDdmcDUGpyCDR" +
                "rP7DW8Q6d1/1l/cenwD6BQqWhLCgmoQo2HdtiLuQ3HjJeXlA/rlJpXuGaKzEZrKIV0ZtGXKyw7ZiC3IHDGA9rrGvnngsMoKB6NuAaa" +
                "/LFpcu40y9zM1ZjwN6QHDv5fFHZ9ddINqj6dHAie/qeQNfzugGRbYJfelq5aZc33wFRQscLIVJGFNneZziU+abOGsG6V3F1pTinRBqV" +
                "uluHpwd6AKZNe07CYPZMtrKl6G5sOwfo1ZTrhNPzc19NlZ42adqRFWHmiktRd46W7wNgRHPDEJ6n/t7g4f2C0VWcl0pYGNYuurNxcHNi" +
                "6wf8RC5310C0sFB+U/xmpGYdkfL9F0ruq6MixsNuGAsuXcuMuEIozQZgiXSe+AopshANn1kAZlHb3r695uDnfYmbKxu6qa/94N/25KPq" +
                "E4p5kFCa2nJZskiop9dTx9lIDFBPFjqD8QSnstJCHFDIopqgOKnvlawXSF7zlJM/bQQFnRB/C7b1s5Zdh6qtDc5kLTLt2QlVaAc7Pwo72" +
                "CCSc178ddB1rDFRm5+KmewA3U4VMouDyrtKXq6S3PYWIzRKFLKM56MqxlOP/R4oouqeL7Hwb1nRx7q7+N9gaY2FJ9rj2jqEoLlYafSTL" +
                "qN8pMzJjZSIvjDTiLTLDEf/LjtCHg+M0DSEepxxB5ZIameiLpBjA3qQMlmrfkPWhKPM1l6uht2+FNlKR3jlPMzUiKVgbqgME+Mhwz3TT" +
                "y+uZi43QsPQ3iZ72VlU0NXblgJ0aq8gqXmqbkUfMwttCNvlCnbDBphEDJjnvpzRxRNCCFKTADpX8ew36qmQpnxEzvnmSXt76Us2PK/RD" +
                "QofR9e6zSIZtRRBWWheLHvmfnHQXReqvWQpsQjO/p+puAnlCkiHlk41cR7d1RzJfnEob+kjMGe/w31E+EUZ+O/Wj/OPI01zqU0G+crHqF" +
                "p/aAgK3HFTd/Ofb5xq3NHb2/aV2oOU4kXCJhez6pVlxIjdqlJsHPO/Wd/dPKygKoh3p7BGEFOKJZmEtucIXTwh2qC9rhwbJOb1kLNLwvE" +
                "lnMRy3jYsA5/z3zn9tvazGlM4o1zZEWvG9AALxcvbNBkIIfT324YWH6ZPNJIAOflDzSLLr9c+2uYhITIxpcPBvDhVMEswLzALBglghkgBZQMEAgE" +
                "EIFhvNuM9kXvg1GapUUdeNHDIBJHfMi5TApdfTpToMEZYBBRUOxH5FNWe62XJ+6d7rCUKxvxKBQICB9A=";

            var pfxBytes = Convert.FromBase64String(key);
            X509Certificate2 cert = new X509Certificate2(pfxBytes, "Passw0rd", X509KeyStorageFlags.MachineKeySet);
            return cert;
        }
    }
}