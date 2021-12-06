using CoreSmartSheetApi.SmartSheetInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSmartSheetApi.Controllers
{
	[Route("api/Salesforce")]
	[ApiController]
	public class SalesforceApiController : ControllerBase
	{

		private static SalesforceInterface _salesforceInterface;
		IConfiguration _iconfiguration;
		public SalesforceApiController(SalesforceInterface salesforceInterface, IConfiguration iconfiguration)
		{
			_salesforceInterface = salesforceInterface;
			_iconfiguration = iconfiguration;
		}
		[HttpPost("login")]
		public string login(Logindata logindata)
		{
			//logindata.ClientId = _iconfiguration["ClientId"];
			//logindata.ClientSecret = _iconfiguration["ClientSecret"];
			//logindata.Username = _iconfiguration["Username"];
			//logindata.Password = _iconfiguration["Password"];
			string authinfo = _salesforceInterface.LoginDeatils(logindata);
			return authinfo;
		}
		[HttpPost("GetOpportunities")]
		public RootObject GetOpportunities(string token)
		{
			string accesstoken = token;
			RootObject Opportunitiesinfo = _salesforceInterface.getRecordDetail(accesstoken);
			return Opportunitiesinfo;
		}
		[HttpPost("GetOpportunityfieldlist")]
		public RootObject GetOpportunityfieldlist(string token)
		{
			RootObject fieldinfo = _salesforceInterface.GetOpportunityfieldlist(token);
			return fieldinfo;
		}
		[HttpPost("GetalltheOpportunities")]
		public object GetalltheOpportunities(string type, string token)
		{
			object Opportunitiesinfo = _salesforceInterface.GetOpportunities(type, token);
			return Opportunitiesinfo;
		}
		[HttpPost("GetUserdetails")]
		public Userdetails GetUserdetails(string token,string username)
		{
			Userdetails usrinfo = _salesforceInterface.GetUserdetails(token,username);
			return usrinfo;
		}
	}
}
