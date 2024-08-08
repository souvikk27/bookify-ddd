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
}