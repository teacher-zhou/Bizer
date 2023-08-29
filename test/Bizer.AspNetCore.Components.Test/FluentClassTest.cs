namespace Bizer.AspNetCore.Components.Test;

public class FluentClassTest:TestBase
{
    [Fact]
    public void TestMargin()
    {

        Class.Margin.None.Create().JoinAsString(" ").Should().Be("m-0");
        Class.Margin.Is1.Create().JoinAsString(" ").Should().Be("m-1");
        Class.Margin.Is2.Create().JoinAsString(" ").Should().Be("m-2");
        Class.Margin.Is3.Create().JoinAsString(" ").Should().Be("m-3");
        Class.Margin.Is4.Create().JoinAsString(" ").Should().Be("m-4");
        Class.Margin.Is5.Create().JoinAsString(" ").Should().Be("m-5");
        Class.Margin.Auto.Create().JoinAsString(" ").Should().Be("m-auto");
    }
    [Fact]
    public void TestMargin_WithSide()
    {
        //Class.Margin.None.FromTop.Create().JoinAsString(" ").Should().Be("mt-0");
        Class.Margin.Is1.FromBottom.Create().JoinAsString(" ").Should().Be("mb-1");
        //Class.Margin.Is2.FromEnd.Create().JoinAsString(" ").Should().Be("me-2");
        //Class.Margin.Is3.FromStart.Create().JoinAsString(" ").Should().Be("ms-3");
        //Class.Margin.Is4.FromX.Create().JoinAsString(" ").Should().Be("mx-4");
        //Class.Margin.Is5.FromY.Create().JoinAsString(" ").Should().Be("my-5");
    }
}