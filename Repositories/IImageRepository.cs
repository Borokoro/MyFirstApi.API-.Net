using MyFirstApi.API.Models.Domain;
using System.Net;

namespace MyFirstApi.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
