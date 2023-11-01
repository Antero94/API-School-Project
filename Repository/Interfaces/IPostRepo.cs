using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces;

public interface IPostRepo
{
    Task<Post?> AddPostAsync(Post post);
    Task<Post?> UpdatePostAsync(int Id, Post post);
    Task<Post?> DeletePostByIdAsync(int Id);
    Task<Post?> GetPostByIdAsync(int Id);
    Task<ICollection<Post>> GetPostsAsync();
}
