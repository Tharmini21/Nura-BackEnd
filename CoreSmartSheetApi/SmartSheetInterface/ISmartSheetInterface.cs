using System.Collections.Generic;

namespace CoreSmartSheetApi.SmartSheetInterface
{
	public interface SmartSheetInterfaces
	{
		List<MappingSheet> GetMappingSheetDetails(long id, string token);
		List<SmartsheetData> GetSmartsheetDatas(long id, string accesstoken);
		string SaveSmartsheetDatas(long id, List<SmartsheetAnswerData> data, string accesstoken);
		object Smartsheeturl();
		string GetAccessToken(AccesstokenData data);
		string GetRefreshAccessToken(AccesstokenData data);
	}
}
