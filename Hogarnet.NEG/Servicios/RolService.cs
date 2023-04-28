using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hogarnet.NEG.Servicios.Contrato;
using Hogarnet.DAT.Repositorios.Contrato;
using Hogarnet.DTO;
using Hogarnet.MOD;

namespace Hogarnet.NEG.Servicios
{
    public class RolService: IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepositorio, IMapper mapper)
        {
            _rolRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaRoles = await _rolRepositorio.Consultar();
                return _mapper.Map<List<RolDTO>>(listaRoles.ToList());   
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al consultar", ex);
            }
        }
    }
}
