using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.API.Mappers.Interfaces;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers
{
    public class ImageMapper : IImageMapper
    {
        public ImageResultDto Map(Image entity)
        {
            return new ImageResultDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
        }

        public List<ImageResultDto> Map(IEnumerable<Image> entities)
        {
            return entities.Select(Map).ToList();
        }


        public Image Map(ImageUploadRequestDto dto, int userinfoId)
        {
            using var stream = new MemoryStream();
            dto.Image.CopyTo(stream);
            var imageBytes = stream.ToArray();
            return new Image
            {
                Name = dto.Name!,
                Description = dto.Description!,
                UserinfoId = userinfoId,
                ImageBytes = imageBytes,
            };
        }
    }
}
