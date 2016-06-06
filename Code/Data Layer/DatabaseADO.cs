using System;
using System.Linq;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.IO;
using System.Data;

namespace DBBasics
{
	/// <summary>
	/// TaskDatabase uses ADO.NET to create the [Items] table and create,read,update,delete data
	/// </summary>
	public class PatientDatabase 
	{
		static object locker = new object ();

		public SqliteConnection connection;

		public string path;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		public PatientDatabase (string dbPath)
		{
			var output = "";
			path = dbPath;
			// create the tables
			//createPatientDetailsTable (dbPath);
			bool exists = File.Exists (dbPath);

			//Put in create table to reset Patient Details table
			//createPatientDetailsTable (dbPath);


			//clear out database so can add in more tables

	if (!exists) {
				
				output=	createMedicSettingsTable (dbPath);
				output = output + createPatientDetailsTable (dbPath);
				output = output + createMedicalTable (dbPath);
				output = output + createTraumaTable (dbPath);
				output = output + createVitalsTable (dbPath);



//				
			Console.WriteLine (output);
		}
		}
//
		public static string  createMedicSettingsTable (string path)
		{
			// create a connection string for the database
			var connectionString = string.Format("Data Source={0};Version=3;", path);
			try
			{
				using (var conn = new SqliteConnection((connectionString)))
				{
					conn.Open ();
					using (var command = conn.CreateCommand())
					{

						command.CommandText = "CREATE TABLE MedicSettings (MedicID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
							"MedicFirstName ntext,MedicLastName ntext, RECLevel ntext, RECExpiryDate ntext, PHECCLevel ntext," +
							"PHECCExpiryDate ntext, PHECCPin ntext)";


						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery();

					}
					conn.Close ();
					return "Database table MedicSettings created successfully";
				}
			}
			catch (Exception ex)
			{
				var reason = string.Format("Failed to create database table MedicSettings - reason = {0}", ex.Message);
				return reason;
			}
		}


		public static string  createPatientDetailsTable (string path)
		{
			// create a connection string for the database
			var connectionString = string.Format("Data Source={0};Version=3;", path);
			try
			{
				using (var conn = new SqliteConnection((connectionString)))
				{
					conn.Open ();
					using (var command = conn.CreateCommand())
					{
						
						command.CommandText = "CREATE TABLE PatientDetails (PatientID	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
							"PatientFirstName ntext, PatientLastName ntext, PatientDateOfBirth ntext, PatientAge INTEGER, PatientGender ntext, " +
							"NextOfKin ntext, NOKContactDetails ntext, Location ntext, GridReference ntext, AssessStartDate ntext, AssessStartTime ntext, " +
							"Signs ntext, Allergies ntext, Medications ntext, PastHistory ntext, LastIntake ntext, Event next, MOI ntext, HandOverTo ntext, " +
							"HandOverDate ntext, HandOverTime ntext, PatientInfo ntext, Transferred INTEGER, MedicID	INTEGER," +
							"TraumaChiefComplaint INTEGER, TraumaType ntext, TraumaDesc ntext, TraumaLocation ntext, TraumaInterventions ntext," +
							"TraumaMedAdmin ntext, PainScaleBefore ntext, PainScaleAfter ntext," +
							//Medical
							"MedChiefComplaint INTEGER, " +
							"MedType ntext, " +
							"MedDesc ntext, " +
							"MedLocation ntext, " +
							"MedInterventions ntext," +
							"MedMedAdmin ntext, " +
							"Onset ntext, " +
							"Provokes ntext, " +
							"Quality ntext, " +
							"Radiates ntext, " +
							"Severity ntext, " +
							"Time ntext," +
							//Vitals
							"VitalSetDate ntext, " +
							"VitalSetTime ntext, " +
							"Status ntext, " +
							"PulseRate ntext, " +
							"PulseQuality ntext, " +
							"BloodPressure ntext," +
							"RespiratoryRate ntext," +
							"RespiratoryQuality ntext, " +
							"SPO ntext, " +
							"Temperature ntext, " +
							"Colour ntext, " +
							"BloodSugar ntext," +
							"CSM ntext, " +
							"LOC ntext, " +
							"Pupils ntext,"+
							"FOREIGN KEY(MedicID) REFERENCES MedicSettings(MedicID))";

						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery();

					}
					conn.Close ();
					return "Database table PatientDetails created successfully";
				}
			}
			catch (Exception ex)
			{
				var reason = string.Format("Failed to create database table PatientDetails - reason = {0}", ex.Message);
				return reason;
			}
		}



		public static string  createVitalsTable (string path)
		{
			// create a connection string for the database
			var connectionString = string.Format("Data Source={0};Version=3;", path);
			try
			{
				using (var conn = new SqliteConnection((connectionString)))
				{
					conn.Open ();
					using (var command = conn.CreateCommand())
					{

						command.CommandText = "CREATE TABLE Vitals (VitalSetID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, PatientID INTEGER," +
							"VitalSetDate ntext, VitalSetTime ntext, Status ntext, PulseRate ntext, PulseQuality ntext, BloodPressure ntext," +
							"RespiratoryRate ntext, RespiratoryQuality ntext, SPO ntext, Temperature ntext, Colour ntext, BloodSugar ntext," +
							"CSM ntext, LOC ntext, Pupils ntext, FOREIGN KEY(PatientID) REFERENCES PatientDetails(PatientID))";
						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery();
					}
					conn.Close ();
					return "Database table Vitals created successfully";
				}
			}
			catch (Exception ex)
			{
				var reason = string.Format("Failed to create database table Vitals - reason = {0}", ex.Message);
				return reason;
			}
		}

		public static string  createTraumaTable (string path)
		{
			// create a connection string for the database
			var connectionString = string.Format("Data Source={0};Version=3;", path);
			try
			{
				using (var conn = new SqliteConnection((connectionString)))
				{
					conn.Open ();
					using (var command = conn.CreateCommand())
					{

						command.CommandText = "CREATE TABLE Trauma (TraumaID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, PatientID INTEGER," +
							"TraumaChiefComplaint INTEGER, TraumaType ntext, TraumaDesc ntext, TraumaLocation ntext, TraumaImage ntext, " +
							"TraumaInterventions ntext,TraumaMedAdmin ntext, PainScaleBefore ntext, PainScaleAfter ntext," +
							"FOREIGN KEY(PatientID) REFERENCES PatientDetails (PatientID))";

						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery();
					}
					conn.Close ();
					return "Database table Trauma created successfully";
				}
			}
			catch (Exception ex)
			{
				var reason = string.Format("Failed to create database table Trauma - reason = {0}", ex.Message);
				return reason;
			}
		}

		public static string  createMedicalTable (string path)
		{
			// create a connection string for the database
			var connectionString = string.Format("Data Source={0};Version=3;", path);
			try
			{
				using (var conn = new SqliteConnection((connectionString)))
				{
					conn.Open ();
					using (var command = conn.CreateCommand())
					{

						command.CommandText = "CREATE TABLE Medical (MedicalID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, PatientID INTEGER," +
							"MedChiefComplaint INTEGER, MedType ntext, MedDesc ntext, MedLocation ntext, MedImage ntext, MedInterventions ntext," +
							"MedMedAdmin ntext, Onset ntext, Provokes ntext, Quality ntext, Radiates ntext, Time ntext," +
							"FOREIGN KEY(PatientID) REFERENCES PatientDetails (PatientID))";
						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery();
					}
					conn.Close ();
					return "Database table Medical created successfully";
				}
			}
			catch (Exception ex)
			{
				var reason = string.Format("Failed to create database table Medical - reason = {0}", ex.Message);
				return reason;
			}
		}

	
		#region "Basic Patient Details"



		/// <summary>Convert from DataReader to Task object</summary>
		PatientDetails  FromReader (SqliteDataReader r) {
			var t = new PatientDetails  ();
			t.PatientID = Convert.ToInt32 (r ["PatientID"]);
			t.PatientFirstName  = r ["PatientFirstName"].ToString ();
			t.PatientLastName  = r ["PatientLastName"].ToString ();
			t.Transferred = Convert.ToInt32 (r["Transferred"])== 1 ? true: false;
			t.PatientDateOfBirth   = r ["PatientDateOfBirth"].ToString ();
			t.PatientAge  =Convert.ToInt32 ( r ["PatientAge"]);
			t.PatientGender  = r ["PatientGender"].ToString ();
			t.NextOfKin   = r ["NextOfKin"].ToString ();
			t.NOKContactDetails  = r ["NOKContactDetails"].ToString ();
			t.Location   = r ["Location"].ToString ();
			t.GridReference   = r ["GridReference"].ToString ();
			t.AssessStartDate   = r ["AssessStartDate"].ToString ();
			t.AssessStartTime   = r ["AssessStartTime"].ToString ();
			//Sample History
			t.Signs   = r ["Signs"].ToString ();
			t.Allergies   = r ["Allergies"].ToString ();
			t.PastHistory  = r ["PastHistory"].ToString ();
			t.LastIntake   = r ["LastIntake"].ToString ();
			t.Event  = r ["Event"].ToString ();
			t.MOI   = r ["MOI"].ToString ();
			t.HandOverTo   = r ["HandOverTo"].ToString ();
			t.HandOverDate   = r ["HandOverDate"].ToString ();
			t.HandOverTime   = r ["HandOverTime"].ToString ();
			t.PatientInfo    = r ["PatientInfo"].ToString ();
			//Trauma
			t.TraumaChiefComplaint  = Convert.ToInt32 (r["TraumaChiefComplaint"])== 1 ? true: false;
			t.TraumaType   = r ["TraumaType"].ToString ();
			t.TraumaDesc    = r ["TraumaDesc"].ToString ();
			t.TraumaLocation   = r ["TraumaLocation"].ToString ();
			t.TraumaInterventions    = r ["TraumaInterventions"].ToString ();
			t.TraumaMedAdmin   = r ["TraumaMedAdmin"].ToString ();
			t.PainScaleBefore    = r ["PainScaleAfter"].ToString ();
			t.PainScaleAfter = r ["PainScaleAfter"].ToString ();
			//Medical
			t.MedChiefComplaint  = Convert.ToInt32 (r ["MedChiefComplaint"]) == 1 ? true : false;
			t.MedType    = r ["MedType"].ToString ();
			t.MedDesc   = r ["MedDesc"].ToString ();
			t.MedLocation    = r ["MedLocation"].ToString ();
			t.MedInterventions   = r ["MedInterventions"].ToString ();
			t.MedMedAdmin   = r ["MedMedAdmin"].ToString ();
			t.Onset   = r ["Onset"].ToString ();
			t.Provokes    = r ["Provokes"].ToString ();
			t.Quality   = r ["Quality"].ToString ();
			t.Severity    = r ["Severity"].ToString ();
			t.Radiates    = r ["Radiates"].ToString ();
			t.Time   = r ["Time"].ToString ();
			//Vitals
			t.VitalSetDate     = r ["VitalSetDate"].ToString ();
			t.VitalSetTime    = r ["VitalSetTime"].ToString ();
			t.Status = r ["Status"].ToString ();
			t.PulseRate    = r ["PulseRate"].ToString ();
			t.PulseQuality    = r ["PulseQuality"].ToString ();
			t.BloodPressure     = r ["BloodPressure"].ToString ();
			t.RespiratoryRate    = r ["RespiratoryRate"].ToString ();
			t.RespiratoryQuality     = r ["RespiratoryQuality"].ToString ();
			t.SPO    = r ["SPO"].ToString ();
			t.Temperature    = r ["Temperature"].ToString ();
			t.Colour    = r ["Colour"].ToString ();
			t.BloodSugar    = r ["BloodSugar"].ToString ();
			t.CSM    = r ["CSM"].ToString ();
			t.LOC   = r ["LOC"].ToString ();
			t.Pupils   = r ["Pupils"].ToString ();
			return t;
		}

//		/// <summary>Convert from DataReader to Task object</summary>
//		Medical   FromReader (SqliteDataReader mr) {
//			var mt = new Medical  ();
//			mt.PatientID = Convert.ToInt32 (mr ["Patientid"]);
//			mt.MedChiefComplaint  = Convert.ToInt32 (mr ["MedChiefComplaint"]) == 1 ? true : false;
//			mt.MedType    = mr ["MedType"].ToString ();
//			mt.MedDesc   = mr ["MedDesc"].ToString ();
//			mt.MedLocation    = mr ["MedLocation"].ToString ();
//			mt.MedInterventions   = mr ["MedInterventions"].ToString ();
//			mt.MedMedAdmin   = mr ["MedMedAdmin"].ToString ();
//			mt.Onset   = mr ["Onset"].ToString ();
//			mt.Provokes    = mr ["Provokes"].ToString ();
//			mt.Quality   = mr ["Quality"].ToString ();
//			mt.Radiates    = mr ["Radiates"].ToString ();
//			mt.Time   = mr ["Time"].ToString ();
//			return mt;
//		}
//		/// <summary>Convert from DataReader to Task object</summary>
//		Trauma    FromReader (SqliteDataReader tr) {
//			var t = new Trauma  ();
//			t.PatientID = Convert.ToInt32 (tr ["Patientid"]);
//			t.TraumaChiefComplaint  = Convert.ToInt32 (tr ["TraumaChiefComplaint"]) == 1 ? true : false;
//			t.TraumaType    = tr ["TraumaType"].ToString ();
//			t.TraumaDesc   = tr ["TraumaDesc"].ToString ();
//			t.TraumaLocation    = tr ["TraumaLocation"].ToString ();
//			t.TraumaInterventions   = tr ["TraumaInterventions"].ToString ();
//			t.TraumaMedAdmin   = tr ["TraumaMedAdmin"].ToString ();
//			t.PainScaleBefore    = tr ["Onset"].ToString ();
//			t.PainScaleAfter   = tr ["Provokes"].ToString ();
//			return t;
//		}
//
//		Vitals    FromReader (SqliteDataReader vr) {
//			var t = new Vitals ();
//			t.PatientID = Convert.ToInt32 (vr ["Patientid"]);
//			t.VitalSetDate     = vr ["VitalSetDate"].ToString ();
//			t.VitalSetTime    = vr ["VitalSetTime"].ToString ();
//			t.Status = vr ["Status"].ToString ();
//			t.PulseRate    = vr ["PulseRate"].ToString ();
//			t.PulseQuality    = vr ["PulseQuality"].ToString ();
//			t.BloodPressure     = vr ["BloodPressure"].ToString ();
//			t.RespiratoryRate    = vr ["RespiratoryRate"].ToString ();
//			t.RespiratoryQuality     = vr ["RespiratoryQuality"].ToString ();
//			t.SPO    = vr ["SPO"].ToString ();
//			t.Temperature    = vr ["Temperature"].ToString ();
//			t.Colour    = vr ["Colour"].ToString ();
//			t.BloodSugar    = vr ["BloodSugar"].ToString ();
//			t.CSM    = vr ["CSM"].ToString ();
//			t.LOC   = vr ["LOC"].ToString ();
//			t.Pupils   = vr ["Pupils"].ToString ();
//			return t;
//		}




		public IEnumerable<PatientDetails > GetPatients ()
		{
			var tl = new List<PatientDetails > ();

			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var contents = connection.CreateCommand ()) {
					contents.CommandText = "SELECT [PatientID],[PatientFirstName],[PatientLastName], [PatientDateOfBirth]," +
						"[PatientAge]," +
						"[PatientGender]," +
						"[NextOfKin]," +
						"[NOKContactDetails]," +
						"[Location]," +
						"[GridReference]," +
						"[PatientInfo]," +
						"[Signs]," +
						"[Allergies]," +
						"[Medications]," +
						"[PastHistory]," +
						"[LastIntake]," +
						"[Event]," +
						"[MOI]," +
						"[Transferred]," +
						"[HandOverTo]," +
						"[HandOverDate]," +
						"[HandOverTime]," +
						"[AssessStartDate], " +
						"[AssessStartTime]," +
						//Trauma
						"[TraumaChiefComplaint]," +
						"[TraumaType]," +
						"[TraumaDesc]," +
						"[TraumaLocation]," +
						"[TraumaInterventions]," +
						"[TraumaMedAdmin]," +
						"[PainScaleBefore]," +
						"[PainScaleAfter]," +
						//Medical
						"[MedChiefComplaint], " +
						"[MedType], " +
						"[MedDesc], " +
						"[MedLocation], " +
						"[MedInterventions]," +
						"[MedMedAdmin], " +
						"[Onset], " +
						"[Provokes], " +
						"[Quality], " +
						"[Radiates], " +
						"[Severity], " +
						"[Time]," +
						//Vitals
						"[VitalSetDate], " +
						"[VitalSetTime], " +
						"[Status], " +
						"[PulseRate], " +
						"[PulseQuality], " +
						"[BloodPressure]," +
						"[RespiratoryRate]," +
						"[RespiratoryQuality], " +
						"[SPO], " +
						"[Temperature], " +
						"[Colour], " +
						"[BloodSugar]," +
						"[CSM], " +
						"[LOC], " +
						"[Pupils]"+
						" from [PatientDetails]";
					var r = contents.ExecuteReader ();
					while (r.Read ()) {
						tl.Add (FromReader(r));
					}
				}
				connection.Close ();
			}
			return tl;
		}



		public PatientDetails  GetPatient (int id) 
		{
			var t = new PatientDetails  ();
			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "SELECT [PatientID], [PatientFirstName], [PatientLastName], [PatientDateOfBirth]," +
						"[PatientAge], " +
						"[PatientGender], " +
						"[NextOfKin]," +
						"[NOKContactDetails]," +
						"[Location]," +
						"[GridReference]," +
						"[PatientInfo]," +
						"[Signs]," +
						"[Allergies]," +
						"[Medications]," +
						"[PastHistory]," +
						"[LastIntake]," +
						"[Event]," +
						"[MOI]," +
						"[Transferred], " +
						"[HandOverTo]," +
						"[HandOverDate]," +
						"[HandOverTime]," +
						"[AssessStartDate], " +
						"[AssessStartTime]," +
						//Trauma
						"[TraumaChiefComplaint]," +
						"[TraumaType]," +
						"[TraumaDesc]," +
						"[TraumaLocation]," +
						"[TraumaInterventions]," +
						"[TraumaMedAdmin]," +
						"[PainScaleBefore]," +
						"[PainScaleAfter]," +
						//Medical
						"[MedChiefComplaint], " +
						"[MedType], " +
						"[MedDesc], " +
						"[MedLocation], " +
						"[MedInterventions]," +
						"[MedMedAdmin], " +
						"[Onset], " +
						"[Provokes], " +
						"[Quality], " +
						"[Radiates], " +
						"[Severity], " +
						"[Time]," +
						//Vitals
						"[VitalSetDate], " +
						"[VitalSetTime], " +
						"[Status], " +
						"[PulseRate], " +
						"[PulseQuality], " +
						"[BloodPressure]," +
						"[RespiratoryRate]," +
						"[RespiratoryQuality], " +
						"[SPO], " +
						"[Temperature], " +
						"[Colour], " +
						"[BloodSugar]," +
						"[CSM], " +
						"[LOC], " +
						"[Pupils]"+
						" from [PatientDetails] WHERE [PatientID] = ?";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });
					var r = command.ExecuteReader ();
					while (r.Read ()) {
						t = FromReader (r);
						break;
					}
				}
				connection.Close ();
			}
			return t;
		}

		public int SavePatient (PatientDetails  item) 
		{
			int r;
			lock (locker) {
				if (item.PatientID  != 0) {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "UPDATE [PatientDetails] SET [PatientFirstName] = ?, [PatientLastName] = ?,[PatientDateOfBirth] = ?, " +
							"[PatientAge] = ?, " +
							"[PatientGender] = ?," +
							"[NextOfKin] = ?," +
							"[NOKContactDetails] = ?," +
							"[Location] = ?," +
							"[GridReference] = ?," +
							"[PatientInfo] =?," +
							"[Signs] =?," +
							"[Allergies] = ?," +
							"[Medications] = ?," +
							"[PastHistory] =?," +
							"[LastIntake] =?," +
							"[Event] =? ," +
							"[MOI]=?," +
							"[Transferred] = ?," +
							"[HandOverTo] =?," +
							"[HandOverDate]=?," +
							"[HandOverTime]=?," + 
							//Trauma
							"[TraumaChiefComplaint]=?," +
							"[TraumaType]=?," +
							"[TraumaDesc]=?," +
							"[TraumaLocation]=?," +
							"[TraumaInterventions]=?," +
							"[TraumaMedAdmin]=?," +
							"[PainScaleBefore]=?," +
							"[PainScaleAfter]=?," +
							//Medical
							"[MedChiefComplaint]=?, " +
							"[MedType]=?, " +
							"[MedDesc]=?, " +
							"[MedLocation]=?, " +
							"[MedInterventions]=?," +
							"[MedMedAdmin]=?, " +
							"[Onset]=?, " +
							"[Provokes]=?, " +
							"[Quality]=?, " +
							"[Radiates]=?, " +
							"[Severity]=?, " +
							"[Time]=?," +
							//Vitals
							"[VitalSetDate]=?, " +
							"[VitalSetTime]=?, " +
							"[Status]=?, " +
							"[PulseRate]=?, " +
							"[PulseQuality]=?, " +
							"[BloodPressure]=?," +
							"[RespiratoryRate]=?," +
							"[RespiratoryQuality]=?, " +
							"[SPO]=?, " +
							"[Temperature]=?, " +
							"[Colour]=?, " +
							"[BloodSugar]=?," +
							"[CSM]=?, " +
							"[LOC]=?, " +
							"[Pupils]=?"+
							"WHERE [PatientID] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientFirstName  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientLastName  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientDateOfBirth   });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.PatientAge  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientGender  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.NextOfKin   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.NOKContactDetails    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Location    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.GridReference     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientInfo      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Signs  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Allergies  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Medications     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PastHistory    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.LastIntake   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Event     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MOI      });
					
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Transferred });

						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.HandOverTo    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.HandOverDate      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.HandOverTo      });
						//Trauma
						command.Parameters.Add (new SqliteParameter (DbType.Int32 ) { Value = item.TraumaChiefComplaint   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaType    });
						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.TraumaDesc   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaLocation   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaInterventions    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaMedAdmin     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleBefore    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleAfter     });
						//Medical
						command.Parameters.Add (new SqliteParameter (DbType.Int32 ) { Value = item.MedChiefComplaint   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedType    });
						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.MedDesc   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedLocation   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedInterventions    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedMedAdmin     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Onset     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Provokes     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Quality      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Radiates   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Severity    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Time     });
						//Vitals
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.VitalSetDate     });
						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.VitalSetTime    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Status    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PulseRate    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PulseQuality      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.BloodPressure      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.RespiratoryRate      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.RespiratoryQuality       });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.SPO    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Temperature      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Colour       });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.BloodSugar     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.CSM      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.LOC    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Pupils     });
					
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.PatientID  });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				} else {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [PatientDetails] ([PatientFirstName], [PatientLastName],[PatientDateOfBirth], [PatientAge], " +
							"[PatientGender]," +
							"[NextOfKin]," +
							"[NOKContactDetails]," +
							"[Location]," +
							"[GridReference]," +
							"[PatientInfo]," +
							"[Signs]," +
							"[Allergies]," +
							"[Medications]," +
							"[PastHistory]," +
							"[LastIntake]," +
							"[Event]," +
							"[MOI]," +
							"[Transferred]," +
							"[HandOverTo]," +
							"[HandOverDate]," +
							"[HandOverTime]," +
							"[AssessStartDate], " +
							"[AssessStartTime], " +
							//Trauma
							"[TraumaChiefComplaint]," +
							"[TraumaType]," +
							"[TraumaDesc]," +
							"[TraumaLocation]," +
							"[TraumaInterventions]," +
							"[TraumaMedAdmin]," +
							"[PainScaleBefore]," +
							"[PainScaleAfter]," +
							//Medical
							"[MedChiefComplaint], " +
							"[MedType], " +
							"[MedDesc], " +
							"[MedLocation], " +
							"[MedInterventions]," +
							"[MedMedAdmin], " +
							"[Onset], " +
							"[Provokes], " +
							"[Quality], " +
							"[Severity]," +
							"[Radiates], " +
							"[Time]," +
							//Vitals
							"[VitalSetDate], " +
							"[VitalSetTime], " +
							"[Status], " +
							"[PulseRate], " +
							"[PulseQuality], " +
							"[BloodPressure]," +
							"[RespiratoryRate]," +
							"[RespiratoryQuality], " +
							"[SPO], " +
							"[Temperature], " +
							"[Colour], " +
							"[BloodSugar]," +
							"[CSM], " +
							"[LOC], " +
							"[Pupils])"+
							"VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientFirstName });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientLastName  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientDateOfBirth   });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.PatientAge  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientGender   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.NextOfKin    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.NOKContactDetails    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Location    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.GridReference    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientInfo    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Signs  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Allergies  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Medications     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PastHistory    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.LastIntake   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Event     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MOI      });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Transferred });

						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.HandOverTo    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.HandOverDate     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.HandOverTime     });

						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.AssessStartDate   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.AssessStartTime   });
						//Trauma
						command.Parameters.Add (new SqliteParameter (DbType.Int32 ) { Value = item.TraumaChiefComplaint   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaType    });
						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.TraumaDesc   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaLocation   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaInterventions    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaMedAdmin     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleBefore    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleAfter     });
						//Medical
						command.Parameters.Add (new SqliteParameter (DbType.Int32 ) { Value = item.MedChiefComplaint   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedType    });
						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.MedDesc   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedLocation   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedInterventions    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.MedMedAdmin     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Onset     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Provokes     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Quality      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Radiates   });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Severity    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Time     });
						//Vitals
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.VitalSetDate     });
						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.VitalSetTime    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Status    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PulseRate    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PulseQuality      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.BloodPressure      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.RespiratoryRate      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.RespiratoryQuality       });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.SPO    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Temperature      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Colour       });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.BloodSugar     });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.CSM      });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.LOC    });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Pupils     });

						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				}

			}
		}

		public int DeletePatient(int id) 
		{
			lock (locker) {
				int r;
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "DELETE FROM [PatientDetails] WHERE [PatientID] = ?;";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
					r = command.ExecuteNonQuery ();
				}
				connection.Close ();
				return r;
			}
		}

		#endregion "Basic Patient Details"

//		#region "Trauma"
//
//
//
//		// <summary>Convert from DataReader to Task object</summary>
//			Trauma  TRFromReader (SqliteDataReader tr) {
//				var z = new Trauma  ();
//				z.TraumaID = Convert .ToInt32 (tr["TraumaID"]);
//				z.PatientID = Convert.ToInt32 (tr ["PatientID"]);
//				z.TraumaChiefComplaint  = Convert.ToInt32 (tr["TraumaChiefComplaint"])== 1 ? true: false;
//				z.TraumaType   = tr ["TraumaType"].ToString ();
//				z.TraumaDesc    = tr ["TraumaDesc"].ToString ();
//				z.TraumaLocation   = tr ["TraumaLocation"].ToString ();
//				z.TraumaInterventions    = tr ["TraumaInterventions"].ToString ();
//				z.TraumaMedAdmin   = tr ["TraumaMedAdmin"].ToString ();
//				z.PainScaleBefore    = tr ["PainScaleAfter"].ToString ();
//				z.PainScaleAfter   = tr ["PainScaleAfter"].ToString ();
//				return z;
//			}
//
//		//		/// <summary>Convert from DataReader to Task object</summary>
//		//		Medical   FromReader (SqliteDataReader mr) {
//		//			var mt = new Medical  ();
//		//			mt.PatientID = Convert.ToInt32 (mr ["Patientid"]);
//		//			mt.MedChiefComplaint  = Convert.ToInt32 (mr ["MedChiefComplaint"]) == 1 ? true : false;
//		//			mt.MedType    = mr ["MedType"].ToString ();
//		//			mt.MedDesc   = mr ["MedDesc"].ToString ();
//		//			mt.MedLocation    = mr ["MedLocation"].ToString ();
//		//			mt.MedInterventions   = mr ["MedInterventions"].ToString ();
//		//			mt.MedMedAdmin   = mr ["MedMedAdmin"].ToString ();
//		//			mt.Onset   = mr ["Onset"].ToString ();
//		//			mt.Provokes    = mr ["Provokes"].ToString ();
//		//			mt.Quality   = mr ["Quality"].ToString ();
//		//			mt.Radiates    = mr ["Radiates"].ToString ();
//		//			mt.Time   = mr ["Time"].ToString ();
//		//			return mt;
//		//		}
//		//		/// <summary>Convert from DataReader to Task object</summary>
//		//		Trauma    FromReader (SqliteDataReader tr) {
//		//			var t = new Trauma  ();
//		//			t.PatientID = Convert.ToInt32 (tr ["Patientid"]);
//		//			t.TraumaChiefComplaint  = Convert.ToInt32 (tr ["TraumaChiefComplaint"]) == 1 ? true : false;
//		//			t.TraumaType    = tr ["TraumaType"].ToString ();
//		//			t.TraumaDesc   = tr ["TraumaDesc"].ToString ();
//		//			t.TraumaLocation    = tr ["TraumaLocation"].ToString ();
//		//			t.TraumaInterventions   = tr ["TraumaInterventions"].ToString ();
//		//			t.TraumaMedAdmin   = tr ["TraumaMedAdmin"].ToString ();
//		//			t.PainScaleBefore    = tr ["Onset"].ToString ();
//		//			t.PainScaleAfter   = tr ["Provokes"].ToString ();
//		//			return t;
//		//		}
//		//
//		//		Vitals    FromReader (SqliteDataReader vr) {
//		//			var t = new Vitals ();
//		//			t.PatientID = Convert.ToInt32 (vr ["Patientid"]);
//		//			t.VitalSetDate     = vr ["VitalSetDate"].ToString ();
//		//			t.VitalSetTime    = vr ["VitalSetTime"].ToString ();
//		//			t.Status = vr ["Status"].ToString ();
//		//			t.PulseRate    = vr ["PulseRate"].ToString ();
//		//			t.PulseQuality    = vr ["PulseQuality"].ToString ();
//		//			t.BloodPressure     = vr ["BloodPressure"].ToString ();
//		//			t.RespiratoryRate    = vr ["RespiratoryRate"].ToString ();
//		//			t.RespiratoryQuality     = vr ["RespiratoryQuality"].ToString ();
//		//			t.SPO    = vr ["SPO"].ToString ();
//		//			t.Temperature    = vr ["Temperature"].ToString ();
//		//			t.Colour    = vr ["Colour"].ToString ();
//		//			t.BloodSugar    = vr ["BloodSugar"].ToString ();
//		//			t.CSM    = vr ["CSM"].ToString ();
//		//			t.LOC   = vr ["LOC"].ToString ();
//		//			t.Pupils   = vr ["Pupils"].ToString ();
//		//			return t;
//		//		}
//
//
//
//
//		public IEnumerable<Trauma > GetTraumas ()
//		{
//			var tl = new List<Trauma  > ();
//
//			lock (locker) {
//				connection = new SqliteConnection ("Data Source=" + path);
//				connection.Open ();
//				using (var contents = connection.CreateCommand ()) {
//					contents.CommandText = "SELECT [TraumaID]," +
//						"[PatientID]," +
//						"[TraumaChiefComplaint]," +
//						"[TraumaType]," +
//						"[TraumaDesc]," +
//						"[TraumaLocation]," +
//						"[TraumaInterventions]," +
//						"[TraumaMedAdmin]," +
//						"[PainScaleBefore]," +
//						"[PainScaleAfter]" +
//						" from [Trauma]";
//					var r = contents.ExecuteReader ();
//					while (r.Read ()) {
//						tl.Add (TRFromReader(r));
//					}
//				}
//				connection.Close ();
//			}
//			return tl;
//		}
//
//
//
//		public Trauma  GetTrauma (int id) 
//		{
//			var t = new Trauma  ();
//			lock (locker) {
//				connection = new SqliteConnection ("Data Source=" + path);
//				connection.Open ();
//				using (var command = connection.CreateCommand ()) {
//					command.CommandText = "SELECT [TraumaID]," +
//						"[PatientID]," +
//						"[TraumaChiefComplaint]," +
//						"[TraumaType]," +
//						"[TraumaDesc]," +
//						"[TraumaLocation]," +
//						"[TraumaInterventions]," +
//						"[TraumaMedAdmin]," +
//						"[PainScaleBefore]," +
//						"[PainScaleAfter]" +
//						" from [Trauma] WHERE [TraumaID] = ?";
//					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });
//					var r = command.ExecuteReader ();
//					while (r.Read ()) {
//						t = TRFromReader (r);
//						break;
//					}
//				}
//				connection.Close ();
//			}
//			return t;
//		}
//
//		public int SaveTrauma (Trauma  item) 
//		{
//			int r;
//			lock (locker) {
//				if (item.PatientID  != 0) {
//					connection = new SqliteConnection ("Data Source=" + path);
//					connection.Open ();
//					using (var command = connection.CreateCommand ()) {
//						command.CommandText = "UPDATE [Trauma] " +
//							"SET [PatientID] = ?, " +
//							"[TraumaChiefComplaint] = ?," +
//							"[TraumaType] = ?, " +
//							"[TraumaDesc] = ?, " +
//							"[TraumaLocation] = ?," +
//							"[TraumaInterventions] = ?," +
//							"[TraumaMedAdmin] = ?," +
//							"[PainScaleBefore] = ?," +
//							"[PainScaleAfter] = ?" +
//							"WHERE [TraumaID] = ?;";
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientID  });
//						command.Parameters.Add (new SqliteParameter (DbType.Int32 ) { Value = item.TraumaChiefComplaint   });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaType    });
//						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.TraumaDesc   });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaLocation   });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaInterventions    });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaMedAdmin     });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleBefore    });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleAfter     });
//						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.TraumaID  });
//						r = command.ExecuteNonQuery ();
//					}
//					connection.Close ();
//					return r;
//				} else {
//					connection = new SqliteConnection ("Data Source=" + path);
//					connection.Open ();
//					using (var command = connection.CreateCommand ()) {
//						command.CommandText = "INSERT INTO [Trauma] " +
//							"([PatientID]," +
//							"[TraumaChiefComplaint]," +
//							"[TraumaType]," +
//							"[TraumaDesc]," +
//							"[TraumaLocation]," +
//							"[TraumaInterventions]," +
//							"[TraumaMedAdmin]," +
//							"[PainScaleBefore]," +
//							"[PainScaleAfter])" +
//							"VALUES (?,?,?,?,?,?,?,?,?)";
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientID  });
//						command.Parameters.Add (new SqliteParameter (DbType.Int32 ) { Value = item.TraumaChiefComplaint   });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaType    });
//						command.Parameters.Add (new SqliteParameter (DbType.String ) { Value = item.TraumaDesc   });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaLocation   });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaInterventions    });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.TraumaMedAdmin     });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleBefore    });
//						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PainScaleAfter     });
//						r = command.ExecuteNonQuery ();
//					}
//					connection.Close ();
//					return r;
//				}
//
//			}
//		}
//
//		public int DeleteTrauma(int id) 
//		{
//			lock (locker) {
//				int r;
//				connection = new SqliteConnection ("Data Source=" + path);
//				connection.Open ();
//				using (var command = connection.CreateCommand ()) {
//					command.CommandText = "DELETE FROM [Trauma] WHERE [TraumaID] = ?;";
//					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
//					r = command.ExecuteNonQuery ();
//				}
//				connection.Close ();
//				return r;
//			}
//		}
//
//		#endregion "Trauma"

	}
}
