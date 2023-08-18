using Bizer.AspNetCore.Components.Abstractions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bizer.AspNetCore.Components;

public class BizerComponentBuilder
{
    internal BizerComponentBuilder(BizerBuilder builder)
    {
        Builder = builder;
    }

    internal BizerBuilder Builder { get; }

    public BizerComponentBuilder AddMenuManager<TMenuManager>() where TMenuManager : class, IMenuManager
    {
        Builder.Services.AddSingleton<IMenuManager, TMenuManager>();
        return this;
    }

    internal BizerComponentBuilder AddDefaultMenuManager() => AddMenuManager<DefaultMenuManager>();
}
