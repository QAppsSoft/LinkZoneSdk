using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using JetBrains.Annotations;
using LinkZoneManager.ViewModels;
using LinkZoneManager.ViewModels.Interfaces;

namespace LinkZoneManager
{
    public class ViewLocator : IDataTemplate
    {
        [CanBeNull]
        public IControl Build([CanBeNull] object data)
        {
            if (data == null)
            {
                return null;
            }

            var name = data.GetType().FullName;
            return GetViewControl(name);
        }

        public bool Match(object data)
        {
            return data is IViewModel;
        }

        private static Control GetViewControl(string name)
        {
            var control = CreateInstanceFor("View", name);

            if (control != null)
            {
                return control;
            }

            control = CreateInstanceFor("Page", name);

            return control ?? new TextBlock { Text = name };
        }

        private static Control? CreateInstanceFor(string viewTrailingName, string name)
        {
            var getViewName = name.Replace("ViewModel", viewTrailingName);

            var type = Type.GetType(getViewName);

            return type == null ? null : (Control)Activator.CreateInstance(type)!;
        }
    }
}