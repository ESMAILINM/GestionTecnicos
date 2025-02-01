using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class TicketsService (IDbContextFactory<Contexto> DbFactory)
    {    //Existe
        public async Task<bool> Existe (int Id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .AnyAsync (t=>t.TicketId == Id);
        }
        //Guardar
        public async Task<bool> Guardar (Tickets ticket)
        {
            if (!await Existe(ticket.TicketId))
            {
                return await Insertar(ticket);
            }
            else
            {
                return await Modificar(ticket);
            }
        }
        //Insertar
        public async Task<bool> Insertar (Tickets ticket)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            contexto.Tickets.Add(ticket);
            return await contexto.SaveChangesAsync() > 0;

        }
        //Buscar
        public async Task<Tickets?>Buscar(int ticketId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .FirstOrDefaultAsync(t=>t.TicketId== ticketId);
        }
        //Listar
        public async Task<List<Tickets>> Listar (Expression<Func<Tickets, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .Where(criterio)
                .ToListAsync();
        }
        //Modificar
        public async Task<bool> Modificar(Tickets ticket)
        {
            await using var contexto=await DbFactory.CreateDbContextAsync();
            contexto.Update(ticket);
            return await contexto
                .SaveChangesAsync() > 0;
        }
        
        //Eliminar
        public async Task<bool> Eliminar(int  ticketId)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .AsNoTracking()
                .Where(T => T.TicketId == ticketId)
                .ExecuteDeleteAsync() > 0;
        }
    }
}
