using Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Queries
{
    public class GetAllFoodsQuery : IQueryBase
    {

    }

    public class GetFoodByIdQuery : IQueryBase 
    {
        public Guid Id { get; set; }
    }
}
