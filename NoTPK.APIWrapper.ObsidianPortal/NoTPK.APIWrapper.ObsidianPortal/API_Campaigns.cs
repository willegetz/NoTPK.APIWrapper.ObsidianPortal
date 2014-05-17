﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_Campaigns
	{
		public static async Task<string> ShowByCampaignId(string appId, string appSecret, string token, string tokenSecret, string campaignId)
		{
			string showUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}.json", campaignId);

			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowByCampaignSlug(string appId, string appSecret, string accessToken, string accessTokenSecret, string slug)
		{
			string showUrl = String.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}.json", slug);
			
			var optionalParams = new Dictionary<string, string>();
			optionalParams.Add("use_slug", "true");
			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, accessToken, accessTokenSecret, showUrl, HttpMethod.Get,"?use_slug=true", optionalParams);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}
	}
}