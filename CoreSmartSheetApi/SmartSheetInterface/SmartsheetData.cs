namespace CoreSmartSheetApi.SmartSheetInterface
{
	public class SmartsheetData
	{
		public long Id { get; set; }
		public int No { get; set; }
		public string Question { get; set; }
		public string FieldType  { get; set; }
		public string FieldValues { get; set; }
		public bool IsRequired { get; set; }
		public string Visible { get; set; }
		public string FieldName { get; set; }

	}
}
