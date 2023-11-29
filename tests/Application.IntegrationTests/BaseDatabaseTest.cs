namespace Application.IntegrationTests;

[Collection(nameof(DatabaseCollection))]
public class BaseDatabaseTest : IAsyncLifetime
{
    protected readonly DatabaseFixture _fixture;

    protected BaseDatabaseTest(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Reset state before each test.
    /// </summary>
    /// <returns></returns>
    public Task InitializeAsync() => _fixture.ResetState();

    public Task DisposeAsync() => Task.CompletedTask;
}