using Domain.Security;
using Xunit;

namespace SigaUcc.UnitTests;

public sealed class PermissionCodesTests
{
    [Fact]
    public void Catalog_contains_unique_nonempty_codes_and_descriptions()
    {
        Assert.NotEmpty(PermissionCodes.All);
        Assert.Equal(PermissionCodes.All.Count, PermissionCodes.All.Keys.Distinct(StringComparer.Ordinal).Count());
        Assert.All(PermissionCodes.All, item =>
        {
            Assert.False(string.IsNullOrWhiteSpace(item.Key));
            Assert.False(string.IsNullOrWhiteSpace(item.Value));
        });
    }
}
