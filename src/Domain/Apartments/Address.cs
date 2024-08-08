namespace Domain.Apartments
{
	public record Address(
		string Country,
		string City,
		string ZipCode,
		string State,
		string Street);
}