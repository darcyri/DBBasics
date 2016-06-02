using Android.App;
using Android.Widget;
using Android.OS;

using System;
using System.Data;
using System.IO;
using System.Collections.Generic;

using Android.Content;
using Mono.Data.Sqlite;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DBBasics
{
	[Activity (Label = "DB Basics", MainLauncher = true, 
		Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
//		

		PatientListAdapter patientList;
		IList<PatientDetails> patients;
		Button addPatientButton;
		ListView patientListView;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

	
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			PersonSettings Person = new PersonSettings ();
			#region "List"
			//Find our controls
			 patientListView = FindViewById<ListView> (Resource.Id.listViewPatients );
			 addPatientButton = FindViewById<Button> (Resource.Id.btnOpenPatient);

			// wire up add task button handler
			if(addPatientButton != null) {
				addPatientButton.Click += (sender, e) => {
					StartActivity(typeof(PatientDetailsScreen));
				};
			}

			// wire up task click handler
			if(patientListView != null) {
				patientListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var patientDetails = new Intent (this, typeof (PatientDetailsScreen));
					patientDetails.PutExtra ("PatientID", patients[e.Position].PatientID);
					StartActivity (patientDetails);
				};
			}
		

			#endregion "List"



			// Get our button from the layout resource,
			// and attach an event to it

			var btnAddDatabase = FindViewById <Button > (Resource.Id.btnCreateDatabase);
			var btnAddTable = FindViewById <Button> (Resource.Id.btnCreateTable);
			var btnAddItem = FindViewById <Button > (Resource.Id.btnAddItems);
			var btnReadItem = FindViewById <Button> (Resource.Id.btnRead);
			var btnCountItem = FindViewById <Button > (Resource.Id.btnCount);
			var btnDelTable = FindViewById <Button> (Resource.Id.btnDeleteTable);
			var btnDelDatabase = FindViewById <Button > (Resource.Id.btnDeleteDatabase);
			var results = FindViewById<TextView > (Resource.Id.txtResults);

			var btnAddMedicTable = FindViewById <Button> (Resource.Id.btnAddMedicTable);
			var btnAddPatientDetailsTable = FindViewById <Button> (Resource.Id.btnAddPatientTable);
			var btnAddVitalsTable = FindViewById <Button> (Resource.Id.btnVitalsTable );
			var btnAddMedicalTable = FindViewById <Button> (Resource.Id.btnAddMedical);
			var btnAddTraumaTable = FindViewById <Button> (Resource.Id.btnAddTrauma);

			var btnGoToPatient = FindViewById <Button> (Resource.Id.btnOpenPatient);

			var PeopleIDText = FindViewById <EditText  > (Resource.Id.txtPeopleID);
			int PeopleID = Convert.ToInt32 (PeopleIDText.Text);
			Person.PersonID = PeopleID;
			var btnDelFromPeople = FindViewById <Button > (Resource.Id.btnDeletePeopleID);

			// create and test the database connection
			var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var pathToDatabase = Path.Combine(docsFolder, "Simpledb_adonet1.db");



			var context = btnAddTable.Context;
			// create the event for the button
			//Create database

			#region "Buttons"
			btnAddDatabase.Click += (sender, e) =>
			{
				try
				{
					bool exists = File.Exists (pathToDatabase);
					if (!exists) {
					SqliteConnection.CreateFile(pathToDatabase);
					results.Text = string.Format("Database created successfully - filename = {0}\n", pathToDatabase);
					}
					else{
						results.Text =string.Format ("Database aleady exists");
					}
				}
				catch (IOException ex)
				{
					var reason = string.Format("Unable to create the database - reason = {0}", ex.Message);
					Toast.MakeText(context, reason, ToastLength.Long).Show();
				}

				// create the schema, perform using an async task
				//txtResult.Text += await createTable(pathToDatabase);
			};




			btnAddTable.Click += (sender, e) =>
			{
								
					results.Text = createTable (pathToDatabase );
			};
//

			btnAddItem.Click += (sender, e) =>
			{
				results.Text =addItems (pathToDatabase );

			};


			btnReadItem.Click +=  (sender, e) =>
			{
					results.Text = readItems (pathToDatabase );

			};

			btnCountItem.Click +=  (sender, e) =>
			{
				results.Text = countItems (pathToDatabase );

			};

			btnDelTable.Click +=  (sender, e) =>
			{
				results.Text = deleteTable (pathToDatabase );

			};
			btnDelDatabase.Click +=  (sender, e) =>
			{
				results.Text = deleteDatabase (pathToDatabase );

			};
			btnDelFromPeople.Click   +=  (sender, e) =>
			{
				results.Text = DeletePeopleDetails (pathToDatabase, Person .PersonID ); 

			};

			//Create Patient Assessment Database Tables

			btnAddMedicTable.Click  +=  (sender, e) =>
			{
				results.Text = PatientDatabase.createMedicSettingsTable  (pathToDatabase ); 

			};


			btnAddPatientDetailsTable .Click  +=  (sender, e) =>
			{
				results.Text = PatientDatabase.createPatientDetailsTable (pathToDatabase ); 

			};

			btnAddVitalsTable  .Click  +=  (sender, e) =>
			{
				results.Text = PatientDatabase.createVitalsTable  (pathToDatabase ); 

			};

			btnAddMedicalTable  .Click  +=  (sender, e) =>
			{
				results.Text = PatientDatabase.createMedicalTable  (pathToDatabase ); 
			};


			btnAddTraumaTable  .Click  +=  (sender, e) =>
			{
				results.Text = PatientDatabase.createTraumaTable  (pathToDatabase ); 
			};


		}

		#endregion "Buttons"

		protected override void OnResume ()
		{
			base.OnResume ();

			patients = PatientDetailsManager.GetPatients();

			// create our adapter
			patientList  = new PatientListAdapter(this, patients);

			//Hook up our adapter to our ListView
			patientListView.Adapter = patientList;
		}

		public static string  createTable (string path)
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
						command.CommandText = "CREATE TABLE People (PersonID INTEGER PRIMARY KEY AUTOINCREMENT, FirstName ntext, LastName ntext)";
						command.CommandType = CommandType.Text;
						 command.ExecuteNonQuery();
										
					}
					conn.Close ();
					return "Database table created successfully";
				}
			}
			catch (Exception ex)
			{
				var reason = string.Format("Failed to insert into the database - reason = {0}", ex.Message);
				return reason;
			}
		}

		public static string addItems (string path)
		//private async Task<string> addItems(string path)
		{
			// create a connection string for the database
			var connectionString = string.Format("Data Source={0};Version=3;", path);
			try
			{
				using (var conn = new SqliteConnection((connectionString)))
				{
					conn.Open ();
					using (var command = conn.CreateCommand ())
					{
						command.CommandText = "INSERT INTO [People] ([FirstName],[LastName]) VALUES ('Rita', 'Darcy')";
						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery (); // rowcount will be 1


					}
					conn.Close ();
					return "Items added to table successfully";
				}
			}
			catch (Exception ex)
			{
				var reason = string.Format("Failed to insert into Table People - reason = {0}", ex.Message);
				return reason;
			}
		}

//		public static string readItems (string path)
//		{
//			var output = "";
//			output += "\n=== Complex query example: ";
//		
//			// create a connection string for the database
//			var connectionString = string.Format ("Data Source={0};Version=3;", path);
//			try {
//				using (var conn = new SqliteConnection ((connectionString))) {
//					 conn.Open ();
//					using (var command = conn.CreateCommand ()) {
//						command.CommandText = "Select * from [People] ";
//						var r = command.ExecuteReader ();
//
//						output += "\nReading data"; 
//						while (r.Read ())
//							output += String.Format ("\n\tKey={0}; Value={1}{2}" ,
//								r ["PersonID"].ToString (),
//								r ["FirstName"].ToString (),
//								r["LastName"].ToString ());
//						
//					}
//					conn.Close () ;
//					return output;
//				}
//			} catch (Exception ex) {
//				var reason = string.Format ("Failed to read from Table People - reason = {0}", ex.Message);
//				return reason;
//			}
//
//		
//		}	

		public static string readItems (string path)
		{
			var output = "";
			output += "\n=== Complex query example: ";

			// create a connection string for the database
			var connectionString = string.Format ("Data Source={0};Version=3;", path);
			try {
				using (var conn = new SqliteConnection ((connectionString))) {
					conn.Open ();
					using (var command = conn.CreateCommand ()) {
						command.CommandText = "Select * from [PatientDetails] ";
						var r = command.ExecuteReader ();

						output += "\nReading data"; 
						while (r.Read ())
							output += String.Format ("\n\tKey={0}; Value={1}{2}" ,
								r ["PatientID"].ToString (),
								r ["PatientFirstName"].ToString (),
								r["PatientLastName"].ToString ());

					}
					conn.Close () ;
					return output;
				}
			} catch (Exception ex) {
				var reason = string.Format ("Failed to read from Table PatientDetails - reason = {0}", ex.Message);
				return reason;
			}


		}	

		public static string countItems (string path)
		{
			var output = "";
			output += "\n=== Scalar query example: ";

			// create a connection string for the database
			var connectionString = string.Format ("Data Source={0};Version=3;", path);
			try {
				using (var conn = new SqliteConnection ((connectionString))) {
					conn.Open();
					using (var command = conn.CreateCommand ())
					{
						command.CommandText = "SELECT COUNT(*) FROM [People] ";
						var i = command.ExecuteScalar ();
						output += "\nExecuting a scalar query";
						output += "\n\tResult=" + i;
					
					}
					conn.Close ();
					return output;
					}


			} catch (Exception ex) {
				var reason = string.Format ("Failed to count from Table People - reason = {0}", ex.Message);
				return reason;
			}

		}		
		public static string deleteTable (string path)
		{
			var output = "";
			output += "\n=== Scalar query example: ";

			// create a connection string for the database
			var connectionString = string.Format ("Data Source={0};Version=3;", path);
			try {
				using (var conn = new SqliteConnection ((connectionString))) {
					conn.Open ();
					using (var command = conn.CreateCommand ())
					{
						command.CommandText = "DROP TABLE [People] ";
						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery (); // rowcount will be 1

					}
					conn.Close ();
					return "People Table deleted successfully";

				}


			} catch (Exception ex) {
				var reason = string.Format ("Failed to delete Table People - reason = {0}", ex.Message);
				return reason;
			}

		}

		public static string deleteDatabase (string path)
		{
			

			// create a connection string for the database
			var connectionString = string.Format ("Data Source={0};Version=3;", path);
			try {
				using (var conn = new SqliteConnection ((connectionString))) {
					conn.Open ();
					using (var command = conn.CreateCommand ())
					{
						command.CommandText = "DROP DATABASE Simpledb_adonet.db";
						command.CommandType = CommandType.Text;
						command.ExecuteNonQuery (); // rowcount will be 1

						conn.Close ();
						return "Database deleted successfully";;
					}

				}


			} catch (Exception ex) {
				var reason = string.Format ("Failed to delete database - reason = {0}", ex.Message);
				return reason;
			}

		}	

		#region "Using Parameters"
		public static string DeletePeopleDetails(string path, int id) 
		{
			var connectionString = string.Format ("Data Source={0};Version=3;", path);
			try {
				using (var conn = new SqliteConnection ((connectionString))) {
					conn.Open ();
					using (var command = conn.CreateCommand ())
					{
						command.CommandText = "DELETE FROM [People] WHERE [PersonID] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
						command.ExecuteNonQuery (); // rowcount will be 1

					}
					conn.Close ();
					return "Deleted from People successfully";;
				}
			
			} catch (Exception ex) {
				var reason = string.Format ("Failed to delete from people - reason = {0}", ex.Message);
				return reason;
			}
		}




		#endregion "Using Parameters"
	}
}


