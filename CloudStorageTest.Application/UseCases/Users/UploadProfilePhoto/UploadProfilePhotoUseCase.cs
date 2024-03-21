using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase
{
    private readonly IStorageService _storageService;
    public UploadProfilePhotoUseCase(IStorageService storageService)
    {
        _storageService = storageService;
    }
    public void Execute(IFormFile file)
    {
        var fileStream = file.OpenReadStream();

        var isImage = fileStream.Is<JointPhotographicExpertsGroup>();

        if (isImage == false)
            throw new Exception("The file is nat an image.");

        var user = GetFromDatabase();

        _storageService.Upload(file, user);
    }

    private User GetFromDatabase() {
        return new User
        {
            Id = 1,
            Name = "Fernando",
            Email = "candianirfernando@gmail.com",
            RefreshToken = "1//04iphWwU8RZPGCgYIARAAGAQSNwF-L9IrfNeX4EgsmTiRBfYfcv-DvpffwXvHVLL7lXVUrNp0IvEQT2XrzszMjzVIlRrxooUPvow",
            AccessToken = "ya29.a0Ad52N3_oXQRhE3D7h_3GaJbIgLuUZNUiW_ZZpAJI9NqPLrQ_TaBGPsy091JP1tO2z-7bjB0Mb7sTKaf156FeqNnHd3cLy7ghLDSlSoC7hisTeZzgOKPXVcUxEbRlUYNUm9Z4V5SUEECuWEImmc3QuXAkulcxNbmKM3gFaCgYKAbUSARASFQHGX2Mi2fphLkacEL7ALizvYcyHzg0171"
        };
    }
}
