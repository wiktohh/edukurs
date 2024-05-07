namespace Application.DTO.Response;

public class FileDto
{
    public FileDto(MemoryStream memoryStream, string directory)
    {
        MemoryStream = memoryStream;
        Directory = directory;
    }

    public MemoryStream MemoryStream { get; set; }
    public string Directory { get; set; }
}