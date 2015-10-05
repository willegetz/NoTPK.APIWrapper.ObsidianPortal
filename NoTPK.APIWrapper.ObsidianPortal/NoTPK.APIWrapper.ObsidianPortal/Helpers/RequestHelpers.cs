﻿using ApprovalUtilities.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NoTPK.APIWrapper.ObsidianPortal.Helpers
{
	public class RequestHelpers
	{
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private static string GenerateTimeStamp(Clock clock)
		{
            TimeSpan secondsSinceUnixEpocStart = clock.Load().ToUniversalTime() - Epoch;
			return Convert.ToInt64(secondsSinceUnixEpocStart.TotalSeconds).ToString(CultureInfo.InvariantCulture);
		}

		private static string ComputeSignature(string appSecret, string tokenSecret, string signatureData)
		{
			using (var algorithm = new HMACSHA1())
			{
				var escapedConsumerSecret = Uri.EscapeDataString(appSecret);
				var escapedTokenSecret = String.IsNullOrEmpty(tokenSecret) ? String.Empty : Uri.EscapeDataString(tokenSecret);

				algorithm.Key = Encoding.ASCII.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0}&{1}", escapedConsumerSecret, escapedTokenSecret));
				byte[] hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(signatureData));
				return Convert.ToBase64String(hash);
			}
		}

		internal static async Task<JObject> RetrieveResponseContentJobj(HttpRequestMessage request)
		{
			var _httpClient = new HttpClient();
			HttpResponseMessage response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				response.EnsureSuccessStatusCode();
			}

			string responseText = await response.Content.ReadAsStringAsync();
			var jsonObject = JObject.Parse(responseText);
			return jsonObject;
		}

		internal static async Task<JArray> RetrieveResponseContentJarray(HttpRequestMessage request)
		{
			var _httpClient = new HttpClient();
			HttpResponseMessage response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				response.EnsureSuccessStatusCode();
			}

			string responseText = await response.Content.ReadAsStringAsync();
			var jsonArray = JArray.Parse(responseText);
			return jsonArray;
		}

		internal static HttpRequestMessage BuildRequest(string appId, string appSecret, string accessToken, string accessTokenSecret, string location, HttpMethod webMethod, string queryParams = "", Dictionary<string, string> optionalParams = null)
		{
            string nonce = Guid.NewGuid().ToString("N");
            var authorizationHeader = GetAuthorizationHeader(new Clock(), appId, appSecret, accessToken, accessTokenSecret, location, webMethod, nonce, optionalParams);

			var fullUri = location + queryParams;
			var request = new HttpRequestMessage(webMethod, fullUri);
			request.Headers.Add("Authorization", authorizationHeader);
			return request;
		}

		public static string GetAuthorizationHeader(Clock clock, string appId, string appSecret, string accessToken, string accessTokenSecret, string location, HttpMethod webMethod, string nonce, Dictionary<string, string> optionalParams = null)
        {
            var oauthTimestamp = GenerateTimeStamp(clock);
            var authorizationParts = GetAuthorizationParts(appId, accessToken, nonce, oauthTimestamp, optionalParams);
            var parameterString = BuildAuthorizationParameterString(authorizationParts);

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
            return authorizationHeader;
        }

        public static string BuildAuthorizationParameterString(SortedDictionary<string, string> authorizationParts)
        {
            var parameterBuilder = new StringBuilder();
            foreach (var authorizationKey in authorizationParts)
            {
                parameterBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(authorizationKey.Key), Uri.EscapeDataString(authorizationKey.Value));
            }
            parameterBuilder.Length--;
            string parameterString = parameterBuilder.ToString();
            return parameterString;
        }

        public static SortedDictionary<string, string> GetAuthorizationParts(string appId, string accessToken, string nonce, string oauthTimestamp, Dictionary<string, string> optionalParams = null)
        {
            var authorizationParts = new SortedDictionary<string, string>()
            {
                {"oauth_consumer_key", appId},
                {"oauth_nonce", nonce},
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_token", accessToken},
                {"oauth_timestamp", oauthTimestamp},
                {"oauth_version", "1.0"},
            };

            if (optionalParams != null)
            {
                foreach (var param in optionalParams)
                {
                    authorizationParts.Add(param.Key, param.Value);
                }
            }

            return authorizationParts;
        }
    }
}