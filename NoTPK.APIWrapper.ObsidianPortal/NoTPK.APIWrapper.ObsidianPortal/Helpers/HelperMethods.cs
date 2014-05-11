using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace NoTPK.APIWrapper.ObsidianPortal.Helpers
{
	public class HelperMethods
	{
		public static Dictionary<string, object> ParseJson(string jsonText)
		{
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Dictionary<string, object>>(jsonText);
		}

		public static string ValidateEntry(object value)
		{
			return value != null ? value.ToString() : string.Empty;
		}
	}
}