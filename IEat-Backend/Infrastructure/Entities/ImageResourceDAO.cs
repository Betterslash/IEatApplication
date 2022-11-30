using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class ImageResourceDAO : EntityBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public byte[] Data { get; set; }
        public Guid FoodId { get; set; }
    }
}
