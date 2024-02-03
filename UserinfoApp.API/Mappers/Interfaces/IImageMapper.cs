using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers.Interfaces
{
    public interface IImageMapper
    {
        List<ImageResultDto> Map(IEnumerable<Image> entities);
        Image Map(ImageUploadRequestDto dto, int userinfoId);
    }
}
