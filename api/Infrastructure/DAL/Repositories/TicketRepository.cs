using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Ticket;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly DataContext _context;

    public TicketRepository(DataContext context)
    {
        _context = context;
    }
    
    public IQueryable<Ticket> GetAllTicketsAsync()
    {
        return _context.Tickets.AsQueryable();
    }

    public async Task<Ticket> GetTicketByIdAsync(TicketId Id) => await _context.Tickets.FirstOrDefaultAsync(x => x.Id == Id);

    public async Task AddTicketAsync(Ticket ticket) => await _context.Tickets.AddAsync(ticket);

    public void UpdateTicketAsync(Ticket ticket)
    {
         _context.Tickets.Update(ticket);
    }

    public void DeleteTicketAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
    }
}