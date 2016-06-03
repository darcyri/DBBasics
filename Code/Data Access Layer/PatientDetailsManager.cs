using System;
using System.Collections.Generic;

namespace DBBasics
{
		public static class PatientDetailsManager
	{
		static  PatientDetailsManager ()
		{
		}


		public static PatientDetails  GetPatient(int id)
		{
			return PatientAssessmentRepositoryADO.GetPatient(id);
		}

		public static IList<PatientDetails > GetPatients()
		{
			return new List<PatientDetails >(PatientAssessmentRepositoryADO.GetPatients());
		}

		public static int SavePatient (PatientDetails  item)
		{
			return PatientAssessmentRepositoryADO.SavePatient (item);
		}
//		public static int HistoryPatient (PatientDetails  item)
//		{
//			return PatientAssessmentRepositoryADO .HistoryPatient (item);
//		}


		public static int DeletePatient(int id)
		{
			return PatientAssessmentRepositoryADO.DeletePatient (id);
		}
	}
}