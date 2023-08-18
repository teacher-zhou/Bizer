using Bizer;
using Bizer.AspNetCore.Components;
using Bizer.AspNetCore.Components.Abstractions;

using Blazorise;

namespace Sample.AspNetCore.Components
{
    public class DemoMenuManager : MenuManagerBase
    {
        public override Task<IEnumerable<MenuItem>> GetNavbarMenusAsync()
        {
            var list = new List<MenuItem>()
            {
                new("Home","/", IconName.Home),
                new("Test")
                {
                    Items= new MenuItem[]
                    {
                        new("Sub", icon: IconName.Subscript)
                    }
                }
            };

            return list.AsEnumerable().ToResultTask();
        }
    }
}
