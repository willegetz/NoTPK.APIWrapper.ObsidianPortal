using System;
using System.Net.Http;
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
	}
}