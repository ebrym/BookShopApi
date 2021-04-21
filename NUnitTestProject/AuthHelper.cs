
using BookShop.Repository.Request.User;
using BookShop.Repository.Response.User;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XUnitIntegrationTests.Models;
using XUnitIntegrationTests.WebFactory;

namespace XUnitIntegrationTests.AuthenticationHelper
{
    public class AuthHelper
    {
        private readonly HttpClient client;

        public AuthHelper(HttpClient client)
        {
            this.client = client;
        }

        public async Task<CreateUserRequest> CreateTestUser(CreateUserRequest newUser,
        WebFactory<IntegrationTestStartup> factory, HttpClient client)
        {
            bool created = false;
            CreateUserRequest createUserRequest = null;
            try
            {
                createUserRequest = new CreateUserRequest
                {
                    Email = "test@test.com",
                    FullName = "Test Account",
                    Password = "Welcome123@",
                    Roles = new string[] { "Admin", "Test Role" },
                    UserName = "test@test.com"
                };
               

                string creatUserUrl = "/api/v1.0/UserAccount/Register";
                var userToCreate = new StringContent(JsonConvert.SerializeObject(createUserRequest), Encoding.UTF8, "application/json");
                var createUserResponse = await client.PostAsync(creatUserUrl, userToCreate);
                //   createUserResponse.EnsureSuccessStatusCode();

                var createdUserString = await createUserResponse.Content.ReadAsStringAsync();
                var createdUser = JsonConvert.DeserializeObject<CreateUserResponse>(createdUserString);
            }
            catch (Exception)
            {
            }
            return createUserRequest;
        }

        public async Task<CreateUserRequest> CreateTestUser(CreateUserRequest newUser)
        {
            bool created = false;
            CreateUserRequest createUserRequest = null;
            try
            {
                createUserRequest = new CreateUserRequest
                {
                    Email = "test@test.com",
                    FullName = "Test Account",
                    Password = "Welcome123@",
                    Roles = new string[] { "Admin", "Test Role" },
                    UserName = "test@test.com"
                };
                if (newUser != null)
                {
                    createUserRequest = newUser;
                }
                string creatUserUrl = "/api/v1.0/UserAccount/Register";
                var userToCreate = new StringContent(JsonConvert.SerializeObject(createUserRequest), Encoding.UTF8, "application/json");
                var createUserResponse = await client.PostAsync(creatUserUrl, userToCreate);
                //   createUserResponse.EnsureSuccessStatusCode();

                var createdUserString = await createUserResponse.Content.ReadAsStringAsync();
                var createdUser = JsonConvert.DeserializeObject<CreateUserResponse>(createdUserString);
            }
            catch (Exception ex)
            {
            }
            return createUserRequest;
        }


        public async Task<LoginToken> GetLoginToken(CreateUserRequest createUserRequest)
        {
            LoginToken loginToken = new LoginToken();
            try
            {
                var tokenUrl = "/connect/token";
                var getTokenRequest = "email=" + createUserRequest.Email + "&password=" + createUserRequest.Password + "&grant_type=password&scope=openid email&username=" + createUserRequest.UserName;

                var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
                tokenRequest.Content = new StringContent(getTokenRequest, Encoding.UTF8, "application/x-www-form-urlencoded");

                var geUserTokenResponse = await client.SendAsync(tokenRequest);
                geUserTokenResponse.EnsureSuccessStatusCode();

                var userTokenString = await geUserTokenResponse.Content.ReadAsStringAsync();
                loginToken = JsonConvert.DeserializeObject<LoginToken>(userTokenString);
            }
            catch (Exception ex)
            {
            }
            return loginToken;
        }

        public async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string url, Object data, LoginToken loginToken)
        {
            HttpResponseMessage response = null;
            try
            {
                var request = new HttpRequestMessage(httpMethod, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginToken.access_token);
                if (data != null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    request.Content = content;
                }

                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
            }
            return response;
        }

        public async Task<LoginToken> GetTokenForTestUser(CreateUserRequest createUserRequest, WebFactory<IntegrationTestStartup> factory)
        {
            LoginToken loginToken = new LoginToken();
            try
            {
                CreateUserRequest createdUser = await this.CreateTestUser(createUserRequest);
                loginToken = await this.GetLoginToken(createdUser);
            }
            catch (Exception ex)
            {
            }
            return loginToken;
        }

    }
}