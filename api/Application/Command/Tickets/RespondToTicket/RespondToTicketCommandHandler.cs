using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Command.Tickets.RespondToTicket;

public class RespondToTicketCommandHandler : IRequestHandler<RespondToTicketCommand>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepRepository _repRepository;
    private readonly IAccountRepository _userRepository;

    public RespondToTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, IRepRepository repRepository, IAccountRepository userRepository)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _repRepository = repRepository;
        _userRepository = userRepository;
    }
    
    public async Task Handle(RespondToTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(request.TicketId);
        if (ticket is null)
        {
            throw new NotFoundException("Ticket not found");
        }

        if (ticket.Status != "Pending")
        {
            throw new UserTicketException("Ticket is already closed");
        }
        
        var repository = await _repRepository.GetRepositoryByIdAsync(ticket.RepositoryId);
        if (repository is null)
        {
            throw new NotFoundException("Repository not found");
        }

        if (repository.OwnerId != request.UserId)
        {
            throw new UnauthorazedException("User is not the owner of the repository");
        }
        var user = await _userRepository.GetByIdAsync(ticket.UserId);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        ticket.Status = request.Status;
        _ticketRepository.UpdateTicketAsync(ticket);
        if(ticket.Status == "Approved")
        {
          await _repRepository.AddUserToRepositoryAsync(new UserRepository(repository.Id,user.Id,false));
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
    }
}