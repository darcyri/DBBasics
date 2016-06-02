﻿using System;
using System.Collections .Generic ;
using System.IO ;

namespace DBBasics
{
	public class PatientAssessmentRepositoryADO
	{

			PatientDatabase  db = null;
			protected static string dbLocation;		
			protected static PatientAssessmentRepositoryADO  me;		

			static PatientAssessmentRepositoryADO  ()
			{
				me = new PatientAssessmentRepositoryADO ();
			}

			protected PatientAssessmentRepositoryADO ()
			{
				// set the db location
				dbLocation = DatabaseFilePath;

				// instantiate the database	
				db = new PatientDatabase (dbLocation);
			}

			public static string DatabaseFilePath 
			{
				get 
				{ 
				
				var sqliteFilename = "Simpledb_adonet1.db";
					//var sqliteFilename = "PatientAssessmentDatabase.db3";
				//Simpledb_adonet.db
					#if NETFX_CORE
					var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
					#else

					#if SILVERLIGHT
					// Windows Phone expects a local path, not absolute
					var path = sqliteFilename;
					#else

					#if __ANDROID__
					// Just use whatever directory SpecialFolder.Personal returns
					string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
					#else
					// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
					// (they don't want non-user-generated data in Documents)
					string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
					string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
					#endif
					var path = Path.Combine (libraryPath, sqliteFilename);
					#endif

					#endif
					return path;	
				}
			}

		public static PatientDetails  GetPatient(int id)
		{
			return me.db .GetPatient(id);
		}

		public static IEnumerable<PatientDetails> GetPatients ()
		{
			return me.db.GetPatients();
		}

		public static int SavePatient (PatientDetails  item)
		{
			return me.db.SavePatient(item);
		}

		public static int DeletePatient(int id)
		{
			return me.db.DeletePatient(id);
		}
//		public static int HistoryPatient(int id)
//		{
//			return me.db.HistoryPatientDetails(id);
//		}

//		//SAMPLE History
//		public static SampleHistory   GetSampleHistory(int id)
//		{
//			return me.db.GetSampleHistory(id);
//		}
//
////		public static IEnumerable<PatientDetails > GetPatients ()
////		{
////			return me.db.GetPatients();
////		}
//
//		public static int SaveSampleHistory (SampleHistory   item)
//		{
//			return me.db.SaveSampleHistory(item);
//		}
//
//		public static int DeleteSampleHistory(int id)
//		{
//			return me.db.DeleteSampleHistory(id);
//		}
//
//		public static int FindSampleHistory(int id)
//		{
//			return me.db.FindSampleHistoryID (id);
//		}

	}
}

