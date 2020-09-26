using System;

namespace Unlakki.Bns.Launcher.Components.Router
{
    public class RouteData
    {
        public Func<RoutedComponent> Component { get; }

        public string Title { get; }

        public RouteData(Func<RoutedComponent> component, string title = null)
        {
            Component = component;
            Title = title;
        }
    }
}
