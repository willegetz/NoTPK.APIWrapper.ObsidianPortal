using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
	public class API_Characters_Tests
	{
		private static string _appId = "";
		private static string _appSecret = "";
		private static string _token = "";
		private static string _tokenSecret = "";

		private static XElement _approvedResults;
		private static XElement _testVariables;

		[ClassInitialize]
		public static void LoadConstants(TestContext testContext)
		{
			var configPath = Path.GetFullPath(@"..\..\..\..\..\..\Configs\NoTPK.APIWrapper.ObsidianPortal.Tests.Config.xml");
			var configDoc = XDocument.Load(configPath);
			var tokens = (from t in configDoc.Descendants("TestTokens") select t).FirstOrDefault();

			if (tokens != null)
			{
				_appId = (string)tokens.Element("AppId");
				_appSecret = (string)tokens.Element("AppSecret");
				_token = (string)tokens.Element("AccessToken");
				_tokenSecret = (string)tokens.Element("AccessTokenSecret");
			}

			var approvedPath = Path.GetFullPath(@"..\..\..\..\..\..\Configs\NoTPK.APIWrapper.ObsidianPortal.Tests.Approved.xml");
			var approvedDOc = XDocument.Load(approvedPath);

			_approvedResults = (from a in approvedDOc.Descendants("ApprovedValues").Descendants("Characters") select a).FirstOrDefault();
			_testVariables = (from v in configDoc.Descendants("TestVariables") select v).FirstOrDefault();
		}

		[TestMethod]
		public async Task Test_Characters_Index__ByCampaignId()
		{
			var approved = (string)_approvedResults.Element("Index_Character");
			var campaignId = (string)_testVariables.Element("CampaignId");

			var result = await API_Characters.IndexByCampaignId(_appId, _appSecret, _token, _tokenSecret, campaignId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_Characters_Show__ByCharacterId()
		{
			var approved = (string)_approvedResults.Element("Show_CharacterById");
			var campaignId = (string)_testVariables.Element("CampaignId");
			var characterId = (string)_testVariables.Element("CharacterId");

			var result = await API_Characters.ShowById(_appId, _appSecret, _token, _tokenSecret, campaignId, characterId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_Characters_Show__ByCharacterSlug()
		{
			var approved = (string)_approvedResults.Element("Show_CharacterBySlug");
			var campaignId = (string)_testVariables.Element("CampaignId");
			var characterSlug = (string)_testVariables.Element("CharacterSlug");

			var result = await API_Characters.ShowBySlug(_appId, _appSecret, _token, _tokenSecret, campaignId, characterSlug);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public async Task Test_Characters_Create__ByCampaignId()
		{
			var approved = (string)_approvedResults.Element("Show_CreateCharacter");
			var campaignId = (string)_testVariables.Element("ModifiableCampaign");
			var newCharacter = (string)_testVariables.Element("NewCharacter");

			var result = await API_Characters.Create(_appId, _appSecret, _token, _tokenSecret, campaignId, newCharacter);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public async Task Test_Characters_Update__ByCharacterId()
		{
			var approved = (string) _approvedResults.Element("Show_UpdateCharacter");
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var updateCharacter = (string)_testVariables.Element("UpdateCharacterId");
			var updateContent = (string) _testVariables.Element("UpdateContent");

			var result = await API_Characters.Update(_appId, _appSecret, _token, _tokenSecret, campaignId, updateCharacter, updateContent);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public async Task Test_Characters_Delete__ByCharacterId()
		{
			var approved = (string) _approvedResults.Element("Show_DeleteCharacter");
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var deleteCharacter = (string) _testVariables.Element("DeleteCharacterId");

			var result = await API_Characters.Delete(_appId, _appSecret, _token, _tokenSecret, campaignId, deleteCharacter);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_Characters_StoreLocal__ByCharacterId()
		{
			// Store the character locally to allow for undoing
			var campaignId = (string)_testVariables.Element("CampaignId");
			var characterId = (string)_testVariables.Element("CharacterId");

			var result = await API_Characters.ShowById(_appId, _appSecret, _token, _tokenSecret, campaignId, characterId);

			var serializer = new JavaScriptSerializer();
			var parsedJson = serializer.Deserialize<Dictionary<string, object>>(result);
			var dstData = parsedJson["dynamic_sheet"];



			string dst = serializer.Serialize(dstData);

			var saveName = string.Format("{0}_dst", characterId);

			var storageLocation = API_Characters.StoreLocal(saveName, dst);
			Assert.IsTrue(File.Exists(storageLocation));
		}
	}
}