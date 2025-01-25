using System.Linq.Expressions;
using global::RegistroTecnicos.DAL;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class ClientesService(IDbContextFactory<Contexto> DbFactory)
    {
        //Metodo Existe
        public async Task<bool> Existe(int Id, string nombre)
        {
            await using var contexto =await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AnyAsync(C => (C.ClienteId != Id && C.Nombres == nombre));
        }
        //Metodo Insertar
        public async Task<bool> Insertar (Clientes cliente)
        {
            await using var contexto=await DbFactory.CreateDbContextAsync();
            contexto.Clientes.Add(cliente);
            return await contexto.SaveChangesAsync()  > 0;
        }
        //Metodo Modificar
        public async Task<bool> Modificar(Clientes clientes)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(clientes);
            return await contexto
                .SaveChangesAsync() > 0;
        }
        //Metodo Guardar
        public async Task<bool> Guardar(Clientes Cliente)
        {
            if (await Existe(Cliente.ClienteId, Cliente.Nombres))
            {
                return await Insertar(Cliente);
            }
            else
            {
                return await Modificar(Cliente);
            }
        }


        //Metodo Eliminar
         public async Task<bool>Eliminar(int clienteId)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .Where(c => c.ClienteId == clienteId)
                .ExecuteDeleteAsync() > 0;
        }
        //Metdodo Buscar

        public async Task <Clientes?> Buscar (int clienteId)
        {
            await using var Contexto=await DbFactory.CreateDbContextAsync();
            return await Contexto.Clientes
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }
        public async Task <Clientes?> BuscarNombre (string nombres)
        {
            await using var Contexto=await DbFactory.CreateDbContextAsync();
            return await Contexto.Clientes
                .FirstOrDefaultAsync(c => c.Nombres == nombres);
        }

        public async Task<Clientes?> rnc(string rnc)
        {
            await using var Contexto = await DbFactory.CreateDbContextAsync();
            return await Contexto.Clientes
                .FirstOrDefaultAsync(c => c.Rnc == rnc);

        }
        public async Task<bool> ExisteRnc(int Id, string rnc)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AnyAsync(C => (C.ClienteId != Id && C.Rnc == rnc));
        }
        //Metodo Listar
        public async Task <List<Clientes>> Listar ( Expression<Func<Clientes, bool>>criterio)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .Where(criterio)
                .ToListAsync();
        }
        }

    } 
      

