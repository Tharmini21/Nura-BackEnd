using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSmartSheetApi.SmartSheetInterface
{
	public interface SalesforceInterface
	{
		string LoginDeatils(Logindata logindata);
		RootObject getRecordDetail(string token);
		RootObject GetOpportunityfieldlist(string token);
		object GetOpportunities(string type, string token);
		Userdetails GetUserdetails(string token,string username);

	}
}
