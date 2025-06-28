namespace Project1.Application.FunctionalTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public Task TestSetUp()
    {
        return Task.CompletedTask;
    }
}
