using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace DesafioIntelltech
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Formatters.Remove(config.Formatters.XmlFormatter); //Remove o formatador antigo
			//Cria o novo
			JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
			jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
			jsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
		}
	}
}
