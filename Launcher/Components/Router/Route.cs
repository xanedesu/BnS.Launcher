using System;

namespace Unlakki.Bns.Launcher.Components.Router
{
    public class Route
    {
        public Func<RoutableComponent> Component { get; }

        public RouteData Data { get; }

        public Route(Func<RoutableComponent> component, RouteData data = null)
        {
            Component = component;
            Data = data;
        }
    }
}
