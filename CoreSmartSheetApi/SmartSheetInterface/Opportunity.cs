using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSmartSheetApi.SmartSheetInterface
{
	public class Opportunity
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Dep_Type__c { get; set; }
		public string OwnerId { get; set; }

		//public List<User> userList { get; set; }
		//public string Owner { get; set; }
		//public string OwnerId { get; set; }
		public attributes attributes { get; set; }
	}

	public class attributes {
		public string type { get; set; }
		public string url { get; set; }
	} 
	public class RootObject
	{
		public int totalSize { get; set; }
		public bool done { get; set; }
		public List<Opportunity> records { get; set; }
	
	}
}
