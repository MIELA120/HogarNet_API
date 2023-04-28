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
    public class MenuService:IMenuService
    {
        private readonly IGenericRepository<MenuRol> _menuRolRepositorio;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IGenericRepository<Menu> _menuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<MenuRol> menuRolRepositorio, IGenericRepository<Usuario> usuarioRepositorio, IGenericRepository<Menu> menuRepositorio, IMapper mapper)
        {
            _menuRolRepositorio = menuRolRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _menuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();
            IQueryable<Menu> tbMenu = await _menuRepositorio.Consultar();

            try
            {
                IQueryable<Menu> tbResultado = (from u in tbUsuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                var listaMenu = tbResultado.ToList();

                return _mapper.Map<List<MenuDTO>>(listaMenu);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
