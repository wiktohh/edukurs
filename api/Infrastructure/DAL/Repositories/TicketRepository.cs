using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Ticket;

namespace Infrastructure.DAL.Repositories;

public class TicketRepository : ITicketRepository
{
    public IQueryable<Ticket> GetAllTicketsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> GetTicketByIdAsync(TicketId Id)
    {
        throw new NotImplementedException();
    }

    public Task AddTicketAsync(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTicketAsync(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTicketAsync(Ticket ticket)
    {
        throw new NotImplementedException();
    }
}