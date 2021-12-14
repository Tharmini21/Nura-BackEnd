using CoreSmartSheetApi.SmartSheetInterface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smartsheet.Api;
using Smartsheet.Api.Models;
using Smartsheet.Api.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;



namespace CoreSmartSheetApi.SmartSheetServices
{

	public class SmartSheetService : SmartSheetInterfaces
	{
		static Dictionary<string, long> columnMap = new Dictionary<string, long>();
		public object Smartsheeturl()
		{
			OAuthFlow oauth = new OAuthFlowBuilder()
				 .SetClientId("9bc4e7idskvrxwjgngm")
				 .SetClientSecret("o6zc3rumct6er1jll9n")
				 .SetRedirectURL("http://localhost:4200/callback")
				 .Build();

			//.SetRedirectURL("http://localhost:4200/home")
			string url = oauth.NewAuthorizationURL
			(
				new Smartsheet.Api.OAuth.AccessScope[]
				{
				Smartsheet.Api.OAuth.AccessScope.READ_SHEETS,
				Smartsheet.Api.OAuth.AccessScope.WRITE_SHEETS,
				Smartsheet.Api.OAuth.AccessScope.SHARE_SHEETS,
				Smartsheet.Api.OAuth.AccessScope.DELETE_SHEETS,
				Smartsheet.Api.OAuth.AccessScope.CREATE_SHEETS,
				Smartsheet.Api.OAuth.AccessScope.READ_USERS,
				Smartsheet.Api.OAuth.AccessScope.ADMIN_USERS,
				Smartsheet.Api.OAuth.AccessScope.ADMIN_SHEETS,
				Smartsheet.Api.OAuth.AccessScope.ADMIN_WORKSPACES,
				},
				"key=Test"
			);
			//if (responseurl != null)
			//{

			//	string authorizationResponseURL = responseurl;

			//	AuthorizationResult authResult = oauth.ExtractAuthorizationResult(authorizationResponseURL);

			//	Token Oauthtoken = oauth.ObtainNewToken(authResult);
			//	//Assert.IsTrue(token.AccessToken == "ACCESS_TOKEN");
			//	//Token tokenRefreshed = oauth.RefreshToken(Oauthtoken);
			//	//Assert.IsTrue(token.AccessToken != "ACCESS_TOKEN");
			//	//oauth.RevokeToken(Oauthtoken);
			//	return Oauthtoken;
			//}
			return url;

		}
		public List<SmartsheetData> GetSmartsheetDatas(long id, string accesstoken)
		{
			List<SmartsheetData> result = new List<SmartsheetData>();
			try
			{


				Token token = new Token();
				token.AccessToken = accesstoken;
				SmartsheetClient smartsheet = new SmartsheetBuilder().SetAccessToken(token.AccessToken).Build();
				Sheet sheet = smartsheet.SheetResources.GetSheet(id, null, null, null, null, null, null, null);
				columnMap.Clear();
				foreach (Column column in sheet.Columns)
				{
					columnMap.Add(column.Title, (long)column.Id);
				}

				foreach (Row tmpRow in sheet.Rows)
				{
					SmartsheetData smartsheetData = null;
					foreach (Cell tmpCell in tmpRow.Cells)
					{
						smartsheetData = new SmartsheetData()
						{
							Id = Convert.ToInt64(tmpCell.ColumnId),
							No = Convert.ToInt32(tmpRow.Cells[0].Value),
							Question = Convert.ToString(tmpRow.Cells[1].Value),
							FieldName = Convert.ToString(tmpRow.Cells[2].Value),
							FieldType = Convert.ToString(tmpRow.Cells[3].Value),
							FieldValues = Convert.ToString(tmpRow.Cells[4].Value),
							IsRequired = Convert.ToBoolean(tmpRow.Cells[5].Value),
							Visible = Convert.ToString(tmpRow.Cells[6].Value),

						};
					}
					result.Add(smartsheetData);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public string SaveSmartsheetDatas(long id, List<SmartsheetAnswerData> data, string accesstoken)
		{
			List<SmartsheetAnswerData> result = new List<SmartsheetAnswerData>();
			try
			{
				Token token = new Token();
				token.AccessToken = accesstoken;
				SmartsheetClient smartsheet = new SmartsheetBuilder().SetAccessToken(token.AccessToken).Build();
				//// Get current user
				UserProfile userProfile = smartsheet.UserResources.GetCurrentUser();
				Sheet sheet = smartsheet.SheetResources.GetSheet(id, null, null, null, null, null, null, null);
				columnMap.Clear();
				foreach (Column column in sheet.Columns)
					columnMap.Add(column.Title, (long)column.Id);

				List<Cell> cells = new List<Cell>();
				Cell[] cellsA = null;
				Row rowA = null;
				//for (int i = 0; i < sheet.Columns.Count; i++)
				//{
				//	for (int j = 0; j < data.Count; j++)
				//	{
				//		cellsA.Append(new Cell.AddCellBuilder(sheet.Columns[i].Id, data[i].UserId).Build());
				//		continue;
				//	}
				//}
				cellsA = new Cell[]
				{
					 new Cell.AddCellBuilder(sheet.Columns[0].Id, data[0].UserId).Build()
					,new Cell.AddCellBuilder(sheet.Columns[1].Id, (data[0].Answer!="" || data[0].Answer!=null )?data[0].Answer:"NA").Build()
					,new Cell.AddCellBuilder(sheet.Columns[2].Id, (data[1].Answer!="" || data[1].Answer!=null )?data[1].Answer:"NA").Build()
					,new Cell.AddCellBuilder(sheet.Columns[3].Id, (data[2].Answer!="" || data[2].Answer!=null )?data[2].Answer:"NA").Build()
					,new Cell.AddCellBuilder(sheet.Columns[4].Id, (data[3].Answer!="" || data[3].Answer!=null )?data[3].Answer:"NA").Build()
					,new Cell.AddCellBuilder(sheet.Columns[5].Id, (data[4].Answer!="" || data[4].Answer!=null )?data[4].Answer:"NA").Build()
					,new Cell.AddCellBuilder(sheet.Columns[6].Id, (data[5].Answer!="" || data[5].Answer!=null )?data[5].Answer:"NA").Build()
					,new Cell.AddCellBuilder(sheet.Columns[7].Id, (data[6].Answer!="" || data[6].Answer!=null )?data[6].Answer:"NA").Build()
					,new Cell.AddCellBuilder(sheet.Columns[8].Id, (data[7].Answer!="" || data[7].Answer!=null )?data[7].Answer:"NA").Build()

				};
				rowA = new Row.AddRowBuilder(true, null, null, null, null).SetCells(cellsA).Build();
				IList<Row> newRows = smartsheet.SheetResources.RowResources.AddRows(id, new Row[] { rowA });



				//for (int j = 0; j < data.Count; j++)
				//{
				//	cellsA = new Cell[]
				//				{
				//		new Cell.AddCellBuilder(columnMap["UserId"],data[j].UserId).Build(),
				//		new Cell.AddCellBuilder(columnMap["QuestionId"],data[j].QuestionId).Build(),
				//		new Cell.AddCellBuilder(columnMap["Answer"],data[j].Answer).Build(),
				//				};
				//	Row rowA = new Row.AddRowBuilder(null, true, null, null, null).SetCells(cellsA).Build();
				//	smartsheet.SheetResources.RowResources.AddRows(id, new Row[] { rowA });
				//}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return "success";
		}

		public List<MappingSheet> GetMappingSheetDetails(long id, string accesstoken)
		{
			List<MappingSheet> result = new List<MappingSheet>();
			try
			{

				Token token = new Token();
				token.AccessToken = accesstoken;
				SmartsheetClient smartsheet = new SmartsheetBuilder().SetAccessToken(token.AccessToken).Build();
				Sheet sheet = smartsheet.SheetResources.GetSheet(id, null, null, null, null, null, null, null);
				foreach (Row tmpRow in sheet.Rows)
				{
					MappingSheet MappingSheetData = null;
					foreach (Cell tmpCell in tmpRow.Cells)
					{
						MappingSheetData = new MappingSheet()
						{
							Key = Convert.ToString(tmpRow.Cells[0].Value),
							Values = Convert.ToString(tmpRow.Cells[1].Value),
							Style = Convert.ToString(tmpRow.Cells[2].Value),
							Theme = Convert.ToString(tmpRow.Cells[3].Value),
						};
					}
					result.Add(MappingSheetData);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public string GetAccessToken(AccesstokenData data)
		{
			//AccesstokenData tokendata = new AccesstokenData();
			string response = string.Empty;
			try
			{
				using (var Client = new HttpClient())
				{
					HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
					{
					 {"grant_type", "authorization_code"},
					 {"client_id", data.ClientId},
					 {"client_secret", data.ClientSecret},
					 {"code" ,data.Code }
					});
					HttpResponseMessage message = Client.PostAsync(AccesstokenData.TokenUrl, content).Result;
					response = message.Content.ReadAsStringAsync().Result;
				}
				
				Newtonsoft.Json.Linq.JObject obj = JObject.Parse(response);
				data.AuthToken = (string)obj["access_token"];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return response;
		}
		public string GetRefreshAccessToken(AccesstokenData data)
		{
			string response = string.Empty;
			try
			{
				using (var Client = new HttpClient())
				{
					HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
					{
					 {"grant_type", "refresh_token"},
					 {"client_id", data.ClientId},
					 {"client_secret", data.ClientSecret},
					 {"refresh_token" ,data.RefreshToken }
					});
					HttpResponseMessage message = Client.PostAsync(AccesstokenData.TokenUrl, content).Result;
					response = message.Content.ReadAsStringAsync().Result;
				}
				Newtonsoft.Json.Linq.JObject obj = JObject.Parse(response);
				data.AuthToken = (string)obj["access_token"];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return response;
		}
	}
}
