namespace DAL
{
	public class PERMISSION
	{
		public int ID { get; set; }

		public int EmployeeID { get; set; }

		public System.DateTime PermissionStartDate { get; set; }

		public System.DateTime PermissionEndDate { get; set; }

		public int PermissionState { get; set; }

		public string PermissionExplanation { get; set; }

		public int PermissionDay { get; set; }
	}
}