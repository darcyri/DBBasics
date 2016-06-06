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

		//Trauma
		public bool TraumaChiefComplaint { get; set; }
		public string TraumaType{ get; set; }
		public string TraumaDesc { get; set; }
		public string TraumaLocation { get; set; }
		public string TraumaInterventions { get; set; }
		public string TraumaMedAdmin{ get; set; }

		public string PainScaleBefore { get; set; }
		public string PainScaleAfter{ get; set; }

		//Medical

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
		public string Severity { get; set;}
		public string Time { get; set; }


		//Vitals
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
