﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class ApiCalls
	{
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static async Task<string> ShowMe(string appId, string appSecret, string accessToken, string accessTokenSecret)
		{
			const string showMeUrl = @"http://api.obsidianportal.com/v1/users/me.json";

			var requestMessage = GetAuthorizationHeader(appId, appSecret, accessToken, accessTokenSecret, showMeUrl, HttpMethod.Get);
			return await RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowByUserId(string appId, string appSecret, string token, string tokenSecret, string userId)
		{
			string showUrl = string.Format(@"http://api.obsidianportal.com/v1/users/{0}.json", userId);

			var requestMessage = GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowByUserName(string appId, string appSecret, string token, string tokenSecret, string userName)
		{
			string showUrl = string.Format(@"http://api.obsidianportal.com/v1/users/{0}.json", userName);

			var optionalParams = new Dictionary<string, string>();
			optionalParams.Add("use_username", "true");
			var requestMessage = GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_username=true", optionalParams);
			return await RetrieveDataFromGet(requestMessage);
		}

		public static async Task<string> ShowByCampaignId(string appId, string appSecret, string token, string tokenSecret, string campaignId)
		{
			string showUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}.json", campaignId);

			var requestMessage = GetAuthorizationHeader(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RetrieveDataFromGet(requestMessage);
		}

		public static HttpRequestMessage GetAuthorizationHeader(string appId, string appSecret, string accessToken, string accessTokenSecret, string location, HttpMethod webMethod, string queryParams = "", Dictionary<string, string> optionalParams = null)
		{
			string nonce = Guid.NewGuid().ToString("N");

			var authorizationParts = new SortedDictionary<string, string>()
			{
				{"oauth_consumer_key", appId},
				{"oauth_nonce", nonce},
				{"oauth_signature_method", "HMAC-SHA1"},
				{"oauth_token", accessToken},
				{"oauth_timestamp", GenerateTimeStamp()},
				{"oauth_version", "1.0"},

			};

			if (optionalParams != null)
			{
				foreach (var param in optionalParams)
				{
					authorizationParts.Add(param.Key, param.Value);
				}
			}

			var parameterBuilder = new StringBuilder();
			foreach (var authorizationKey in authorizationParts)
			{
				parameterBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(authorizationKey.Key), Uri.EscapeDataString(authorizationKey.Value));
			}
			parameterBuilder.Length--;
			string parameterString = parameterBuilder.ToString();

			var canonicalizedRequestBuilder = new StringBuilder();
			canonicalizedRequestBuilder.Append(webMethod.Method);
			canonicalizedRequestBuilder.Append("&");
			canonicalizedRequestBuilder.Append(Uri.EscapeDataString(location));
			canonicalizedRequestBuilder.Append("&");
			canonicalizedRequestBuilder.Append(Uri.EscapeDataString(parameterString));

			string signature = ComputeSignature(appSecret, accessTokenSecret, canonicalizedRequestBuilder.ToString());
			authorizationParts.Add("oauth_signature", signature);
			authorizationParts.Remove("oauth_verifier");

			if (optionalParams != null)
			{
				foreach (var optionalParam in optionalParams)
				{
					authorizationParts.Remove(optionalParam.Key);
				}
				location = location + queryParams;
			}

			var authorizationHeaderBuilder = new StringBuilder();
			authorizationHeaderBuilder.Append("OAuth ");
			foreach (var authorizationPart in authorizationParts)
			{
				authorizationHeaderBuilder.AppendFormat(
					"{0}=\"{1}\", ", authorizationPart.Key, Uri.EscapeDataString(authorizationPart.Value));
			}
			authorizationHeaderBuilder.Length = authorizationHeaderBuilder.Length - 2;
			var authorizationHeader = authorizationHeaderBuilder.ToString();

			var request = new HttpRequestMessage(HttpMethod.Get, location);
			request.Headers.Add("Authorization", authorizationHeader);
			return request;
		}

		public static async Task<string> RetrieveDataFromGet(HttpRequestMessage request)
		{
			var _httpClient = new HttpClient();

			HttpResponseMessage response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				response.EnsureSuccessStatusCode();
			}

			string responseText = await response.Content.ReadAsStringAsync();
			return responseText;
		}

		private static string ComputeSignature(string appSecret, string tokenSecret, string signatureData)
		{
			using (var algorithm = new HMACSHA1())
			{
				var escapedConsumerSecret = Uri.EscapeDataString(appSecret);
				var escapedTokenSecret = string.IsNullOrEmpty(tokenSecret) ? string.Empty : Uri.EscapeDataString(tokenSecret);

				algorithm.Key = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0}&{1}", escapedConsumerSecret, escapedTokenSecret));
				byte[] hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(signatureData));
				return Convert.ToBase64String(hash);
			}
		}

		private static string GenerateTimeStamp()
		{
			TimeSpan secondsSinceUnixEpocStart = DateTime.UtcNow - Epoch;
			return Convert.ToInt64(secondsSinceUnixEpocStart.TotalSeconds).ToString(CultureInfo.InvariantCulture);
		}
	}
}