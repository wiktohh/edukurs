using Domain.Entities;
using Domain.ValueObjects.Ticket;

namespace Domain.Repositories;

public interface ITicketRepository
{
    IQueryable<Ticket> GetAllTicketsAsync();
    Task<Ticket> GetTicketByIdAsync(TicketId Id);
    Task AddTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(Ticket ticket);
}