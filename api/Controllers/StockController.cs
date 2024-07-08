using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAll(){

            var stocks = _context.Stock.ToList().Select(s=>s.ToStockDto());

            // Select works as a map here, so its gonna interate through the list and then its gonna check the dto and get the info according to this DTO

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stock.Find(id);
            if (stock == null){
                return NotFound(); 
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]

        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            _context.Stock.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());

            //CreateAtAction is used for POST requests. Gets the id into the function GetByID and returns into a form of StockDto
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){
            
            var stockModel = _context.Stock.FirstOrDefault(x=>x.Id == id); 

            if (stockModel == null){
                return  NotFound();
            }


             stockModel.Symbol = updateDto.Symbol;
             stockModel.Company = updateDto.Company;
             stockModel.Purchase  = updateDto.Purchase;
             stockModel.LastDiv = updateDto.LastDiv;
             stockModel.Industry = updateDto.Industry;
             stockModel.MarketCap = updateDto.MarketCap;

             _context.SaveChanges();

             return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public IActionResult Delete([FromRoute] int id){

            var stockModel = _context.Stock.FirstOrDefault(x=>x.Id == id);    
            if(stockModel == null){
                return NotFound();
            }

            _context.Stock.Remove(stockModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}