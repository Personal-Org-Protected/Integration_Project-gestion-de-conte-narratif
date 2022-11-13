using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.Queries
{
    public class TransactionsDto : IMapFrom<TransactionsDto>
    {
        public string NameBook { get; set; }
        public double price { get; set; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd -- H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateTransaction { get; set; }
        public int StoryTellId { get; set; }
        public string IdLibrary { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Transaction, TransactionsDto>();
        }
    }
}
