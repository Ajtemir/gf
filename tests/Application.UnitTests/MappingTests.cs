using Application.Common.MappingProfiles;
using AutoMapper;

namespace Application.UnitTests;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<CandidateProfile>();
            config.AddProfile<CandidateWithoutImageProfile>();
            config.AddProfile<DictionaryProfile>();
            config.AddProfile<OfficeProfile>();
            config.AddProfile<RewardProfile>();
            config.AddProfile<RoleProfile>();
            config.AddProfile<UserProfile>();
        });
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }
}