
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech;


namespace DBBasics
{
	[Activity (Label = "PatientDetails")]			
	public class PatientDetailsScreen : Activity
	{
		PatientDetails patient = new PatientDetails ();
		EditText editPatName;
		EditText editPatSurname;
		EditText editPatAge;
		RadioButton rbGenderMale;
		RadioButton rbGenderFemale;
		RadioButton rbGenderOther;
		EditText editPatInfo;
		CheckBox checkBoxTransferred;
		Button buttonSave;
		Button buttonCancel;




				protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// set the isRecording flag to false (not recording)
		//	isRecording = false;

			int patientID = Intent.GetIntExtra("PatientID", 0);
			if(patientID > 0) {
				patient = PatientDetailsManager.GetPatient(patientID);
			}


			// set our layout to be the home screen
			SetContentView(Resource.Layout.PatientDetails);
			editPatName  = FindViewById<EditText>(Resource.Id.editPatName );
			editPatSurname = FindViewById <EditText > (Resource.Id.editPatSurname);
			editPatAge = FindViewById <EditText > (Resource.Id.editPatAge);
			rbGenderMale = FindViewById <RadioButton > (Resource.Id.rbGenderMale);
			rbGenderFemale = FindViewById <RadioButton > (Resource.Id.rbGenderFemale);
			rbGenderOther = FindViewById <RadioButton > (Resource.Id.rbGenderOther);
			editPatInfo = FindViewById <EditText > (Resource.Id.editPatOtherInfo);
			buttonSave = FindViewById <Button> (Resource.Id.buttonSave);
		


			checkBoxTransferred = FindViewById <CheckBox > (Resource.Id.chkTransferred ); 
			checkBoxTransferred.Checked = patient.Transferred ;

								

			// set the cancel delete based on whether or not it's an existing task
			buttonCancel = FindViewById <Button> (Resource.Id.buttonCancel);
			buttonCancel .Text = (patient.PatientID == 0 ? "Cancel" : "Delete");


			editPatName.Text = patient.PatientFirstName; 
			editPatSurname.Text = patient.PatientLastName;
			editPatAge.Text = Convert.ToString(patient.PatientAge); 


			//gender
	

			if (patient.PatientGender =="Male")
			{

				rbGenderMale.PerformClick ()  ;
			}
			else if (patient.PatientGender =="Female")
			{
				rbGenderFemale.PerformClick() ;
			}
			else if (patient.PatientGender =="Other")
			{
				rbGenderOther.PerformClick() ;
			}


			editPatInfo.Text = patient.PatientInfo;

		
			//Voice Recognition

			buttonCancel .Click += (sender, e) => { CancelDelete(); };

			buttonSave.Click += (sender, e) => 
			{ Save(); };
	

		

		}

		void Save()
		{
			patient.PatientFirstName = editPatName.Text;
			patient.PatientLastName = editPatSurname.Text;
			patient.PatientAge = Convert.ToInt32 (editPatAge.Text);


			if (rbGenderMale.Checked)
			{
				patient.PatientGender =rbGenderMale.Text;
			}
			else if (rbGenderFemale.Checked)
			{
				patient.PatientGender = rbGenderFemale.Text;
			}
			else if (rbGenderOther.Checked)
			{
				patient.PatientGender = rbGenderOther.Text;
			}
					
			patient.PatientInfo= editPatInfo .Text;
			patient.Transferred  = checkBoxTransferred.Checked;



			PatientDetailsManager.SavePatient(patient);
			Finish();
		}

		void CancelDelete()
		{
			if (patient.PatientID != 0) {
				PatientDetailsManager.DeletePatient(patient.PatientID);
			}
			Finish();
		}

		//		
	}
}






