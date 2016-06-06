
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

namespace DBBasics
{
	[Activity (Label = "SampleHistoryScreen")]			
	public class SampleHistoryScreen : Activity
	{
		PatientDetails patient = new PatientDetails ();
		TextView txtPatID;

		EditText editSigns;
		EditText editAllergies;
		EditText editMedications;
		EditText editPastHistory;
		EditText editLastIntake;
		EditText editEvent;
		EditText editMOI;

		Button buttonSaveSample;
		//Button buttonCancel;




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
			SetContentView(Resource.Layout.SampleHistoryScreen );
			txtPatID = FindViewById <TextView > (Resource.Id.textViewPatientID );
			editSigns   = FindViewById<EditText>(Resource.Id.editSigns  );
			editAllergies  = FindViewById <EditText > (Resource.Id.editAllergies );
			editMedications  = FindViewById <EditText > (Resource.Id.editMedications );
			editPastHistory  = FindViewById <EditText > (Resource.Id.editPastHistory );
			editLastIntake  = FindViewById <EditText > (Resource.Id.editLastIntake );
			editEvent  = FindViewById <EditText > (Resource.Id.editEvent );
			editMOI  = FindViewById <EditText > (Resource.Id.editMOI );

					// set the cancel delete based on whether or not it's an existing task
			//buttonCancel = FindViewById <Button> (Resource.Id.buttonCancel);
			//buttonCancel .Text = (patient.PatientID == 0 ? "Cancel" : "Delete");
			buttonSaveSample =FindViewById <Button>(Resource .Id.buttonSave );

			editSigns.Text = patient.Signs ; 
			editAllergies .Text = patient.Allergies ;
			editMedications .Text = patient.Medications ;
			editPastHistory .Text = patient.PastHistory ;
			editLastIntake .Text = patient.LastIntake ;
			editEvent .Text = patient.Event ;
			editMOI .Text = patient.MOI ;
			txtPatID.Text = Convert.ToString (patient.PatientID) ;
			//buttonCancel .Click += (sender, e) => { CancelDelete(); };

			buttonSaveSample.Click += (sender, e) => 
			{ Save(); };




		}

		void Save()
		{
			patient.PatientID =Convert.ToInt32 ( txtPatID.Text);
			patient.Signs = editSigns.Text;
			patient.Allergies  = editAllergies .Text;
			patient.Medications  = editMedications .Text;
			patient.PastHistory  = editPastHistory .Text ;
			patient.LastIntake  = editLastIntake .Text;
			patient.Event  = editLastIntake .Text;
			patient.MOI  = editMOI .Text;
			PatientDetailsManager.SavePatient(patient);
			Finish();
		}

//		void CancelDelete()
//		{
//			if (patient.PatientID != 0) {
//				PatientDetailsManager.DeletePatient(patient.PatientID);
//			}
//			Finish();
//		}

		//		
	}
}

