using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{

    [Route("api/portfolio")]
    [ApiController]

    public class PorfolioController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager
        private readonly IStockRepository _stockRepo
         private readonly IPortfolioRepository _portfolioRepo
       public PorfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo,  IPortfolioRepository portfolioRepo)
       {
        _userManager = userManager;
        _stockRepo = stockRepo;
        _portfolioRepo = portfolioRepo

       }

       [HttpGet]
       [Authorize]

       public async Task<IActionResult> GetUserPortfolio(){

        var userName = User.GetUser();

        // User above is being inherited from the COntrollerBase. Whenever we see it , User allows to grab anything that's associated with the User and the Claim.

        var appUser = await _userManager.FindByNameAsync(username)
        var userPorfolio = await _portfolioRepo.GetUserPortfolio(appUser);

        return OK(userPorfolio);
       }
    }
}