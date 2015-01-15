using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;
using Newtonsoft.Json.Linq;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_WikiPages
	{
		public static async Task<JArray> IndexByCampaignId(string appId, string appSecret, string token, string tokenSecret, string campaignId)
		{
			var indexUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis.json", campaignId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, indexUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContentJarray(requestMessage);
		}

		public static async Task<JObject> ShowById(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageId)
		{
			var showUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis/{1}.json", campaignId, wikiPageId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> ShowBySlug(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageSlug)
		{
			var showUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis/{1}.json", campaignId, wikiPageSlug);
			var optionalParams = new Dictionary<string, string>();
			optionalParams.Add("use_slug", "true");

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_slug=true", optionalParams);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> Create(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageJson)
		{
			var createUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis.json", campaignId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, createUrl, HttpMethod.Post);
			requestMessage.Content = new StringContent(wikiPageJson, Encoding.UTF8, "application/json");
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> Update(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageId, string wikiPageJson)
		{
			var updateUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis/{1}.json", campaignId, wikiPageId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, updateUrl, HttpMethod.Put);
			requestMessage.Content = new StringContent(wikiPageJson, Encoding.UTF8, "application/json");
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> Delete(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageId)
		{
			var deleteUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis/{1}.json", campaignId, wikiPageId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, deleteUrl, HttpMethod.Delete);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}
	}
}