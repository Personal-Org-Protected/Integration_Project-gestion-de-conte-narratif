using Application.Common.Mappings;
using AutoMapper;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query.Dto_s
{
    public class UserDisplay : IMapFrom<User>
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string Location { get; set; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string phoneNumber { get; set; }

        public ICollection<RolesDto> rolesDtos { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDisplay>();
        }
    }
}
