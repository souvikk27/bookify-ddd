using Application.Abstractions.Data;
using Bogus;
using Dapper;
using Domain.Apartments;

namespace Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        var now = DateTime.UtcNow;

        List<object> apartments = new();
        for (var i = 0; i < 100; i++)
        {
            var id = Guid.NewGuid();
            apartments.Add(new
            {
                Id = id,
                Name = faker.Company.CompanyName(),
                Description = "Amazing view",
                Country = faker.Address.Country(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode(),
                City = faker.Address.City(),
                Street = faker.Address.StreetAddress(),
                PriceAmount = faker.Random.Decimal(50, 1000),
                PriceCurrency = "USD",
                CleaningFeeAmount = faker.Random.Decimal(25, 200),
                CleaningFeeCurrency = "USD",
                Amenities = new List<int> { (int)Amenity.Parking, (int)Amenity.MountainView },
                LastBookedOn = DateTime.MinValue,
                ReferenceId = id.ToString(),  // Setting ReferenceId
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        const string sql = """
                           INSERT INTO public.apartments
                           (id, "name", description, address_country, address_state, address_zip_code, address_city, address_street, price_amount, price_currency, cleaning_fee_amount, cleaning_fee_currency, amenities, last_booked_on_utc, reference_id, created_at, updated_at)
                           VALUES(@Id, @Name, @Description, @Country, @State, @ZipCode, @City, @Street, @PriceAmount, @PriceCurrency, @CleaningFeeAmount, @CleaningFeeCurrency, @Amenities, @LastBookedOn, @ReferenceId, @CreatedAt, @UpdatedAt);
                           """;


        connection.Execute(sql, apartments);
    }
}
