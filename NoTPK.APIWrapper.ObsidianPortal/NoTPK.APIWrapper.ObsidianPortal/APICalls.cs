﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class ApiCalls
	{
		public static async Task<string> IndexCharactersByCampaignId(string appId, string appSecret, string token, string tokenSecret, string campaignId)
		{
			string indexUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters.json", campaignId);

			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, token, tokenSecret, indexUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowCharacterByCampaignIdCharacterId(string appId, string appSecret, string token, string tokenSecret, string campaignId, string characterId)
		{
			var showUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, characterId);

			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowCharacterByCampaignIdCharacterSlug(string appId, string appSecret, string token, string tokenSecret, string campaignId, string slug)
		{
			string showUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, slug);
			var optionalParams = new Dictionary<string, string>()
			{
				{"use_slug", "true"},
			};

			var requestMessage = RequestHelpers.GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_slug=true", optionalParams);
			return await RequestHelpers.RetrieveDataFromGet(requestMessage);
		}
	}
}