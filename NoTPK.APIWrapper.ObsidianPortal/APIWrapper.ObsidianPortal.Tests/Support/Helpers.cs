using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace APIWrapper.ObsidianPortal.Tests.Support
{
	public class Helpers
	{
		public static Dictionary<string, object> ParseJson(string jsonText)
		{
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Dictionary<string, object>>(jsonText);
		}

		public static string GetApprovedResults(string approvedKey)
		{
			var fileName = string.Format("{0}.txt", approvedKey);
			var approvedPath = Path.GetFullPath(string.Format(@"..\..\..\..\..\..\ApprovedFiles\{0}", fileName));
			var sb = "";
			using (var sr = new StreamReader(approvedPath, Encoding.Default, true))
			{
				var line = "";
				while ((line = sr.ReadLine()) != null)
				{
					sb += line;
				}
			}
			return sb;
		}

		public static JObject GetApprovedResultsJobj(string approvedKey)
		{
			var fileName = string.Format("{0}.txt", approvedKey);
			var approvedPath = Path.GetFullPath(string.Format(@"..\..\..\..\..\..\ApprovedFiles\{0}", fileName));
			var sb = "";
			using (var sr = new StreamReader(approvedPath, Encoding.Default, true))
			{
				var line = "";
				while ((line = sr.ReadLine()) != null)
				{
					sb += line;
				}
			}
			return JObject.Parse(sb);
		}
	}
}