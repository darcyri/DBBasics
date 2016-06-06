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

//		public static Trauma  GetTrauma(int id)
//		{
//			return PatientAssessmentRepositoryADO.GetTrauma(id);
//		}
//
//		public static IList<Trauma > GetTraumas()
//		{
//			return new List<Trauma>(PatientAssessmentRepositoryADO.GetTraumas());
//		}
//
//		public static int SaveTrauma (Trauma  item)
//		{
//			return PatientAssessmentRepositoryADO.SaveTrauma (item);
//		}
//		//		public static int HistoryPatient (PatientDetails  item)
//		//		{
//		//			return PatientAssessmentRepositoryADO .HistoryPatient (item);
//		//		}
//
//
//		public static int DeleteTrauma(int id)
//		{
//			return PatientAssessmentRepositoryADO.DeleteTrauma (id);
//		}


	}
}