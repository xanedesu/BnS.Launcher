using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Unlakki.Bns.Launcher.Components.Router
{
    public class Router
    {
        private readonly ContainerControl _root;

        private readonly Dictionary<string, RouteData> _routes;

        private string _location;

        public Dictionary<string, string> QueryParams = new Dictionary<string, string>();

        public Router(ContainerControl root, string initLocation = "/")
        {
            _root = root;
            _routes = new Dictionary<string, RouteData>();
            SetLocation(initLocation);
        }

        public void AddRoute(string path, Func<RoutedComponent> component, string title = null)
        {
            _routes.Add(path, new RouteData(component, title));
            OnLocationChangedOrNewRouteAdded();
        }

        public void SetLocation(string location)
        {
            var locationAndQuery = location.Split('?');

            _location = locationAndQuery[0];

            QueryParams.Clear();
            if (locationAndQuery.Length == 2)
            {
                foreach (string queryParam in locationAndQuery[1].Split('&'))
                {
                    var nameAndValue = queryParam.Split('=');
                    QueryParams.Add(nameAndValue[0], nameAndValue[1] ?? null);
                }
            }

            OnLocationChangedOrNewRouteAdded();
        }

        private void OnLocationChangedOrNewRouteAdded()
        {
            RouteData routeData;
            if (_routes.TryGetValue(_location, out routeData))
            {
                if (routeData.Title != null)
                {
                    _root.Text = routeData.Title;
                }

                var component = routeData.Component();
                component.Router = this;

                _root.Controls.Clear();
                _root.Controls.Add(component);
            }
        }
    }
}
