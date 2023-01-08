using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BookEventread.Dtos;
using BookEventread.Models;


namespace BookEvent2.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EventModel, EventDto>().ReverseMap();
        }
    }
}