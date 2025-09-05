namespace authentication.Services.Interfaces;

public interface IFileUpload
{
    Task<string> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken);
}