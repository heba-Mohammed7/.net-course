using authentication.Services.Interfaces;

namespace authentication.Services.Implementations;

public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileService> _logger;

        public FileService(IWebHostEnvironment environment, ILogger<FileService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public string GetFilePath(string relativePath)
        {
            var fullPath = Path.Combine(_environment.WebRootPath, relativePath);
            _logger.LogInformation("Resolved file path: {FullPath}", fullPath);
            return fullPath;
        }
    }