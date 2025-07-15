using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Eticket.Models;
using Eticket.Models.ViewModels;
using Eticket.Repository;
using Eticket.Repository.IRepositories;
using Eticket.Utility;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Eticket.Areas.Idintity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailsender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserOTPRepository _userOTPRepository;

        public AccountController( UserManager<ApplicationUser> userManager ,IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager, IUserOTPRepository userOTPRepository)


        {
            _userManager = userManager;
            _emailsender = emailSender;
            _signInManager = signInManager;
            _userOTPRepository = userOTPRepository;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            ApplicationUser applicationUser = new()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                UserName=registerVM.UserName,
                Email = registerVM.Email,
                PhoneNumber = registerVM.Phone,
                Address = registerVM.Address,

            };

           // ApplicationUser applicationUser=registerVM.Adapt<ApplicationUser>();
           var result= await _userManager.CreateAsync(applicationUser,registerVM.Password);
            if (result.Succeeded)
            {
                var token= await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                var link = Url.Action(nameof(ConfirmEmail), "Account", new {area="Identity",token,
                
               UserId=applicationUser.Id},Request.Scheme);
              await  _emailsender.SendEmailAsync(registerVM.Email, "Confirm your Email your Account",
                    $"<h1>Conifirm by click <a href={link}>Here</a></h1>");

             await   _userManager.AddToRoleAsync(applicationUser, SD.Customer);
                TempData["Notification-Succc"] = "Create Account Sucssfully";
                return RedirectToAction(nameof(Login));
            }
            foreach(var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
            }
            // TempData["Notification-filed"]=string.Join(",",result.Errors);
            return View(registerVM);
        }
        public async Task<IActionResult> ConfirmEmail(string token,string UserId)
        {
            var user=await _userManager.FindByIdAsync(UserId);
            if(user is not  null)
            {
          var result=  await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
            return View();

                }

            }
            return NotFound();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            var user = await _userManager.FindByEmailAsync(loginVm.EmailORUserName);
            if (user is null)
                user = await _userManager.FindByNameAsync(loginVm.EmailORUserName);
            if (user is not null) 
            {
               var result =await _signInManager.PasswordSignInAsync(user.UserName, loginVm.Password, loginVm.RememberMe,true);
            
                if(result.Succeeded)
                {
                    TempData["Login-Notification"] = "Login successsfuly";
                return RedirectToAction("Index", "Customer", new {area="customer"});
                   
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "to many incrroct login");
                return View(loginVm);
                }
                ModelState.AddModelError(string.Empty, "invalid Email or password");

            }
            ModelState.AddModelError(string.Empty, "invalid Email or password");  
            return View(loginVm);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["notification-logout"] = "Logout Suessfully";
            return RedirectToAction(nameof(Login), "Account", new { area = "Identity" });
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(forgetPasswordVM);
            }
            var user = await _userManager.FindByEmailAsync(forgetPasswordVM.EmailORUserName) ?? await _userManager.FindByNameAsync(forgetPasswordVM.EmailORUserName);

            var userOTP = await _userOTPRepository.GetAsync(e => e.ApplicationUserId == user.Id);

            var totalOTPs = userOTP.Count(e => (e.Date.Day == DateTime.UtcNow.Day) && (e.Date.Month == DateTime.UtcNow.Month) && (e.Date.Year == DateTime.UtcNow.Year));

            if (totalOTPs < 3)
            {
                if (user is not null)
                {
                    var OTPNumber = new Random().Next(1000, 9999);
                    await _emailsender.SendEmailAsync(user.Email!, "Reset Password", $"<h1>Reset Password Using OTP Number {OTPNumber}</h1>");

                    await _userOTPRepository.CreateAsync(new()
                    {
                        Code = OTPNumber.ToString(),
                        Date = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddHours(1),
                        ApplicationUserId = user.Id
                    });
                    await _userOTPRepository.CommitAsync();
                }

                TempData["RedirectToAction"] = Guid.NewGuid().ToString();
                return RedirectToAction(nameof(ResetPassword), new { userId = user.Id! });
            }

            // Send msg
            TempData["error-notification"] = "Too Many Request, Please try again Later";
            return View(forgetPasswordVM);
        }
        public IActionResult ResetPassword(string userId)
        {
            if (TempData["RedirectToAction"] is not null)
            {
                if (userId is not null)
                {
                    return View(new ResetPasswordVM()
                    {
                        UserId = userId
                    });
                }

            }

            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var userOTP = (await _userOTPRepository.GetAsync(e => e.ApplicationUserId == resetPasswordVM.UserId)).OrderBy(e => e.Id).LastOrDefault();

            if (userOTP is not null)
            {
                if (DateTime.UtcNow < userOTP.ExpirationDate && !userOTP.Status && userOTP.Code == resetPasswordVM.Code)
                {
                    TempData["RedirectToAction"] = Guid.NewGuid().ToString();
                    return RedirectToAction(nameof(ChangePassword), new { userId = userOTP.ApplicationUserId! });
                }
            }

            // Error
            ModelState.AddModelError(string.Empty, "Invalid Code");
            return View(resetPasswordVM);
        }
        public IActionResult ChangePassword(string userId)
        {
            if (TempData["RedirectToAction"] is not null)
            {
                return View(new ChangePasswordVM()
                {
                    UserId = userId
                });
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordVM);
            }

            var user = await _userManager.FindByIdAsync(changePasswordVM.UserId);

            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, changePasswordVM.Password);

                
                TempData["success-notification"] = "Reset Password Successfully";
                return RedirectToAction(nameof(Login));

            }

            return NotFound();
        }
        



    }
}
