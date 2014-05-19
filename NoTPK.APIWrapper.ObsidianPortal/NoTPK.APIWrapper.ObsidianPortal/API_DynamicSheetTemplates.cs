using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_DynamicSheetTemplates
	{
		public static async Task<string> Index(string appId, string appSecret, string token, string tokenSecret)
		{
			const string indexUrl = "http://api.obsidianportal.com/v1/dynamic_sheet_templates.json";

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, indexUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}

		public static async Task<string> Show(string appId, string appSecret, string token, string tokenSecret, string dstId)
		{
			var showUrl = string.Format(@"http://api.obsidianportal.com/v1/dynamic_sheet_templates/{0}.json", dstId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}
	}
}