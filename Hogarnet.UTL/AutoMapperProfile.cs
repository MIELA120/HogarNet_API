using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Hogarnet.DTO;
using Hogarnet.MOD;

//Conversion de los modelos a las clases DTO
namespace Hogarnet.UTL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {

            #region Estado
            CreateMap<Estado, UsuarioDTO>()
            .ForMember(destino => destino.IdEstado,
                opt => opt.MapFrom(origen => origen.Activo));
            CreateMap<UsuarioDTO, Estado>()
                .ForMember(destino => destino.Activo,
                opt => opt.MapFrom(origen => origen.IdEstadoNavigation));

            #endregion Estado


            #region Rol
            CreateMap<Rol,RolDTO>().ReverseMap();
            #endregion Rol


            #region Menu
            CreateMap<Menu,MenuDTO>().ReverseMap();
            #endregion Menu


            #region Usuario

            CreateMap<Usuario, UsuarioDTO>()
            .ForMember(destino => destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation != null ? origen.IdRolNavigation.NombreRol : null))
            .ForMember(destino => destino.IdEstado,
                opt => opt.MapFrom(origen => origen.IdEstadoNavigation != null && origen.IdEstadoNavigation.Activo == true ? 1 : 0))
            .IncludeMembers(origen => origen.IdEstadoNavigation);


            CreateMap<Usuario, SesionDTO>()
            .ForMember(destino => destino.RolDescripcion,
             opt => opt.MapFrom(origen => origen.IdRolNavigation != null ? origen.IdRolNavigation.NombreRol : null));

            CreateMap<UsuarioDTO, Usuario>()
            .ForMember(destino => destino.IdRolNavigation,
            opt => opt.Ignore())

            .ForMember(destino => destino.IdEstadoNavigation,
            opt => opt.MapFrom(origen => new Estado { Activo = origen.IdEstado == 1 }));
            // En este mapeo, debes crear una nueva instancia de Estado y asignar el valor de Activo según el valor de IdEstado en origen.
            // Ignorar el IdRolNavigation es correcto, ya que en el DTO no necesitas mapear ese objeto.



            #endregion Usuario

            
            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria


            #region Marca
            CreateMap<Marca, MarcaDTO>().ReverseMap();
            #endregion Marca


            #region Producto

            CreateMap<Producto, ProductoDTO>()
            .ForMember(destino => destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation != null ? origen.IdCategoriaNavigation.NombreCategoria : null))
            .ForMember(destino => destino.DescripcionMarca,
                opt => opt.MapFrom(origen => origen.IdMarcaNavigation != null ? origen.IdMarcaNavigation.NombreMarca : null))
            .ForMember(destino => destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio != null ? origen.Precio.Value : null, new CultureInfo("es-CO"))))
            .ForMember(destino => destino.IdEstado,
               opt => opt.MapFrom(origen => origen.IdEstadoNavigation != null && origen.IdEstadoNavigation.Activo == true ? 1 : 0));

            CreateMap<ProductoDTO,Producto>()
            .ForMember(destino => destino.IdCategoriaNavigation,
               opt => opt.Ignore())
            .ForMember(destino => destino.IdMarcaNavigation,
               opt => opt.Ignore())
            .ForMember(destino => destino.Precio,
               opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO"))))
            .ForMember(destino => destino.IdEstadoNavigation,
               opt => opt.MapFrom(origen => new Estado { Activo = origen.IdEstado == 1 }));

            #endregion Producto


            #region Venta
            CreateMap<Venta,VentaDTO>()
            .ForMember(destino => destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total != null ? origen.Total.Value : null, new CultureInfo("es-CO"))))       
            .ForMember(destino => destino.FechaVenta,
                opt => opt.MapFrom(origen => origen.FechaVenta != null ? origen.FechaVenta.Value.ToString("dd/MM/yyyy"):null));

            CreateMap<VentaDTO, Venta>()
            .ForMember(destino => destino.Total, 
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO"))));
            #endregion Venta


            #region DetalleVenta
            CreateMap<DetalleVenta,DetalleVentaDTO>()
            .ForMember(destino => destino.DescripcionProducto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation != null? origen.IdProductoNavigation.NombreProducto : null))
            .ForMember(destino => destino.PrecioTexto,
                opt => opt.MapFrom(origen => origen.Precio != null ? Convert.ToString(origen.Precio != null ? origen.Precio.Value : null, new CultureInfo("es-CO")) : null))
            .ForMember(destino => destino.TotalTexto,
            opt => opt.MapFrom(origen => origen.Total != null ? Convert.ToString(origen.Total != null ? origen.Total.Value : null, new CultureInfo("es-CO")) : null));

            CreateMap<DetalleVentaDTO,DetalleVenta>()
            .ForMember(destino => destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-CO"))))
            .ForMember(destino => destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO"))));
            #endregion DetalleVenta


            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
            .ForMember(destino => destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation != null ? origen.IdVentaNavigation.FechaVenta.Value.ToString("dd/MM/yyyy") : null))
            .ForMember(destino => destino.NumeroDocumento,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation != null ? origen.IdVentaNavigation.NumeroDocumento : null))
            .ForMember(destino => destino.TipoPago,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation != null ? origen.IdVentaNavigation.TipoPago : null))
            .ForMember(destino => destino.TotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation != null? origen.IdVentaNavigation.Total.Value : null, new CultureInfo("es-CO"))))
            .ForMember(destino => destino.Producto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation != null ? origen.IdProductoNavigation.NombreProducto : null))
            .ForMember(destino => destino.Precio, 
                opt => opt.MapFrom(origen => origen.Precio != null ? Convert.ToString(origen.Precio != null ? origen.Precio.Value : null, new CultureInfo("es-CO")) : null))
            .ForMember(destino => destino.Total,
                opt => opt.MapFrom(origen => origen.Total != null ? Convert.ToString(origen.Total != null ? origen.Total.Value : null, new CultureInfo("es-CO")) : null));         

            #endregion Reporte
            

        }

    }
}
