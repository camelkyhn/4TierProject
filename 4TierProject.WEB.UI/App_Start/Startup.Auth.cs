using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Microsoft.Owin.Security.OAuth;
using _4TierProject.BLL.Managers;
using Microsoft.Owin.Security.Facebook;
using System.Threading.Tasks;
using System.Net.Http;
using _4TierProject.Common.Helpers;
using _4TierProject.Common.Entities;
using _4TierProject.BLL.Base;
using _4TierProject.WEB.UI.Providers;

namespace _4TierProject.WEB.UI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Veritabanı bağlamını ve kullanıcı yöneticisini ve oturum açma yöneticisini istek başına tek örnek kullanacak şekilde yapılandırın
            app.CreatePerOwinContext(DatabaseContextFactory.CreateDB);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Uygulamanın oturum açan kullanıcının bilgilerini depolamak için tanımlama bilgisi kullanmasını sağlayın
            // ve üçüncü taraf bir oturum açma sağlayıcısı üzerinden oturum açan kullanıcı bilgilerini tanımlama bilgileri olarak saklamak için  veritabanı bağlamını ve kullanıcı yöneticisini yapılandırın
            // Oturum açma tanımlama bilgilerini yapılandırın
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Kullanıcı oturum açtığında güvenlik damgasının uygulama tarafından doğrulanmasını sağlar.
                    // Bu, parola değiştirdiğinizde veya hesabınıza dış oturum eklediğinizde kullanılan bir güvenlik özelliğidir.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // İki öğeli kimlik doğrulama işleminde ikinci öğeyi doğrulama sırasında uygulamanın, kullanıcı bilgilerini geçici olarak saklanmasını sağlar.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Uygulamanın, telefon veya e-posta gibi ikinci oturum açma doğrulama öğesini hatırlamasını sağlar.
            // Bu seçeneği işaretlerseniz, oturum açma işlemi sırasındaki ikinci doğrulama adımınız oturum açtığınız cihazda hatırlanır.
            // Bu, oturum açarken kullandığınız Beni Hatırla seçeneğine benzer.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Üçüncü taraf oturum sağlayıcılarla oturum açmaya olanak tanımak için aşağıdaki satırlardan açıklamayı kaldırın
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: ConfigurationManager.AppSettings["FacebookId"],
            //   appSecret: ConfigurationManager.AppSettings["FacebookSecret"]);

            var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = ConfigHelper.Get<string>("FacebookId"),
                AppSecret = ConfigHelper.Get<string>("FacebookSecret"),
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name,location",
                Scope = { "email user_friends user_likes user_photos" },
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:tokens:facebook", context.AccessToken));
                        return Task.FromResult(0);
                    }
                },
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                SendAppSecretProof = true
            };

            //facebookAuthenticationOptions.Scope.Add("email user_friends user_likes user_photos");

            app.UseFacebookAuthentication(facebookAuthenticationOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = ConfigurationManager.AppSettings["GoogleId"],
            //    ClientSecret = ConfigurationManager.AppSettings["GoogleSecret"]
            //});
        }
    }

    public class FacebookBackChannelHandler : HttpClientHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            // Replace the RequestUri so it's not malformed
            if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}