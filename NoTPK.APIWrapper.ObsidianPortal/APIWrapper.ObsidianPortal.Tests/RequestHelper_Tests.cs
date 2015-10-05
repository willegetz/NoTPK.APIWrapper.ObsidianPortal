using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;
using ApprovalTests;
using System.Net.Http;

namespace APIWrapper.ObsidianPortal.Tests
{
    [TestClass]
    public class RequestHelper_Tests
    {
        [TestMethod]
        public void TestGetAuthorizationHeader()
        {
            var appId = "appId";
            var appSecret = "appSecret";
            var accessToken = "accessToken";
            var accessTokenSecret = "accessTokenSecret";
            var location = "location";
            var nonce = "nonce";
            var webMethod = HttpMethod.Get;

            var result = RequestHelpers.GetAuthorizationHeader(appId, appSecret, accessToken, accessTokenSecret, location, webMethod, nonce);
            Approvals.Verify(result);
        }

        [TestMethod]
        public void TestGetAuthorizationParts()
        {
            var appId = "appId";
            var accessToken = "accessToken";
            var nonce = "nonce";
            var oauthTimestamp = "1444009267";
            var optionalParams = new Dictionary<string, string>()
            {
                {"optionalKey1", "optionalValue1" },
                {"optionalKey2", "optionalValue2" },
                {"optionalKey3", "optionalValue3" },
            };

            var result = RequestHelpers.GetAuthorizationParts(appId, accessToken, nonce, oauthTimestamp, optionalParams);
            Approvals.VerifyAll(result);
        }

        [TestMethod]
        public void TestGetAuthorizationParts_NoOptionalParams()
        {
            var appId = "appId";
            var accessToken = "accessToken";
            var nonce = "nonce";
            var oauthTimestamp = "1444009267";

            var result = RequestHelpers.GetAuthorizationParts(appId, accessToken, nonce, oauthTimestamp, null);
            Approvals.VerifyAll(result);
        }
    }
}