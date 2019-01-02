﻿using AutoMapper;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nwEventoMVCa.Core.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDto>()
                    .ForMember(x => x.TicketsCount, m => m.MapFrom(p => p.Tickets.Count()));
            }).CreateMapper();
    }
}
