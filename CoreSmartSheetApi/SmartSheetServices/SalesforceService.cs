using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smartsheet.Api;
using Smartsheet.Api.Models;
using Smartsheet.Api.OAuth;
using System.Net.Http;
using CoreSmartSheetApi.SmartSheetInterface;

namespace CoreSmartSheetApi.SmartSheetServices
{
	public class SalesforceService :SalesforceInterface
	{
		public string LoginDeatils(Logindata logindata)
		{
			Logindata data = new Logindata();
			string response = string.Empty;
			try
			{
				using (var Client = new HttpClient())
				{
					HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
				 {
				  {"grant_type", "password"},
				  {"client_id", logindata.ClientId},
				  {"client_secret", logindata.ClientSecret},
				  {"username", logindata.Username},
				  {"password", logindata.Password}
				 });
					HttpResponseMessage message = Client.PostAsync(Logindata.LoginEndpoint, content).Result;
					response = message.Content.ReadAsStringAsync().Result;
				}

				Newtonsoft.Json.Linq.JObject obj = JObject.Parse(response);
				data.AuthToken = (string)obj["access_token"];
				data.ServiceUrl = (string)obj["instance_url"];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return response;
		}
		public RootObject getRecordDetail(string token)
		{
			var apiendpoint = "https://tharuntex-dev-ed.my.salesforce.com/services/data/v53.0/query/?q=";
			string query = "SELECT Id,Name,Dep_Type__c,OwnerId FROM Opportunity WHERE OwnerId IN (SELECT Id FROM User where Username='tharminijagadeesan21@gmail.com')";
			var Client = new HttpClient();
			string urlStr = apiendpoint + query;
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlStr);
			request.Headers.Add("Authorization", "Bearer " + token);
			HttpResponseMessage response = Client.SendAsync(request).Result;
			string output = response.Content.ReadAsStringAsync().Result.ToString();
			RootObject rootObject = System.Text.Json.JsonSerializer.Deserialize<RootObject>(output);
			return rootObject;
		}
		public object GetOpportunities(string type, string token)
		{
			string query = "https://tharuntex-dev-ed.my.salesforce.com/services/data/v53.0/query/?q=select id,Dep_Type__c from Opportunity where Dep_Type__c=";
			try
			{
				{

					var Client = new HttpClient();
					//List<Opportunity> myOpportunities =[Select id, Name, Ownerid, From Opportunity Where OwnerId =:UserInfo.getUserId()];
					string urlStr = query + "'" + type + "'";
					HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlStr);
					//request.Headers.Add("Content-Type", "application/json");
					//request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					request.Headers.Add("Authorization", "Bearer " + token);
					//request.Headers.Add("Authorization", "Bearer " + Convert.ToBase64String(Encoding.UTF8.GetBytes(token)));
					HttpResponseMessage response = Client.SendAsync(request).Result;
					return response.Content.ReadAsStringAsync().Result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public RootObject GetOpportunityfieldlist(string token)
		{
			string query = "https://tharuntex-dev-ed.my.salesforce.com/services/data/v53.0/query/?q=select id,Dep_Type__c from Opportunity where Dep_Type__c!=''";
			try
			{
				{
					var Client = new HttpClient();
					string urlStr = query;
					HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlStr);
					request.Headers.Add("Authorization", "Bearer " + token);
					HttpResponseMessage response = Client.SendAsync(request).Result;
					string res = response.Content.ReadAsStringAsync().Result;
					RootObject rootObject = System.Text.Json.JsonSerializer.Deserialize<RootObject>(res);
					return rootObject;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Userdetails GetUserdetails(string token,string username)
		{
			string query = "https://tharuntex-dev-ed.my.salesforce.com/services/data/v53.0/query/?q=select id,Name,Email,Username from User where Username=" +"'"+ username +"'";
			try
			{
				{
					//SmartSheetInterface.User userinfo = new SmartSheetInterface.User();
					var Client = new HttpClient();
					string urlStr = query;
					HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlStr);
					request.Headers.Add("Authorization", "Bearer " + token);
					HttpResponseMessage response = Client.SendAsync(request).Result;
					string res = response.Content.ReadAsStringAsync().Result;
					Userdetails usrdata = System.Text.Json.JsonSerializer.Deserialize<Userdetails>(res);
					return usrdata;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
