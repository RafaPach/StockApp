using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly ApplicationDBContext _context;
    
        public PortfolioRepository(ApplicationDBContext context)
        {
             _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel =  await _context.Portfolios.FirstOrDefaultAsync(x=>x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() ==  symbol.ToLower());  

            if(portfolioModel  == null){
                return null;
            }

            _context.portfolio.Remove(portfolioModel); 
            // Remove does not accept await 
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {

            // Select statment works like a map in JS. In this case, its going to interate over, apply these changes to it and return a new data structure
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                Company = stock.Stock.Company,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap

            }).ToListAsync();
        }
        
    }
}