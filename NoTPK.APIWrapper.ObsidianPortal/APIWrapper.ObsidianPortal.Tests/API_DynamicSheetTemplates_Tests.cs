using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
	public class API_DynamicSheetTemplates_Tests
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

			_approvedResults = (from a in approvedDOc.Descendants("ApprovedValues").Descendants("DSTs") select a).FirstOrDefault();
			_testVariables = (from v in configDoc.Descendants("TestVariables") select v).FirstOrDefault();
		}

		[TestMethod]
		public async Task Test_DynamicSheetTemplates_Index()
		{
			var approved = (string) _approvedResults.Element("Index_DSTs");

			var result = await API_DynamicSheetTemplates.Index(_appId, _appSecret, _token, _tokenSecret);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_DynamicSheetTemplates_Show__ById()
		{
			var approved = (string) _approvedResults.Element("Show_DST");
			var dstId = (string) _testVariables.Element("DST_Id");

			var restul = await API_DynamicSheetTemplates.Show(_appId, _appSecret, _token, _tokenSecret, dstId);
			Assert.AreEqual(approved, restul);
		}
	}
}