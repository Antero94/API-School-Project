namespace StudentBloggAPI.Models.DTOs;

public record CommentDTO(
    int Id,
    int PostId,
    int UserId,
    string Content,
    DateTime DateCommented);
