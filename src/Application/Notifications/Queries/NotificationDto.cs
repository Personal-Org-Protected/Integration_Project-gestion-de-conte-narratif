using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Queries
{
    public class NotificationDto : IMapFrom<Notification>
    {
        public int idNotification { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public bool read { get; set; }
        public DateTime created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationDto>();
        }
    }
}
