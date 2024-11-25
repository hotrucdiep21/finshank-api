using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<bool> CommentExistAsync(int id);
        Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto updateComment);
        Task<Comment?> DeleteAsync(int id);
    }
}