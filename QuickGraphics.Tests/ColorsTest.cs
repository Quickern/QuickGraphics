namespace QuickGraphics.Tests;

public class ColorsTest
{
    [Fact]
    public void ParseName() => Assert.Equal(Color.Parse("red"), Colors.Red);

    [Fact]
    public void ParseRGBA() => Assert.Equal(Color.Parse("#00FF00FF"), Colors.Lime);

    [Fact]
    public void ParseRGB() => Assert.Equal(Color.Parse("#0000FF"), Colors.Blue);

    [Fact]
    public void ParseShort() => Assert.Equal(Color.Parse("#A1B"), new Color(0xAA11BBFF));
}
