using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CloudBreadAdminWeb.Models;

namespace CloudBreadAdminWeb
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 전자 메일을 보낼 전자 메일 서비스를 여기에 플러그 인으로 추가합니다.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 텍스트 메시지를 보낼 SMS 서비스를 여기에 플러그 인으로 추가합니다.
            return Task.FromResult(0);
        }
    }

    // 이 응용 프로그램에서 사용되는 응용 프로그램 사용자 관리자를 구성합니다. UserManager는 ASP.NET Identity에서 정의하며 응용 프로그램에서 사용됩니다.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // 사용자 이름에 대한 유효성 검사 논리 구성
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 암호에 대한 유효성 검사 논리 구성
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // 사용자 잠금 기본값 구성
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 2단계 인증 공급자를 등록합니다. 이 응용 프로그램은 사용자 확인 코드를 받는 단계에서 전화 및 전자 메일을 사용합니다.
            // 공급자 및 플러그 인을 여기에 쓸 수 있습니다.
            manager.RegisterTwoFactorProvider("전화 코드", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "보안 코드는 {0}입니다."
            });
            manager.RegisterTwoFactorProvider("전자 메일 코드", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "보안 코드",
                BodyFormat = "보안 코드는 {0}입니다."
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // 이 응용 프로그램에서 사용되는 응용 프로그램 로그인 관리자를 구성합니다.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
