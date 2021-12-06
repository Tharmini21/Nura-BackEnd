using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSmartSheetApi.SmartSheetInterface
{
	public class SaveSmartsheet
	{
		public string id { get; set; }
		public List<SmartsheetAnswerData> item { get; set; }
		public string Smartsheettoken { get; set; }
	}
}
