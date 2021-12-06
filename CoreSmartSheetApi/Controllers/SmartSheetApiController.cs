using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using CoreSmartSheetApi.SmartSheetInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CoreSmartSheetApi.Controllers
{


	[Route("api/SmartSheet")]
	[ApiController]
	public class SmartSheetApiController : ControllerBase
	{
		//IConfiguration configuration = new ConfigurationBuilder()
		//	   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
		//	   .Build();
		//string smartsheetAPIToken = configuration.GetValue().AppSettings["AccessToken"];
		private static SmartSheetInterfaces _smartSheetInterface;
		IConfiguration _iconfiguration;
		public SmartSheetApiController(SmartSheetInterfaces smartSheetInterface, IConfiguration iconfiguration)
		{
			_smartSheetInterface = smartSheetInterface;
			_iconfiguration = iconfiguration;

		}
		[HttpPost("Smartsheet/Oauth")]
		public IActionResult OAuthFlow()
		{
			object Oauthurl = _smartSheetInterface.Smartsheeturl();
			IDictionary<string, string> response = new Dictionary<string, string>();
			response.Add("Oauthurl", Convert.ToString(Oauthurl));
			return new JsonResult(response);
		}
		[HttpPost("GetAccessToken")]
		public string GetAccessToken(AccesstokenData data)
		{
			//data.ClientId = _iconfiguration["SmartsheetClientId"];
			//data.ClientSecret = _iconfiguration["SmartsheetClientSecret"];
			string tokenobject = _smartSheetInterface.GetAccessToken(data);
			return tokenobject;
			//return new JsonResult(tokenobject);
		}
		[HttpPost("GetRefreshAccessToken")]
		public string GetRefreshAccessToken(AccesstokenData data)
		{
			//data.ClientId = _iconfiguration["SmartsheetClientId"];
			//data.ClientSecret = _iconfiguration["SmartsheetClientSecret"];
			string tokenobject = _smartSheetInterface.GetRefreshAccessToken(data);
			return tokenobject;
			//return new JsonResult(tokenobject);
		}
		[HttpPost("GetMappingSheetDetails")]
		public List<MappingSheet> GetMappingSheetDetails(string Smartsheettoken)
		{
			string smartsheetAPIToken = string.Empty;
			if (Smartsheettoken != null)
			{
				smartsheetAPIToken = Smartsheettoken;
			}
			else {
				smartsheetAPIToken = _iconfiguration["AccessToken"];
			}
			long mappingsheetvalue = Convert.ToInt64(_iconfiguration["MappingSheetId"]);
			List<MappingSheet> smartsheetDatas = _smartSheetInterface.GetMappingSheetDetails(mappingsheetvalue, smartsheetAPIToken);
			return smartsheetDatas;
		}
		[HttpPost("GetSmartDatas")]
		public List<SmartsheetData> GetSmartData(string id, string Smartsheettoken)
		{
			long sheetid = long.Parse(id);
			string smartsheetAPIToken;
			if (Smartsheettoken != null)
			{
				smartsheetAPIToken = Smartsheettoken;
			}
			else
			{
				smartsheetAPIToken = _iconfiguration["AccessToken"];
			}
			//string smartsheetAPIToken = Smartsheettoken;
			List<SmartsheetData> smartsheetDatas = _smartSheetInterface.GetSmartsheetDatas(sheetid, smartsheetAPIToken);
			return smartsheetDatas;
		}
		[HttpPost("SaveSmartsheetdata")]
		public HttpResponseMessage SaveSmartsheetdata(SaveSmartsheet data)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			long sheetid = long.Parse(data.id);
			string smartsheetAPIToken;
			if (data.Smartsheettoken != null)
			{
				smartsheetAPIToken = data.Smartsheettoken;
			}
			else
			{
				smartsheetAPIToken = _iconfiguration["AccessToken"];
			}
			string smartsheetDatas = _smartSheetInterface.SaveSmartsheetDatas(sheetid, data.item, smartsheetAPIToken);
			if (smartsheetDatas == "success")
			{
				httpResponseMessage.StatusCode = HttpStatusCode.OK;
			}
			return httpResponseMessage;
		}
	}
}
