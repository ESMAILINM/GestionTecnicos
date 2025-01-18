using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class TecnicosService(IDbContextFactory<Contexto> DbFactory)
    {
        //Metodo para evitar que existan dos tecnicos con el mismo nombre
        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .AnyAsync(T => T.TecnicoId == id); //Verifica si ya existe un tecnico con ese nombre
        }
        public async Task<bool> ExisteNombre(string nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .AnyAsync(T => T.Nombres == nombre); //Verifica si ya existe un tecnico con ese nombre
        }

        //Guardar tecnico 
        public async Task<bool> Guardar(Tecnicos tecnico)
        {

            if (!await Existe(tecnico.TecnicoId))
            {
                return await Insertar(tecnico);
            }
            else
            {
                return await Modificar(tecnico);
            }
        }

        //Insertar tecnico
        public async Task<bool> Insertar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tecnicos.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        //Eliminar tecnico 
        public async Task<bool> Eliminar(int tecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .Where(t => t.TecnicoId == tecnicoId)
                .ExecuteDeleteAsync() > 0;
        }

        //Listar
        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var Contexto = await DbFactory.CreateDbContextAsync();
            return await Contexto.Tecnicos
                .Where(criterio)
                .ToListAsync();
        }

        //Buscar
        public async Task<Tecnicos?> BuscarNombre(string nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .FirstOrDefaultAsync(n => n.Nombres == nombre);
        }
        public async Task<Tecnicos?> Buscar(int Id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            Tecnicos tecnicos = await contexto.Tecnicos
                .FirstOrDefaultAsync(t => t.TecnicoId == Id);
            return tecnicos;
        }

        //Editar
        public async Task<bool> Modificar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }
  }  }

