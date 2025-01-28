using RegistroTecnicos.Models;
using global::RegistroTecnicos.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;



namespace RegistroTecnicos.Services
{
    public class CiudadesService(IDbContextFactory<Contexto> DbFactory)
    {

        //1.Metodo Existe
        public async Task<bool> Existe(int Id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades
                .AnyAsync(T => T.CiudadId == Id);
        }
        //2.Metodo Modificar
        public async Task<bool> Modificar(Ciudades ciudades)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(ciudades);
            return await contexto
                .SaveChangesAsync() > 0;
        }
        //3.Insertar
        public async Task<bool> Insertar(Ciudades ciudades)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Ciudades.Add(ciudades);
            return await contexto.SaveChangesAsync() > 0;
        }
        //4.Metodo Guardar
        public async Task<bool> Guardar(Ciudades ciudades)
        {

            if (!await Existe(ciudades.CiudadId))
            {
                return await Insertar(ciudades);
            }
            else
            {
                return await Modificar(ciudades);
            }
        }
        //5.Eliminar

        public async Task<bool>Eliminar (int ciudadId)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades
                .Where(c => c.CiudadId == ciudadId)
                .ExecuteDeleteAsync() > 0;
        }
        //6.Metodo Listar
        public async Task<List<Ciudades>> Listar (Expression<Func<Ciudades, bool>> criterio)
        {
            await using var contexto=await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades
                .Where(criterio)
                .ToListAsync();
        }
        //7.Metodo Buscar
        public async Task<Ciudades?> BuscarNombre (string nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades
                .FirstOrDefaultAsync(c=>c.Nombre==nombre);
           
        }

        
        public async Task<Ciudades?> BuscarId(int Id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            Ciudades ciudades = await contexto.Ciudades
                .FirstOrDefaultAsync(c => c.CiudadId == Id);
            return ciudades;
        }
    }
}
