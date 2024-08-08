using Domain.Shared;

namespace Domain.Bookings;

public record PricingDetails(
	Money PricForPeriod,
	Money CleaningFee,
	Money AmenitiesUpCharge,
	Money TotalPrice);