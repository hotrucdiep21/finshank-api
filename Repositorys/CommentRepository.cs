using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositorys
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }


        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await _context.Comment.ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(cmt => cmt.Id == id);
            return comment;
        }
    }
}