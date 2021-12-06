using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSmartSheetApi.SmartSheetInterface
{
	public class SmartsheetAnswerData
	{
		public string UserId { get; set; }
		public string Answer { get; set; }
		public long QuestionId { get; set; }
	}
}
