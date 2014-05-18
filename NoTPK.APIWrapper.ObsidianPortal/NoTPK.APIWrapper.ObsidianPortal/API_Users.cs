﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_Users
	{
		public static async Task<string> ShowMe(string appId, string appSecret, string accessToken, string accessTokenSecret)
		{
			const string showMeUrl = @"http://api.obsidianportal.com/v1/users/me.json";

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, accessToken, accessTokenSecret, showMeUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}

		public static async Task<string> ShowById(string appId, string appSecret, string token, string tokenSecret, string userId)
		{
			string showUrl = String.Format(@"http://api.obsidianportal.com/v1/users/{0}.json", userId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}

		public static async Task<string> ShowByName(string appId, string appSecret, string token, string tokenSecret, string userName)
		{
			string showUrl = String.Format(@"http://api.obsidianportal.com/v1/users/{0}.json", userName);

			var optionalParams = new Dictionary<string, string>();
			optionalParams.Add("use_username", "true");
			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_username=true", optionalParams);
			return await RequestHelpers.RetrieveResponseContent(requestMessage);
		}
	}
}