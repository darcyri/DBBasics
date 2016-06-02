using System;

namespace DBBasics
{
	public class Vitals
	{
		public Vitals ()
		{
		}
		public int VitalSetID { get ; set; }
		public int PatientID { get ; set; }

		public string VitalSetDate { get; set; }
		public string VitalSetTime { get; set; }

		public string Status { get; set; }


		public string PulseRate { get; set; }
		public string PulseQuality { get; set; }
		public string BloodPressure { get; set; }
		public string RespiratoryRate { get; set; }
		public string RespiratoryQuality { get; set; }
		public string SPO { get; set; }
		public string Temperature { get; set; }
		public string Colour { get; set; }
		public string BloodSugar { get; set; }
		public string CSM { get; set; }
		public string LOC { get; set; }
		public string Pupils { get; set; }

	}
}

