using Domain.Abstractions;
using Domain.Shared;

// ReSharper disable ConvertToPrimaryConstructor
namespace Domain.Apartments
{
    public class Apartment : Entity
    {
        public Apartment(Guid id, Name name, Description description, Address address, Money price,
            Money cleaningFee, DateTime lastbookedOnUtc, List<Amenity> amenities) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            LastbookedOnUtc = lastbookedOnUtc;
            Amenities = amenities;
        }

        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public Address Address { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public DateTime LastbookedOnUtc { get; internal set; }
        public List<Amenity> Amenities { get; private set; } = new();
    }
}