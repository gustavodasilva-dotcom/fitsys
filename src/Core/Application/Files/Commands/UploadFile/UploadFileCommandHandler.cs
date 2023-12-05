using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands.UploadFile
{
    internal sealed class UploadFileCommandHandler(IHostingEnvironment hostingEnvironment) : IRequestHandler<UploadFileCommand, string>
    {
        private readonly IHostingEnvironment _hostingEnvironment = hostingEnvironment;

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            string filePath, fileName = string.Empty;
            foreach (IFormFile file in request.Form.Files)
            {
                string extension = Path.GetExtension(file.FileName);
                fileName = string.Concat(Guid.NewGuid(), extension);
                filePath = Path.Combine(uploadsFolder, fileName);

                using Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(fileStream, cancellationToken);
            }
            return Path.Combine(uploadsFolder, fileName);
        }
    }
}
