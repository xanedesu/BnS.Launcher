using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Unlakki.Bns.Launcher.Components.Router
{
    public class Router
    {
        private static readonly Uri BaseUri = new Uri("app://bnslauncher");

        private readonly ContainerControl _rootComponent;

        private readonly Dictionary<UriTemplate, Route> _routes;

        public NameValueCollection Query { get; private set; }

        public NameValueCollection Params { get; private set; }

        private string _location;

        public Router(ContainerControl rootComponent, string initLocation = "/")
        {
            _rootComponent = rootComponent;
            _routes = new Dictionary<UriTemplate, Route>();

            Query = new NameValueCollection();
            Params = new NameValueCollection();

            SetLocation(initLocation);
        }

        public void AddRoute(string path, Func<RoutableComponent> component, RouteData data = null)
        {
            _routes.Add(new UriTemplate(path), new Route(component, data));
            OnLocationChangedOrNewRouteAdded();
        }

        public void SetLocation(string location)
        {
            _location = location;
            OnLocationChangedOrNewRouteAdded();
        }

        private void OnLocationChangedOrNewRouteAdded()
        {
            var route = GetCurrentRoute();
            if (route != null)
            {
                if (route.Data?.Title != null)
                {
                    _rootComponent.Text = route.Data.Title;
                }

                var component = route.Component();
                component.Connect(this);

                _rootComponent.Controls.Clear();
                _rootComponent.Controls.Add(component);
            }
        }

        private Route GetCurrentRoute()
        {
            var currentLocation = new Uri(BaseUri, _location);

            foreach (var route in _routes)
            {
                var matches = route.Key.Match(BaseUri, currentLocation);
                if (matches != null)
                {
                    Query.Clear();
                    Query.Add(matches.QueryParameters);

                    Params.Clear();
                    Params.Add(matches.BoundVariables);

                    return route.Value;
                }
            }

            return null;
        }
    }
}
