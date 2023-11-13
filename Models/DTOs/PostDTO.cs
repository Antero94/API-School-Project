namespace StudentBloggAPI.Models.DTOs;

public record PostDTO(
    int PostID, 
    int UserID, 
    string Title, 
    string PostContent, 
    DateTime Created,
    DateTime Updated);

