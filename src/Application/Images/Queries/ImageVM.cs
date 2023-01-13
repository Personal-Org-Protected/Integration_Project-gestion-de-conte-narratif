using Application.Images.Queries.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Queries
{
    public class ImageVM
    {
        public IList<ImageDto> Images { get; set; }
    }
}
