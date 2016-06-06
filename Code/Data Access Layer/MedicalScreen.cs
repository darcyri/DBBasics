
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
	[Activity (Label = "MedicalScreen")]			
	public class Medicals : Activity
	{
		PatientDetails patient = new PatientDetails ();
		TextView txtPatID;
		CheckBox chkChiefComplaint;
		EditText editMedType;
		EditText editMedDesc;
		EditText editMedLocation;
		EditText editMedIntervention;
		EditText editMedMedAdmin;
		EditText editPainO;
		EditText editPainP;
		EditText editPainQ;
		EditText editPainR;
		EditText editPainS;
		EditText editPainT;

		Button buttonSaveMedical;
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
			SetContentView(Resource.Layout.Medicals  );
			txtPatID = FindViewById <TextView > (Resource.Id.txtPatientID );


			chkChiefComplaint = FindViewById <CheckBox > (Resource.Id.checkChief);


			editMedType  = FindViewById<EditText>(Resource.Id.editType  );
			editMedDesc   = FindViewById <EditText > (Resource.Id.editDescription  );
			editMedLocation = FindViewById <EditText > (Resource.Id.editLocation   );
			editMedIntervention   = FindViewById <EditText > (Resource.Id.editIntervention  );
			editMedMedAdmin   = FindViewById <EditText > (Resource.Id.editMedications  );
			editPainO   = FindViewById <EditText > (Resource.Id.editO  );
			editPainP  = FindViewById <EditText > (Resource.Id.editP  );
			editPainQ   = FindViewById <EditText > (Resource.Id.editQ  );
			editPainR  = FindViewById <EditText > (Resource.Id.editR  );
			editPainS   = FindViewById <EditText > (Resource.Id.editS  );
			editPainT  = FindViewById <EditText > (Resource.Id.editT );


			buttonSaveMedical=FindViewById <Button>(Resource .Id.buttonSave );

			txtPatID.Text =Convert.ToString( patient.PatientID);
			chkChiefComplaint.Checked = patient.MedChiefComplaint;
			editMedType.Text  = patient  .MedType ;
			editMedDesc.Text  =patient .MedDesc;
			editMedLocation.Text  = patient .MedLocation;
			editMedIntervention.Text  = patient .MedInterventions;
			editMedMedAdmin.Text  = patient .MedMedAdmin;
			editPainO.Text  = patient.Onset ;
			editPainP.Text  =patient.Provokes ;
			editPainQ.Text  = patient.Quality  ;
			editPainR.Text  =patient.Radiates  ;
			editPainS.Text  = patient.Severity;
			editPainT.Text  =patient.Time  ;


			//buttonCancel .Click += (sender, e) => { CancelDelete(); };

			buttonSaveMedical.Click += (sender, e) => 
			{ Save(); };




		}

		void Save()
		{

			patient .PatientID =Convert.ToInt32 ( txtPatID.Text);
			patient.MedChiefComplaint = chkChiefComplaint.Checked;
			patient  .MedType = editMedType .Text ;
			patient .MedDesc = editMedDesc.Text;
			patient .MedLocation  = editMedLocation.Text;
			patient .MedInterventions = editMedIntervention.Text;
			patient .MedMedAdmin = editMedMedAdmin.Text;
			patient .Onset  = editPainO.Text;
			patient.Provokes  = editPainP.Text; 
			patient.Quality   = editPainQ.Text;
			patient .Radiates  = editPainR.Text;
			patient.Severity = editPainS.Text;
			patient.Time   = editPainT.Text;

			PatientDetailsManager.SavePatient(patient );
			Finish();
		}
	}
}


