using AutoMapper;
using Hogarnet.DAT.Repositorios.Contrato;
using Hogarnet.DTO;
using Hogarnet.MOD;
using Hogarnet.NEG.Servicios.Contrato;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogarnet.NEG.Servicios
{
    public class DashBoardService: IDashBoardService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IMapper _mapper;

        public DashBoardService(IGenericRepository<Producto> productoRepositorio, IVentaRepository ventaRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _ventaRepositorio = ventaRepositorio;
            _mapper = mapper;
        }

        private IQueryable<Venta> RetornarVenta(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaVenta).Select(v => v.FechaVenta).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaVenta.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentaUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, -7);
                total = tablaVenta.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, 7);
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-US"));
        }

        private async Task<int> TotalProducto()
        {
            IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();

            int total = _productoQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.FechaVenta.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return resultado;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO vmDashboard = new DashBoardDTO();
            try
            {
                vmDashboard.TotalVentas = await TotalVentaUltimaSemana();
                vmDashboard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashboard.TotalProductos = await TotalProducto();

                List<VentasSemanaDTO> listadeVentaSemana = new List<VentasSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listadeVentaSemana.Add(new VentasSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value,
                    });
                }

                vmDashboard.VentasUltimaSemana = listadeVentaSemana;

            }
            catch (Exception)
            {

                throw;
            }

            return vmDashboard;
        }
    }
}
