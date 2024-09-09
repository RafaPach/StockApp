using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comentModel)
        {
             await _context.Comments.AddAsync(comentModel);
             await _context.SaveChangesAsync();
             return comentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {

            var comment =  await _context.Comments.FirstOrDefaultAsync(x=>x.Id == id);  

            if(comment  == null){
                return null;
            }

            _context.Comments.Remove(comment); 
            // Remove does not accept await 
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(Comment commentModel, int id)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if(existingComment == null){
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}