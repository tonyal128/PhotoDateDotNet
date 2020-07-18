using System;
using Xunit;

public class Photo_UnitTests
{
    [Fact]
    public void Ctor_NullTitle_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Photo(1, 1, null, "test", "test"));
    }

    [Fact]
    public void Ctor_NullUrl_ThrowsArgumentNullException()
    {

    }

    [Fact]
    public void Ctor_NullThumbnailUrl_ThrowsArgumentNullException()
    {

    }
}