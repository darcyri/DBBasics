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
			bool exists = File.Exists (dbPath);

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
							"Signs ntext, Allergies ntext, Medications ntext, PastHistory ntext, LastIntake ntext, MOI ntext, HandOverTo ntext, " +
							"HandOverDate ntext, HandOverTime ntext, PatientInfo ntext, Transferred INTEGER, MedicID	INTEGER," +
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
//			t.PatientDateOfBirth   = r ["PatientDateOfBirth"].ToString ();
//			t.PatientAge  =Convert.ToInt32 ( r ["PatientAge"]);
//			t.PatientGender  = r ["PatientGender"].ToString ();
//			t.NextOfKin   = r ["NextOfKin"].ToString ();
//			t.NOKContactDetails  = r ["NOKContactDetails"].ToString ();
//			t.Location   = r ["Location"].ToString ();
//			t.GridReference   = r ["GridReference"].ToString ();
//			t.AssessStartDate   = r ["AssessStartDate"].ToString ();
//			t.AssessStartTime   = r ["AssessStartTime"].ToString ();
//			t.Signs   = r ["Signs"].ToString ();
//			t.Allergies   = r ["Allergies"].ToString ();
//			t.PastHistory  = r ["PastHistory"].ToString ();
//			t.LastIntake   = r ["LastIntake"].ToString ();
//			t.Event  = r ["Event"].ToString ();
//			t.MOI   = r ["MOI"].ToString ();
//			t.HandOverTo   = r ["HandOverTo"].ToString ();
//			t.HandOverDate   = r ["HandOverDate"].ToString ();
//			t.HandOverTime   = r ["HandOverTime"].ToString ();
//			t.PatientInfo    = r ["PatientInfo"].ToString ();
			//t.Done = Convert.ToInt32 (r ["Done"]) == 1 ? true : false;
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
					contents.CommandText = "SELECT [PatientID],[PatientFirstName],[PatientLastName],[Transferred] from [PatientDetails]";
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
					command.CommandText = "SELECT [PatientID], [PatientFirstName], [PatientLastName], [Transferred] from [PatientDetails] WHERE [PatientID] = ?";
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
						command.CommandText = "UPDATE [PatientDetails] SET [PatientFirstName] = ?, [PatientLastName] = ?, [Transferred] = ? WHERE [PatientID] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientFirstName  });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientLastName  });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Transferred });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.PatientID  });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				} else {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [PatientDetails] ([PatientFirstName], [PatientLastName], [Transferred]) VALUES (?,?, ?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientFirstName });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.PatientLastName  });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Transferred });
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
	}
}
