using System;
using System.Threading.Tasks;
using APIWrapper.ObsidianPortal.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
	public class API_Users_Tests
	{
		private const string AppId = "";
		private const string AppSecret = "";
		private const string Token = "";
		private const string TokenSecret = "";
			
		[TestMethod]
		public async Task Test_Users_Show__LoggedInUser()
		{
			var result = await ApiCalls.ShowMe(AppId, AppSecret, Token, TokenSecret);
			Assert.AreEqual("", result);
		}
	}
}
