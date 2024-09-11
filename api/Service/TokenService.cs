using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecuriryKey _key;
        public TokenService(IConfiguration config)
        {
            _congif = config
            _key = new SymmetricSecuriryKey(Encoding.UTF8.GetBytes(_config.["JWT:SigningKey"]))

            // Enconding turns it into byte, keys dont accept a regular string
        }
{
        public string CreateToken(AppUser user){
            new Claim(JWTRegisteredClaimNames.Email, user.Email),
            new Claim(JWTRegisteredClaimNames.GivenName, user.UserName);
        }

        var creds = new SigningCredentials(_key, SecurityAlghorithms.HmacSha512Signature)

        // HmacSha512Signature is a sort of encryption

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer =  _config["JWT:Issuer"],
            Audience -  _config["JWT:Audience"]
        }

        var tokenHandler = new JWTSecutiryTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token) 
        // Returns token is form of a string
    }
}