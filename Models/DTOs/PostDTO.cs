namespace StudentBloggAPI.Models.DTOs;

public record PostDTO(
    int Id, 
    int UserId, 
    string Title, 
    string Content, 
    DateTime Created,
    DateTime Updated);

