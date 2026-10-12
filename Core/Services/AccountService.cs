using Application.Interfaces;
using Application.Interfaces.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AccountService : IAccountService
	{
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IRoleRepository _roleRepository;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRoleRepository roleRepository)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleRepository = roleRepository;
		}

		public async Task<bool> EmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<SignInResult> Login(LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.Email!);
            
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(user, login.Password!, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterRequest register)
        {
            var userRoleId = _roleRepository.GetRoleIdByName("User");

            var user = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                RoleId = await userRoleId
            };

            var result = await _userManager.CreateAsync(user, register.Password!);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<bool> UserNameExists(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null;
        }
    }
}