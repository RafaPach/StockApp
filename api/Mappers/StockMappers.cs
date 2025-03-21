using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                Company = stockModel.Company,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(x => x.ToCommentDto()).ToList(),
            };
        
        }

        public static Stock ToStockFromCreateDto( this CreateStockRequestDto stockDto)
        {
                return new Stock{

                    Symbol = stockDto.Symbol,
                    Company = stockDto.CompanyName,
                    Purchase = stockDto.Purchase,
                    LastDiv = stockDto.LastDiv,
                    Industry = stockDto.Industry,
                    MarketCap = stockDto.MarketCap,
            };
        }

    }
}