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

        public Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            throw new NotImplementedException();
        }

        public Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            throw new NotImplementedException();
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