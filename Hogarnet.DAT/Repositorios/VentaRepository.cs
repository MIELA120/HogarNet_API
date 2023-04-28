using Hogarnet.DAT.DBContext;
using Hogarnet.DAT.Repositorios.Contrato;
using Hogarnet.MOD;

namespace Hogarnet.DAT.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbhognetContext _dbcontext;

        public VentaRepository(DbhognetContext dbcontext): base(dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var trasaction = _dbcontext.Database.BeginTransaction()) //Metodo para generar un atransaccion 
            {
                try
                {
                    foreach (DetalleVenta element in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == element.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - element.Cantidad;
                        _dbcontext.Productos.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First(); 

                    correlativo.UltiNumero = correlativo.UltiNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();
                    
                    //Formato
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltiNumero.ToString();
                    
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    trasaction.Commit();
                }

                catch
                {
                    trasaction.Rollback();
                    throw;
                }

                return ventaGenerada;


            }
        }
    }
}
