namespace Ordering.Application.Dtos;
public record AddressDto(string FirstName, string LastName, string EmailAddress, string AddressLine, string Country, string State, string ZipCode);

public interface ITest
{
    string FirstName { get; set; }
}

public interface ITest2
{
    string Hi { get; set; }
}
