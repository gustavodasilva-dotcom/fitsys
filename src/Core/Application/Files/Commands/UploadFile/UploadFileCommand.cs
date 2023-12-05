using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands.UploadFile
{
    public sealed record UploadFileCommand(IFormCollection Form) : IRequest<string>;
}
