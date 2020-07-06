# Identity Server 4 with SAML support, dockerized.

## This is an example of SSO using SAML and Identity Server 4

### IdentityServer4 SAML2P library from Rock Solid Knowledge, dotnet core 3.1 is used, Alpine images for docker containers.

In order to use the library, you need to ask for a DEMO licence, as SAML2p is not free:
https://www.identityserver.com/products/saml2p



1. itg.IdentityProvider is acting as an External Identity Provider. 
   - Identity Server 4 with SAML2p library that corresponds to SAML's Identity Provider
2. mitp.IdentityProvider is acting as a normal Identity Server, that implements SAML SSO.
   - Acts as a Service Provider in SAML terminology. By using SAML2p lib.
   - itg is added as an External Identity Provider, which returns proper SAML responses to mitp identity server.
3. All the configurations are in Startup and Config classes of the identity servers.
4. SPA application is used, that implements OAuth 2.0 Auth Code flow + PKCE, is a simple Javascript application, that utilizes oidc-client.js

### Versions:
1. dotnet core 3.1
2. Alpine 3.12
3. MSSQL in docker 2019
4. Identity Server 4.
5. docker-compose 3.7

  ### SETUP
  
  1. Install docker.
  2. Go to the repo (where docker-compose is placed)
  3. run docker-compose up --build
  4. Up will start on http://localhost:5001, http://localhost:5003, http://localhost:7001 and so on.
  5. SPA is on http://localhost:5003
  6. Wait while it starts, creates all the databases, should take about a minute.
  7. Login, creds are specified on a login page.
   
   
   Links:
   1. SAML2P docs: https://www.identityserver.com/articles/saml-20-integration-with-identityserver4
   2. Quickstarts: https://github.com/IdentityServer/IdentityServer4/tree/main/samples/Quickstarts
