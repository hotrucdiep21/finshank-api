using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
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

        public async Task<bool> CommentExistAsync(int id)
        {
            return await _context.Comment.AnyAsync(cmt => cmt.Id == id);
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comment.FirstOrDefaultAsync(cmt => cmt.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            _context.Comment.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
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

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto updateComment)
        {
            var existComment = await _context.Comment.FirstOrDefaultAsync(cmt => cmt.Id == id);
            if (existComment == null)
            {
                return null;
            }
            existComment.Title = updateComment.Title;
            existComment.Content = updateComment.Content;

            await _context.SaveChangesAsync();
            return existComment;

        }
    }
}