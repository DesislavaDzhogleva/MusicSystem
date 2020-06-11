using AutoMapper;
using MusicSystem.Data.Models;
using MusicSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Song, SongDto>();
            CreateMap<SongDto, Song>();
            CreateMap<SongPerformerDto, SongPerformer>();
            CreateMap<SongPerformer, SongPerformerDto>();
            CreateMap<ProducerDto, Producer>();
            CreateMap<Producer, ProducerDto>();
            CreateMap<AlbumDto, Album>();
            CreateMap<Album, AlbumDto>();
            CreateMap<WriterDto, Writer>();
            CreateMap<Writer, WriterDto>();

        }
    }
}
