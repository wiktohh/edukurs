using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using Domain.ValueObjects.Ticket;
using Domain.ValueObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Command.Tickets.SendTIcketCommand;

public class SendTicketCommandHandler : IRequestHandler<SendTicketCommand>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepRepository _repRepository;

    public SendTicketCommandHandler(ITicketRepository ticketRepository,IUnitOfWork unitOfWork, IRepRepository repRepository)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _repRepository = repRepository;
    }
    
    public async Task Handle(SendTicketCommand request, CancellationToken cancellationToken)
    {
        var repository = await _repRepository.GetRepositoryByIdAsync(request.RepositoryId);
        var tickets = await _ticketRepository.GetAllTicketsAsync().Where(x=>x.UserId== new UserId(request.UserId)).ToListAsync();
        var ti2 =tickets.Where(
            x => x.UserId == new UserId(request.UserId) && x.RepositoryId == new RepositoryId( request.RepositoryId) && x.Status == "Pending");
        if (ti2.Any())
        {
            throw new UserTicketException("User already has a ticket for this repository");
        }
        if (repository is null)
        {
            throw new NotFoundException("Repository not found");
        }
        if (repository.OwnerId == request.UserId)
        {
            throw new UserTicketException("You are the owner of this repository");
        }

        if (repository.Users.Any(x => x.UserId == new UserId(request.UserId)))
        {
            throw new UserTicketException("User already in repository");
        }
        
        var ticket = new Ticket()
        {
            Id = request.Id,
            RepositoryId = request.RepositoryId,
            UserId = request.UserId,
            Status = Status.Pending
        };
        await _ticketRepository.AddTicketAsync(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}