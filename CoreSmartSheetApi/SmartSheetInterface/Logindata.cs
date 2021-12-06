using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreSmartSheetApi.SmartSheetInterface
{
	public class Logindata
	{
		//public const string LoginEndpoint = "https://cloudwave2.my.salesforce.com/services/oauth2/token";
		public const string LoginEndpoint = "https://tharuntex-dev-ed.my.salesforce.com/services/oauth2/token";
		//public const string ApiEndpoint = "/services/data/v36.0/"; //Use your org's version number
		public string Username { get; set; }
		public string Password { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string Token { get; set; }
		public string AuthToken { get; set; }
		public string ServiceUrl { get; set; }

	}
}
