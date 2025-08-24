using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return null;
        }

        return comment;
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
    {
        var existingComment = await _context.Comments.FindAsync(id);
        if (existingComment == null)
        {
            return null;
        }

        existingComment.Content = commentModel.Content;
        existingComment.Title = commentModel.Title;
        _context.Comments.Update(existingComment);
        await _context.SaveChangesAsync();
        return existingComment;
    }

    public async Task<Comment?> DeleteCommentAsync(int id)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (commentModel == null)
        {
            return null;
        }

        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }
}