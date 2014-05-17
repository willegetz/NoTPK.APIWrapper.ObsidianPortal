using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_Characters
	{
		public static async Task<string> IndexByCampaignId(string appId, string appSecret, string token, string tokenSecret, string campaignId)
		{
			string indexUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters.json", campaignId);

			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, token, tokenSecret, indexUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowById(string appId, string appSecret, string token, string tokenSecret, string campaignId, string characterId)
		{
			var showUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, characterId);

			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowCharacterByCampaignIdCharacterSlug(string appId, string appSecret, string token, string tokenSecret, string campaignId, string slug)
		{
			string showUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, slug);
			var optionalParams = new Dictionary<string, string>()
			{
				{"use_slug", "true"},
			};

			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_slug=true", optionalParams);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}
	}
}