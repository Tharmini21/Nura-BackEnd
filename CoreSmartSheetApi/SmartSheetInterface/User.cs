using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSmartSheetApi.SmartSheetInterface
{
	public class User
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Username{ get; set; }
		public attributes attributes { get; set; }
	}

	public class attributeslist
	{
		public string type { get; set; }
		public string url { get; set; }
	}
	public class Userdetails
	{
		public int totalSize { get; set; }
		public bool done { get; set; }
		public List<User> records { get; set; }

	}

}
