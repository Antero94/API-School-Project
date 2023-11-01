namespace StudentBloggAPI.Models.DTOs;

public record PostDTO(
    int PostID, 
    int UserID, 
    string Header, 
    string PostContent, 
    DateTime Created);

