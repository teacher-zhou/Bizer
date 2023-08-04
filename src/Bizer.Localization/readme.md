## Bizer.Localization
使用 JSON 文件作为本地化资源。

JSON 文件格式:
```json
{
    "Culture": "en-us",
    "Values" :{
        "Name": "The Name",
        "Age": "User Age",
        ...
    }  
}
```
添加本地化服务
```cs
services.AddBizer().AddLocaliztion();
```

使用 `Microsoft.Extensions.Localizations.IStringLocalizer` 接口调用
```cs
public class MyClass
{
    public MyClass(IStringLocalizer locale)
    {
        Locale = locale;
    }
    public void Run()
    {
      var name = Locale["Name"];
    }
}
```