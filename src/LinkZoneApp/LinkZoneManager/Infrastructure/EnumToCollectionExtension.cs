using System;
using System.Linq;
using Avalonia.Markup.Xaml;

namespace LinkZoneManager.Infrastructure;

// https://stackoverflow.com/a/3986342/8231560
public sealed class EnumToCollectionExtension : MarkupExtension
{
    public Type EnumType { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (EnumType == null) throw new ArgumentNullException(nameof(EnumType));

        return Enum.GetValues(EnumType).Cast<Enum>();
    }
}