using CodewarsSprintBackend.Services.Context;
using CodewarsSprintBackend.Models;
using CodewarsSprintBackend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CodewarsSprintBackend.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string? Username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;
        }

        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            bool result = false;
            if (!DoesUserExist(UserToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);
                newUser.Id = UserToAdd.Id;
                newUser.isAdmin = UserToAdd.isAdmin;
                newUser.Username = UserToAdd.Username;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();

            byte[] SaltByte = new byte[64];
            var provider = new RNGCryptoServiceProvider();

            provider.GetNonZeroBytes(SaltByte);

            var Salt = Convert.ToBase64String(SaltByte);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;


        }

        public bool VerifyUserPassword(string? Password, string? storeHash, string? storeSalt)
        {
            var SaltBytes = Convert.FromBase64String(storeSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, SaltBytes, 10000);
            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storeHash;
        }

        public IActionResult Login(LoginDTO User)
        {
            IActionResult Result = Unauthorized();

            if (DoesUserExist(User.Username))
            {
                UserModel foundUser = GetUserByUsername(User.Username);
                if (VerifyUserPassword(User.Password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    Result = Ok(new { Token = tokenString });
                }
            }
            return Result;
        }

        public UserModel GetUserByUsername(string? username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _context.UserInfo;
        }

          public isAdminDTO GetIsAdmin(string username){
            var UserInfo = new isAdminDTO();
            var foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);
            UserInfo.Id = foundUser.Id;
            UserInfo.Username = foundUser.Username;
            UserInfo.isAdmin = foundUser.isAdmin;
            return UserInfo;
        }

    }
}