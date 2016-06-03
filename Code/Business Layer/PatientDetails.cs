using System;

namespace DBBasics
{

	public class PatientDetails
	{
		public PatientDetails ()
		{
		}

		public int PatientID { get; set; }
		public int MedicID { get; set;}
		public string PatientFirstName { get; set; }
		public string PatientLastName { get; set; }
		public string PatientDateOfBirth { get; set; }
		public int PatientAge { get; set;}
		public string PatientGender { get; set;}

		public string NextOfKin { get; set; }
		public string NOKContactDetails { get; set;}
		public string Location { get; set; }
		public string GridReference { get; set; }
		public string AssessStartDate { get; set; }
		public string AssessStartTime { get; set; }

		//Sample History
		public string Signs { get; set; }
		public string Allergies { get; set; }
		public string Medications { get; set; }
		public string PastHistory { get; set; }
		public string LastIntake { get; set; }
		public string Event { get; set; }
		public string MOI { get; set; }

		//Handover
		public string HandOverTo { get; set;}
		public string HandOverDate { get; set;}
		public string HandOverTime { get; set;}

		public string PatientInfo { get; set; }

		public bool Transferred { get; set;}

	}



}
