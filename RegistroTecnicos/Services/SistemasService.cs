using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class SistemasService(IDbContextFactory<Contexto> DbFactory)
    {
        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="sistemas"></param>
        /// <returns></returns>
        public async Task<bool> Guardar(Sistemas sistemas) {
            if (!await Existe(sistemas.SistemaId))
            {
                return await Insertar(sistemas);
            }
            else
            {
                return await Modificar(sistemas);
            }

        }
        /// <summary>
        /// Existe  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Existe(int id) {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas
               .AnyAsync(s => s.SistemaId == id);
        }
        /// <summary>
        /// Insertar
        /// </summary>
        /// <param name="sistemas"></param>
        /// <returns></returns>
        public async Task<bool> Insertar(Sistemas sistemas)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Sistemas.Add(sistemas);
            return await contexto.SaveChangesAsync() > 0;

        }
        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="sistemas"></param>
        /// <returns></returns>
        public async Task<bool> Modificar(Sistemas sistemas)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(sistemas);
            return await contexto
               .SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Buscar 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Sistemas?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas
               .FirstOrDefaultAsync(s => s.SistemaId == id);
        }
        /// <summary>
        /// Eliminar 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas
              .Where(s => s.SistemaId == id)
              .ExecuteDeleteAsync() > 0;

        }
        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="criterio"></param>
        /// <returns></returns>
        public async Task<List<Sistemas>> Listar (Expression<Func<Sistemas, bool>> criterio)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas
               .Where(criterio)
               .ToListAsync();
        }
       

    }
}
