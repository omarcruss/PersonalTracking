using System;

namespace DAL
{
	public class TASK
	{
		public int ID{ get; set; }
		
		public int EmployeeID{ get; set; }
		
		public string TaskTitle{ get; set; }
		
		public string TaskContent{ get; set; }
		public int TaskState { get; set; }
		public DateTime TaskStartDate { get; set; }
		public DateTime TaskDeliveryDate { get; set; }
	}
}
