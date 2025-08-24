using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Stock;

namespace api.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepo = commentRepository;
            _stockRepo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentDto = comments.Select(comment => comment.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepo.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateStockCommentAsync([FromRoute] int stockId,
            CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _stockRepo.StockExistsAsync(stockId))
            {
                return BadRequest("Stock doesn't exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] int id,
            [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentRepo.UpdateCommentAsync(id, updateCommentDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete()]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteCommentAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }
    }
}