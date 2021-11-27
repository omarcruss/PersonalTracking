using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
	public class EMPOLYEE
	{
		public int ID { get; set; }
		public int UserNumber { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string ImagePath { get; set; }
		public int DepartmentID { get; set; }
		public int PositionID { get; set; }
		public int Salary { get; set; }
		public DateTime? BirthDay { get; set; }
		public string Adress { get; set; }
		public string Password { get; set; }
		public bool? isAdmin { get; set; }
	}
}