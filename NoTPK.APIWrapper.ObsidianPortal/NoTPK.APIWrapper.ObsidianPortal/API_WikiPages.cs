using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_WikiPages
	{
		public static async Task<string> IndexByCampaignId(string appId, string appSecret, string token, string tokenSecret, string campaignId)
		{
			var indexUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis.json", campaignId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, indexUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}

		public static async Task<string> ShowById(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageId)
		{
			var showUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis/{1}.json", campaignId, wikiPageId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}

		public static async Task<string> ShowBySlug(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageSlug)
		{
			var showUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis/{1}.json", campaignId, wikiPageSlug);
			var optionalParams = new Dictionary<string, string>();
			optionalParams.Add("use_slug", "true");

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_slug=true", optionalParams);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}

		public static async Task<string> Create(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageJson)
		{
			var createUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis.json", campaignId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, createUrl, HttpMethod.Post);
			requestMessage.Content = new StringContent(wikiPageJson, Encoding.UTF8, "application/json");
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}

		public static async Task<string> Update(string appId, string appSecret, string token, string tokenSecret, string campaignId, string wikiPageId, string wikiPageJson)
		{
			var updateUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/wikis/{1}.json", campaignId, wikiPageId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, updateUrl, HttpMethod.Put);
			requestMessage.Content = new StringContent(wikiPageJson, Encoding.UTF8, "application/json");
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}
	}
}