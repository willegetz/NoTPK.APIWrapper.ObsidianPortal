using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace NoTPK.APIWrapper.ObsidianPortal
{
	public class API_Characters
	{
		public static async Task<JArray> IndexByCampaignId(string appId, string appSecret, string token, string tokenSecret, string campaignId)
		{
			string indexUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters.json", campaignId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, indexUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContentJarray(requestMessage);
		}

		public static async Task<JObject> ShowById(string appId, string appSecret, string token, string tokenSecret, string campaignId, string characterId)
		{
			var showUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, characterId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> ShowBySlug(string appId, string appSecret, string token, string tokenSecret, string campaignId, string characterSlug)
		{
			string showUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, characterSlug);
			var optionalParams = new Dictionary<string, string>()
			{
				{"use_slug", "true"},
			};

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, showUrl, HttpMethod.Get, "?use_slug=true", optionalParams);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> Create(string appId, string appSecret, string token, string tokenSecret, string campaignId, string characterJson)
		{
			string createUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters.json", campaignId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, createUrl, HttpMethod.Post);
			requestMessage.Content = new StringContent(characterJson, Encoding.UTF8, "application/json");
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> Update(string appId, string appSecret, string token, string tokenSecret, string campaignId, string characterId, string updateContent)
		{
			string updateUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, characterId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, updateUrl, HttpMethod.Put);
			requestMessage.Content = new StringContent(updateContent, Encoding.UTF8, "application/json");
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static async Task<JObject> Delete(string appId, string appSecret, string token, string tokenSecret, string campaignId, string characterId)
		{
			string deleteUrl = string.Format(@"http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json", campaignId, characterId);

			var requestMessage = RequestHelpers.BuildRequest(appId, appSecret, token, tokenSecret, deleteUrl, HttpMethod.Delete);
			return await RequestHelpers.RetrieveResponseContentJobj(requestMessage);
		}

		public static string StoreLocal(string characterId, JObject characterJson)
		{
			var specialFolder = Path.GetFullPath(Environment.SpecialFolder.ApplicationData.ToString());
			var opCharacterFolder = Path.Combine(specialFolder, "ObsidianPortal", "Characters");

			if (!Directory.Exists(opCharacterFolder))
			{
				Directory.CreateDirectory(opCharacterFolder);
			}

			var characterFileName = string.Format("{0}.txt", characterId);
			var filePath = Path.Combine(opCharacterFolder, characterFileName);

			JavaScriptSerializer serializer = new JavaScriptSerializer();
			var characterString = characterJson.ToString();
			
			File.WriteAllText(filePath, characterString);
	
			return filePath;
		}

		public static JObject RetrieveLocal(string storageLocation)
		{
			if (File.Exists(storageLocation))
			{
				var characterString = File.ReadAllText(storageLocation);
				return JObject.Parse(characterString);
			}
			
			return null;
		}
	}
}