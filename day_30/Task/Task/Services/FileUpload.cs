namespace Task.Services;

public class FileUpload : IFileUpload
{
    public async Task<string> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var directoryPath = Path.Combine("wwwroot", folder);
        Directory.CreateDirectory(directoryPath);
        var filePath = Path.Combine(directoryPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        return $"/{folder}/{fileName}";
    }
}