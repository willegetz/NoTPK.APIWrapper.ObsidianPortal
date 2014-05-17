﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

		[AssemblyInitialize]
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

			var result = await ApiCalls.IndexCharactersByCampaignId(_appId, _appSecret, _token, _tokenSecret, campaignId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_Characters_Show__ByCharacterId()
		{
			var approved = (string) _approvedResults.Element("Show_CharacterById");
			var campaignId = (string) _testVariables.Element("CampaignId");
			var characterId = (string) _testVariables.Element("CharacterId");

			var result = await ApiCalls.ShowCharacterByCampaignIdCharacterId(_appId, _appSecret, _token, _tokenSecret, campaignId, characterId);
			Assert.AreEqual(approved, result);
		}
	}
}