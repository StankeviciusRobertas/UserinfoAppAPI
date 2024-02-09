using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.DAL.Entities;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.DAL.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(UserinfoAppDbContext context) : base(context)
        {
        }

        // Override method to get all Image entities from the database, including related UserInfo entities
        override public IQueryable<Image> GetAll()
        {
            // Using Include to eager load UserInfo navigation property
            return _context.Images.Include(i => i.Id);
        }

        // Override method to get a specific Image entity by its id, including the related UserInfo entity
        public override Image? Get(int id)
        {
            // Using Include to eagerl load UserInfo navigation property
            return _context.Images.FirstOrDefault(i => i.AccountId == id);
        }
    }
}
