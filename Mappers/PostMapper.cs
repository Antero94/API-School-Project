using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Mappers;

public class PostMapper : IMapper<Post, PostDTO>
{
    public PostDTO MapToDTO(Post model)
    {
        return new PostDTO(model.Id, model.UserId, model.Title, model.Content, model.Created, model.Updated);
    }

    public Post MapToModel(PostDTO dto)
    {
        var dtNow = DateTime.Now;
        return new Post()
        {
            Id = dto.PostID,
            UserId = dto.UserID,
            Title = dto.Title,
            Content = dto.PostContent,
            Created = dtNow,
            Updated = dtNow
        };
    }
}
