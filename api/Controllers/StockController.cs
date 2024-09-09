using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query ){

            var stocks = await _stockRepo.GetAllStocksAsync(query);
            
            var stockDto = stocks.Select(s=>s.ToStockDto());

            // Select works as a map here, so its gonna interate through the list and then its gonna check the dto and get the info according to this DTO

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetStockByIdAsync(id);
            if (stock == null){
                return NotFound(); 
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {

              if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var stockModel = stockDto.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());

            //CreateAtAction is used for POST requests. Gets the id into the function GetByID and returns into a form of StockDto
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){

              if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            if (stockModel == null){
                return  NotFound();
            }

             return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id){

              if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepo.DeleteAsync(id); 
            if(stockModel == null){
                return NotFound();
            }

            return NoContent();

            // NoContent in a delete is a sucessfull code
        }
    }
}