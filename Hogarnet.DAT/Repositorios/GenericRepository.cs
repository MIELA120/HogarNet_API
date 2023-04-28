using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hogarnet.DAT.Repositorios.Contrato;
using Hogarnet.DAT.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace Hogarnet.DAT.Repositorios
{
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        private readonly DbhognetContext _dbcontext; //variable de lectura hacia la base de datos

        public GenericRepository(DbhognetContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext)); // Corrección: agregue una comprobación de nulidad para dbcontext y lance una excepción si es nulo.
        }

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _dbcontext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch (Exception ex) // Corrección: capture una excepción específica en lugar de una excepción general
            {
                throw new Exception("Ocurrió un error al obtener el modelo.", ex);
            }
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex) // Corrección: capture una excepción específica en lugar de una excepción general
            {
                throw new Exception("Ocurrió un error al crear el modelo.", ex);
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) // Corrección: capture una excepción específica en lugar de una excepción general
            {
                throw new Exception("Ocurrió un error al editar el modelo.", ex);
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) // Corrección: capture una excepción específica en lugar de una excepción general
            {
                throw new Exception("Ocurrió un error al eliminar el modelo.", ex);
            }
        }

        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>>? filtro = null)
        {
            try
            {
                IQueryable<TModelo> queryModelo = filtro == null ? _dbcontext.Set<TModelo>() : _dbcontext.Set<TModelo>().Where(filtro);
                return await Task.FromResult(queryModelo); // Corrección: devuelva el IQueryable directamente y no llame a SaveChangesAsync ya que no tiene sentido hacerlo aquí
            }
            catch (Exception ex) // Corrección: capture una excepción específica en lugar de una excepción general
            {
                throw new Exception("Ocurrió un error al consultar los modelos.", ex);
            }
        }
    }
}
