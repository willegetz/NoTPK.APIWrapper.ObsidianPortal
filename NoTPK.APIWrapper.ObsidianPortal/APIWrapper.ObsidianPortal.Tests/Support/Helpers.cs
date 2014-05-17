using System.Collections.Generic;
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
	}
}