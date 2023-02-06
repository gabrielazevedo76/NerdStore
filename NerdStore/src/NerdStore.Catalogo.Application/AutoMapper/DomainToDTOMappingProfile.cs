﻿using AutoMapper;
using NerdStore.Catalogo.Application.DTO;
using NerdStore.Catalogo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
                .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
                .ForMember(d => d.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));

            CreateMap<Categoria, CategoriaDTO>();
        }
    }

}
