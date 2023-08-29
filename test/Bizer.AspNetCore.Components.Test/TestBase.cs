using Microsoft.Extensions.DependencyInjection;

namespace Bizer.AspNetCore.Components;

public abstract class TestBase
{
    private readonly TestContext _context;
    public TestBase()
    {
        _context = new TestContext();
        _context.Services.AddBizer().AddComponents();

    }

    public TestContext Context => _context;
}
