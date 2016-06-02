using System;

namespace DBBasics
{
	public class Trauma
	{
		public Trauma ()
		{
		}
			public int TraumaID { get; set;}
			public int PatientID { get; set; }
			
			public bool TraumaChiefComplaint { get; set; }
			public string TraumaType{ get; set; }
			public string TraumaDesc { get; set; }
			public string TraumaLocation { get; set; }
			public string TraumaInterventions { get; set; }
			public string TraumaMedAdmin{ get; set; }

			public string PainScaleBefore { get; set; }
			public string PainScaleAfter{ get; set; }



	}
}

