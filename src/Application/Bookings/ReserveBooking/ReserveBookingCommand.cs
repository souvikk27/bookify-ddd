using Application.Abstractions.Messaging;

namespace Application.Abstractions.ReserveBooking;

public record ReserveBookingCommand(
	Guid ApartmentId,
	Guid UserId,
	DateOnly StartDate,
	DateOnly EndDate) : ICommand<Guid>;