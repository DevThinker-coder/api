using api.Models;
using api.Dtos.Comment;
using api.Dtos.Stock;

namespace api.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Content = commentModel.Content,
            Title = commentModel.Title,
            CreatedAt = commentModel.CreatedAt,
            StockId = commentModel.StockId
        };
    }

    public static Comment ToCommentFromCreate(this CreateCommentDto createCommentDto, int stockId)
    {
        return new Comment
        {
            Title = createCommentDto.Title,
            Content = createCommentDto.Content,
            StockId = stockId
        };
    } 
    
    public static Comment ToCommentFromUpdate(this UpdateCommentDto updateCommentDto)
    {
        return new Comment
        {
            Title = updateCommentDto.Title,
            Content = updateCommentDto.Content,
        };
    }
}