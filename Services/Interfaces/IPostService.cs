using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<ICollection<PostDTO>> GetAllPostsAsync(int pageNr, int pageSize);
        Task<PostDTO?> AddPostAsync(PostDTO postDTO);
        Task<PostDTO?> UpdatePostAsync(int Id, PostDTO postDTO);
        Task<PostDTO?> DeletePostAsync(int Id);
        Task<PostDTO?> GetPostsByIdAsync(int Id);
    }
}
