namespace Application.DTO.Response;

public class SingleFileDto
{
    public SingleFileDto(byte[] bytes, string directory)
    {
        Bytes = bytes;
        Directory = directory;
    }

    public byte[] Bytes { get; set; }
    public string Directory { get; set; }
}