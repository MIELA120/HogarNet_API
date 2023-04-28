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
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hogarnet.NEG.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IGenericRepository<DetalleVenta> detalleVentaRepositorio, IVentaRepository ventaRepositorio, IMapper mapper)
        {
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _ventaRepositorio = ventaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("no se puedo generar la venta");

                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            var listaResutado = new List<Venta>();

            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                    listaResutado = await query.Where(v => v.FechaVenta != null && v.FechaVenta.Value.Date >= fech_Inicio.Date &&
                    v.FechaVenta.Value.Date <= fech_Fin.Date).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation).ToListAsync();

                }
                else
                {
                    listaResutado = await query.Where(v => v.NumeroDocumento == numeroVenta).Include(dv => dv.DetalleVenta)
                   .ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<VentaDTO>>(listaResutado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();
            var listaResultado = new List<DetalleVenta>();

            try
            {
                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                listaResultado = await query
                     .Include(p => p.IdProductoNavigation)
                     .Include(v => v.IdVentaNavigation)
                     .Where(dv => dv.IdVentaNavigation != null && dv.IdVentaNavigation.FechaVenta != null && dv.IdVentaNavigation.FechaVenta.Value.Date >= fech_Inicio.Date &&
                    dv.IdVentaNavigation.FechaVenta.Value.Date <= fech_Fin.Date).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(listaResultado);
        }
    }
}
