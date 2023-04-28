using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Hogarnet.DAT.Repositorios.Contrato;
using Hogarnet.NEG.Servicios.Contrato;
using Hogarnet.DTO;
using Hogarnet.MOD;
using Microsoft.EntityFrameworkCore;

namespace Hogarnet.NEG.Servicios
{
    public class MarcaService : IMarcaService
    {
        private readonly IGenericRepository<Marca> _marcaRepositorio;
        private readonly IMapper _mapper;

        public MarcaService(IGenericRepository<Marca> marcaRepositorio, IMapper mapper)
        {
            _marcaRepositorio = marcaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MarcaDTO>> Lista()
        {
            try
            {
                var listaCategoria = await _marcaRepositorio.Consultar();

                return _mapper.Map<List<MarcaDTO>>(listaCategoria.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
