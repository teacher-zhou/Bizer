using Bizer.Test;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

using System.ComponentModel.DataAnnotations;

namespace Bizer.Localization.Test;
public class TestLocalizeService : TestBase
{
    private readonly IStringLocalizer<TestLocalizeService> _localizeService;

    public TestLocalizeService()
    {
        _localizeService = GetService<IStringLocalizer<TestLocalizeService>>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBizer().AddJsonLocalization();
    }

    [Theory]
    [InlineData(new object[] { "Name", "Name" })]
    [InlineData(new object[] { "Age", "Age" })]
    public void Test_Get_Localize(string key, string value)
    {
        var local = _localizeService[key];
        Assert.Equal(value, local);
    }

    //[Theory]
    //[InlineData(new object[] {"en-us", "Name", "Name" })]
    //[InlineData(new object[] { "en-us", "Age", "Age" })]
    //[InlineData(new object[] { "zh-cn", "Name", "–’√˚" })]
    //[InlineData(new object[] { "zh-cn", "Age", "ƒÍ¡‰" })]
    //public void Test_Change_Localize(string culture, string key, string value)
    //{
    //    _localizeService.Change(culture);
    //    var local = _localizeService.Get(key);
    //    Assert.Equal(value, local);
    //}

    [Fact]
    public void Test_Localize_For_DataAnnoation()
    {
        var model = new User
        {
            Age = 1
        };
        var context = new ValidationContext(model);
        System.ComponentModel.DataAnnotations.Validator.ValidateObject(model, context);
    }

    public class User
    {
        [Display(Name = "Name")]
        [Required()]
        public string Name { get; set; }
        [Range(20, 30, ErrorMessageResourceName = "AgeError")]
        public int Age { get; set; }
    }
}