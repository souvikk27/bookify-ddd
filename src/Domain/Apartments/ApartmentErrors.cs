﻿using Domain.Abstractions;

namespace Domain.Apartments;

public static class ApartmentErrors
{
	public static Error NotFound = new(
		"Apartment.NotFound",
		"The apartment with the specified identifier was not found");
}
