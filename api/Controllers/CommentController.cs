using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;


        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;

        }

        [HttpGet]   

        public async Task<IActionResult> GetAll(){

            // The above allows to perform all the validation added to the DTOs

            var comment = await _commentRepo.GetAllAsync();

            var commentDto = comment.Select(x => x.ToCommentDto());

            return Ok(commentDto);
        }

         [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id){

            var comment = await _commentRepo.GetByIdAsync(id);

            if(comment == null){
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create ([FromRoute] int stockId , CreateCommentDto commentDto){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }


            if(!await _stockRepo.StockExsists(stockId)){
                return BadRequest("Stock Does not Exist");
            }

            var username = User.GetUserName();
            // Get user from database 
            var appUser = await _userManager.FindByNameAsync(username);


            var commentModel = commentDto.ToCommentFromCreate(stockId);
            // Add User to the comments objects, to see who commented it 
            // This will not be added if we do not add Includes in our repository
            commentModel.AppUserId = appUser.Id;
            
            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new {id = commentModel}, commentModel.ToCommentDto());
            }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateDto){
            

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var comment =  await _commentRepo.UpdateAsync(updateDto.ToCommentFromUpdate(id) ,id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id){

              if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var commentModel = await _commentRepo.DeleteAsync(id);

               if(commentModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }

}