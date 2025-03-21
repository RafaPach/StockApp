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

        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
         private readonly IPortfolioRepository _portfolioRepo;
       public PorfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo,  IPortfolioRepository portfolioRepo)
       {
        _userManager = userManager;
        _stockRepo = stockRepo;
        _portfolioRepo = portfolioRepo;

       }

       [HttpGet]
       [Authorize]

       public async Task<IActionResult> GetUserPortfolio(){

        // User below is being inherited from the COntrollerBase. Whenever we see it , User allows to grab anything that's associated with the User and the Claim.

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
       }

       [HttpPost]
       [Authorize]

       public async Task<IActionResult> AddPortfolio(string symbol){

        var username =  User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock =  await _stockRepo.GetBySymbolAsync(symbol);

        if(stock==null) return BadRequest("Stock not found!");

        var userPortfolio =  await _portfolioRepo.GetUserPortfolio(appUser);

        if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Can not add same stock to portfolio");

        var portofolioModel = new Portfolio {

            StockId = stock.Id,
            AppUserId = appUser.Id;
        };

        await _portfolioRepo.CreateAsync(portofolioModel);

        if(portofolioModel == null){ 
            return Status.Code(500, "Could not create")
             }
         else {return Created();
       }
    }

    [HttpDelete]
    [Authorize]

     public async Task<IActionResult> DeletePortfolio(string symbol){

        // GetUserName comes from claims
        var username = username.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);

        var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

        var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

        if(filteredStock.Count() == 1){
            await _portfolioRepo.DeletePortfolio(appUser, symbol);
        } else {

            return BadRequest("Stock is not in your portfolio");
        }

        return Ok();
     }
}