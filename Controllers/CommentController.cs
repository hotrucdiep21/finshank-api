using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/commment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;  

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var comments = await _commentRepo.GetAllAsync();
            if (comments == null)
            {
                return NotFound();
            }
            var commentDto = comments.Select(cmt => cmt.ToCommentDto());
            return Ok(commentDto);        
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto createCommentDto) {
            var isExist = await _stockRepo.StockExist(stockId);
            if (!isExist)
            {
                return BadRequest("Stock does not exist!");
            }
            var comment = createCommentDto.ToCommentFromCreate(stockId);
            var newComment =  await _commentRepo.CreateCommentAsync(comment);
            return CreatedAtAction(nameof (GetById), new {id = newComment.Id}, newComment.ToCommentDto());
        } 


    }
}