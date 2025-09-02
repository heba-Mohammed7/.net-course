namespace Task.Services;

public interface IFileUpload
{
    Task<string> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken);
}