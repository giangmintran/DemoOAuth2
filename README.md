# 🔐 ASP.NET Core SSO Demo with IdentityServer

This solution demonstrates how to implement **Single Sign-On (SSO)** using **OAuth2 + OpenID Connect** in ASP.NET Core. It includes:

- ✅ `IdentityProvider`: Issues JWT tokens using Resource Owner Password flow.
- ✅ `ServiceA` & `ServiceB`: Protects API endpoints using Bearer Token Authentication.

---

## 📂 Project Structure

SsoSolution/
│
├── IdentityProvider/ # Duende IdentityServer
├── ServiceA/ # Web API protected by serviceA.scope
└── ServiceB/ # Web API protected by serviceB.scope

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- (Optional) [Postman](https://www.postman.com/) or `curl` for testing

---

### 🛠 Run Projects

Open 3 terminals and run:

```bash
# Terminal 1: Identity Provider
dotnet run --project IdentityProvider

# Terminal 2: Service A
dotnet run --project ServiceA

# Terminal 3: Service B
dotnet run --project ServiceB

How Authentication Works
Client sends username/password to IdentityProvider (Resource Owner Password Flow).

IdentityProvider issues an access_token (JWT) with defined scopes.

Client uses that token to access protected APIs in Service A and B.

Each service validates the token and checks its scope.

🧪 Test Flow
1. Request Token
bash
Copy
Edit
curl -X POST https://localhost:5001/connect/token \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "client_id=client" \
  -d "client_secret=secret" \
  -d "grant_type=password" \
  -d "username=test" \
  -d "password=password" \
  -d "scope=openid profile serviceA.scope serviceB.scope"
This returns a JSON with access_token, id_token, expires_in, etc.

2. Call Service A
bash
Copy
Edit
curl -H "Authorization: Bearer {access_token}" https://localhost:5002/secure-data
3. Call Service B
bash
Copy
Edit
curl -H "Authorization: Bearer {access_token}" https://localhost:5003/secure-data
👤 Test User
Username	Password
test	password

📄 Technologies Used
ASP.NET Core 7

Duende IdentityServer (for local development)

OAuth2 + OpenID Connect

JWT Bearer Authentication

📌 Notes
This is a simplified setup for local SSO testing.

For production, consider:

Using Authorization Code Flow with PKCE (for SPA/mobile apps)

Securing services with HTTPS, reverse proxy, and rate limiting

Adding refresh tokens, token revocation, logout flows

Integrating real user management (e.g., ASP.NET Core Identity, database)
