using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Genratax.Data.Abstrack;
using Genratax.Entity;
using Genratax.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Genratax.Controllers
{
    public class UserController: Controller
    {
                private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;



    
      private readonly IUserRepository _userRepository;
      public UserController(IUserRepository userRepository, IPostRepository postRepository,ICommentRepository commentRepository){
        _userRepository = userRepository;
         _postRepository = postRepository;
        _commentRepository = commentRepository;
      }
    public IActionResult Index()
    {
        return View("~/Views/User/Profile.cshtml");
    }
        public IActionResult Signup(){
            return View();
        }
       [HttpPost]
        public  async Task<IActionResult> Signup(SignUpViewModel model){
            if(ModelState.IsValid){
                var user = await _userRepository.Users.FirstOrDefaultAsync(x=> x.UserName == model.UserName||x.Email == model.Email);
                if(user == null){
                    _userRepository.CreateUser(new User{
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password

                    
                    });
                    return RedirectToAction("Signin");
                }else{
                    ModelState.AddModelError("","Username veya email kullanimda" );
                }
              
            }
            return View(model);
        }
        public IActionResult Signin(){
        if(User.Identity!.IsAuthenticated){
            return RedirectToAction("Index","Posts");
        }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Signin(LoginViewModel model){
            if(ModelState.IsValid){
                var isUser = await _userRepository.Users.FirstOrDefaultAsync(x=> x.Email == model.Email&& x.Password == model.Password);
                if(isUser != null){
                    var UserClaims = new List<Claim>();
                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    UserClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    UserClaims.Add(new Claim(ClaimTypes.GivenName, isUser.UserName ?? ""));
                    if(isUser.Email == "info@furkan.com"){
                        UserClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    var claimsIdentity = new ClaimsIdentity(UserClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties{
                        IsPersistent = true

                    };
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),authProperties);
                        return RedirectToAction("Index","posts");
                }else{
                ModelState.AddModelError("","kullanici adi veya sifre hatali!");
            }
            }
            return View(model);

        }
        public async Task<IActionResult>Signout(){
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("signin");
        }
        public IActionResult Profile(){
            var claims = User.Claims;
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)?? "");
            return View( 
            new PostsViewModel{
        
              Posts =  _postRepository.Posts.Where(i=>i.UserId == userId).ToList(),
            }
            );
             
        }
        public IActionResult ProfileCommented(){
            var claims = User.Claims;
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)?? "");
            return View( 
            new CommentsViewModel{
        
              Comments =  _commentRepository.Comments
              
              .Include(x=>x.User)
              .Include(x=>x.Post)
                .Where(i=>i.UserId == userId)
                .ToList(),
                
            }
            );
    }
}
}