using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ImageResourceDAO : IEntity
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        public string Path { get; set; }
        public FoodDAO Food { get; set; }
    }
}
