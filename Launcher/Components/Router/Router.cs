using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace Unlakki.Bns.Launcher.Components.Router
{
  public class Router
  {
    private readonly ContainerControl _rootContainer;

    private readonly Dictionary<Regex, Route> _routes;

    private string _location;

    public NameValueCollection Query = new NameValueCollection();

    public NameValueCollection Params = new NameValueCollection();

    public Router(ContainerControl rootContainer, string initLocation = "/")
    {
      _rootContainer = rootContainer;
      _routes = new Dictionary<Regex, Route>();

      SetLocation(initLocation);
    }

    public void AddRoute(string path, Func<RoutableComponent> component, RouteData data = null)
    {
      _routes.Add(CompileRoutePathToRegex(path), new Route(component, data));
      OnLocationChangedOrNewRouteAdded();
    }

    public void SetLocation(string location)
    {
      var locationAndQuery = location.Split('?');

      _location = locationAndQuery[0];
      SetQuery(locationAndQuery.ElementAtOrDefault(1));

      OnLocationChangedOrNewRouteAdded();
    }

    private void OnLocationChangedOrNewRouteAdded()
    {
      var route = GetCurrentRoute();
      if (route != null)
      {
        if (route.Data?.Title != null)
        {
          _rootContainer.Text = route.Data.Title;
        }

        var component = route.Component();
        component.Connect(this);

        _rootContainer.Controls.Clear();
        _rootContainer.Controls.Add(component);
      }
    }

    private Route GetCurrentRoute()
    {
      try
      {
        var route = _routes.Single((routeData) => routeData.Key.IsMatch(_location));

        SetParams(route.Key.Match(_location).Groups.Cast<Group>().Select(
                (group) => new KeyValuePair<string, string>(group.Name, group.Value)));

        return route.Value;
      }
      catch (Exception)
      {
        return null;
      }
    }

    private void SetQuery(string query)
    {
      Query.Clear();

      if (query != null)
      {
        Query.Add(HttpUtility.ParseQueryString(query));
      }
    }

    private void SetParams(IEnumerable<KeyValuePair<string, string>> paramsCollection)
    {
      Params.Clear();

      foreach (var keyValue in paramsCollection)
      {
        Params.Add(keyValue.Key, keyValue.Value);
      }
    }

    private Regex CompileRoutePathToRegex(string path)
    {
      var r1 = Regex.Replace(path, "/?\\*", "/.*");
      var r2 = Regex.Replace(r1, ":([a-z]+)", "(?<$1>[^/]+)", RegexOptions.IgnoreCase);

      return new Regex(string.Format("^{0}$", r2));
    }
  }
}
