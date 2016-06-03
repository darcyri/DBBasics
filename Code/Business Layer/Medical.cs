using System;

namespace DBBasics
{
	public class Medical
	{
		public Medical ()
		{
		}
		public int MedicalID { get; set;}
		public int PatientID { get; set; }

		public bool MedChiefComplaint { get; set; }
		public string MedType{ get; set; }
		public string MedDesc { get; set; }
		public string MedLocation { get; set; }
		public string MedInterventions { get; set; }
		public string MedMedAdmin{ get; set; }

		//Pain
		public string Onset { get; set; }
		public string Provokes { get; set; }
		public string Quality { get; set; }
		public string Radiates { get; set; }
		public string Time { get; set; }

	}
}

