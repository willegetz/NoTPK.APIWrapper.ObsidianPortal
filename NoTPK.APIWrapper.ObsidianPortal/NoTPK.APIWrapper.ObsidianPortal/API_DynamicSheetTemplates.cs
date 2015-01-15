using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;
using Newtonsoft.Json.Linq;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_DynamicSheetTemplates
	{
		public static async Task<JArray> Index(string appId, string appSecret, string token, string tokenSecret)
		{
			const string indexUrl = "http://api.obsidianportal.com/v1/dynamic_sheet_templates.json";

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, indexUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContentJarray(requestMessage);
		}

		public static async Task<JObject> ShowById(string appId, string appSecret, string token, string tokenSecret, string dstId)
		{
			var showUrl = string.Format(@"http://api.obsidianportal.com/v1/dynamic_sheet_templates/{0}.json", dstId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> ShowBySlug(string appId, string appSecret, string token, string tokenSecret, string dstSlug)
		{
			var showUrl = string.Format(@"http://api.obsidianportal.com/v1/dynamic_sheet_templates/{0}.json", dstSlug);
			var optionalParams = new Dictionary<string, string>()
			{
				{"use_slug", "true"},
			};

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_slug=true", optionalParams);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}
	}
}