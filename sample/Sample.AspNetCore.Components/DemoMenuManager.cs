using Bizer;
using Bizer.AspNetCore.Components;
using Bizer.AspNetCore.Components.Abstractions;

namespace Sample.AspNetCore.Components
{
    public class DemoMenuManager : MenuManagerBase
    {
        public override Task<IEnumerable<MenuItem>> GetNavbarMenusAsync()
        {
            var list = new List<MenuItem>()
            {
                new("组件")
                {
                    Items= new MenuItem[]
                    {
                        new("Table","table"),
                        new("DataGrid","datagrid"),
                        new("Modal","modal"),
                        new("DropDown","dropdown")
                    }
                }
            };

            return list.AsEnumerable().ToResultTask();
        }

        public override Task<IEnumerable<MenuItem>> GetSidebarMenusAsync()
        {

            var list = new List<MenuItem>()
            {
                new("Home","/"),
                new("用户管理")
                {
                    Items= new MenuItem[]
                    {
                        new("添加"),
                        new("列表")
                    }
                }
            };

            return list.AsEnumerable().ToResultTask();
        }
    }
}
