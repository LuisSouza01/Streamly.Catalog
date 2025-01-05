using Bogus;

namespace Streamly.Catalog.UnitTests.Common;

public class BaseFixture
{
    public Faker Faker { get; set; } = new ();
}