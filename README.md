# Identity Server 4 with SAML support, dockerized.
## This is an example of SSO using SAML and Identity Server 4
### IdentityServer4 SAML2P library from Rock Solid Knowledge

In order to use the library, you need to ask for a DEMO licence, as SAML2p is not free:
https://www.identityserver.com/products/saml2p



1. itg.IdentityProvider is acting as an External Identity Provider. 
   - Identity Server 4 with SAML2p library that corresponds to SAML's Identity Provider
2. mitp.IdentityProvider is acting as a normal Identity Server, that implements SAML SSO.
   - Acts as a Service Provider in SAML terminology. By using SAML2p lib.
3. All the configurations are in Startup and Config classes of the identity servers.

  ### START
  
  - To start the up
  1. Install docker.
  2. Go to the repo (where docker-compose is placed)
  3. run docker-compose up --build
  4. Up will start on http://localhost:5001, http://localhost:5003, http://localhost:7001 and so on.
  5. SPA is on http://localhost:5003
  6. Wait while it starts, creates all the databases, should take about a minute.
  7. Login, creds are specified on a login page.
   
   
   Link:
   https://www.identityserver.com/articles/saml-20-integration-with-identityserver4
