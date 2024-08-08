using Domain.Abstractions;
using Domain.Apartments;
using Domain.Bookings.Events;
using Domain.Shared;

namespace Domain.Bookings;

public sealed class Booking : Entity
{
	private Booking(Guid id,
		Guid apartmentId,
		Guid userId,
		DateRange duration,
		Money priceForPeriod,
		Money cleaningFee,
		Money amenitiesUpCharge,
		Money totalPrice,
		BookingStatus status,
		DateTime createdOnUtc) : base(id)
	{
		ApartmentId = apartmentId;
		UserId = userId;
		Duration = duration;
		PriceForPeriod = priceForPeriod;
		CleaningFee = cleaningFee;
		AmenitiesUpCharge = amenitiesUpCharge;
		TotalPrice = totalPrice;
		Status = status;
		CreatedOnUtc = createdOnUtc;
	}

	public Guid ApartmentId { get; private set; }

	public Guid UserId { get; private set; }

	public DateRange Duration { get; private set; }

	public Money PriceForPeriod { get; private set; } = null!;

	public Money CleaningFee { get; private set; } = null!;

	public Money AmenitiesUpCharge { get; private set; } = null!;

	public Money TotalPrice { get; private set; } = null!;

	public BookingStatus Status { get; private set; }

	public DateTime CreatedOnUtc { get; private set; }

	public DateTime? ConfirmedOnUtc { get; private set; }

	public DateTime? RejectedOnUtc { get; private set; }

	public DateTime? CompletedOnUtc { get; private set; }

	public DateTime? CancelledOnUtc { get; private set; }

	public static Booking Reserve(Apartment apartment,
		Guid userId,
		DateRange duration,
		DateTime utcNow,
		PricingService pricingService)
	{
		var pricingDetails = pricingService.CalculatePricing(apartment, duration);

		var booking = new Booking(Guid.NewGuid(),
			apartment.Id,
			userId,
			duration,
			pricingDetails.PricForPeriod,
			pricingDetails.CleaningFee,
			pricingDetails.AmenitiesUpCharge,
			pricingDetails.TotalPrice,
			BookingStatus.Reserved,
			utcNow);

		booking.RaiseDomainEvents(new BookingReservedDomainEvent(booking.Id));

		apartment.LastbookedOnUtc = utcNow;

		return booking;
	}

	public Result Confirm(DateTime utcNow)
	{
		if (Status != BookingStatus.Reserved)
		{
			return Result.Failure(BookingErrors.NotReserved);
		}

		Status = BookingStatus.Confirmed;
		ConfirmedOnUtc = utcNow;

		RaiseDomainEvents(new BookingConfirmedDomainEvent(Id));

		return Result.Success();
	}

	public Result Reject(DateTime utcNow)
	{
		if (Status != BookingStatus.Reserved)
		{
			return Result.Failure(BookingErrors.NotReserved);
		}

		Status = BookingStatus.Rejected;
		RejectedOnUtc = utcNow;

		RaiseDomainEvents(new BookingRejectedDomainEvent(Id));

		return Result.Success();
	}

	public Result Complete(DateTime utcNow)
	{
		if (Status != BookingStatus.Confirmed)
		{
			return Result.Failure(BookingErrors.NotConfirmed);
		}

		Status = BookingStatus.Completed;
		CompletedOnUtc = utcNow;

		RaiseDomainEvents(new BookingCompletedDomainEvent(Id));

		return Result.Success();
	}

	public Result Cancel(DateTime utcNow)
	{
		if (Status != BookingStatus.Confirmed)
		{
			return Result.Failure(BookingErrors.NotConfirmed);
		}

		var currentDate = DateOnly.FromDateTime(utcNow);

		if (currentDate > Duration.Start)
		{
			return Result.Failure(BookingErrors.AlreadyStarted);
		}

		Status = BookingStatus.Cancelled;
		CancelledOnUtc = utcNow;

		RaiseDomainEvents(new BookingCancelledDomainEvent(Id));

		return Result.Success();
	}
}