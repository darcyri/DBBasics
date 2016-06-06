
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
	[Activity (Label = "TraumaScreen")]			
	public class TraumaScreen : Activity
	{
		PatientDetails patient = new PatientDetails ();
		TextView txtPatID;
		CheckBox chkChiefComplaint;
		EditText editTraumaType;
		EditText editTraumaDesc;
		EditText editTraumaLocation;
		EditText editTraumaIntervention;
		EditText editTraumaMedAdmin;
		EditText editPainBefore;
		EditText editPainAfter;

		Button buttonSaveTrauma;
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
			SetContentView(Resource.Layout.Traumas  );
			txtPatID = FindViewById <TextView > (Resource.Id.txtPatientID );
	

			chkChiefComplaint = FindViewById <CheckBox > (Resource.Id.checkChief);


			editTraumaType  = FindViewById<EditText>(Resource.Id.editType  );
			editTraumaDesc   = FindViewById <EditText > (Resource.Id.editDescription  );
			editTraumaLocation = FindViewById <EditText > (Resource.Id.editLocation   );
			editTraumaIntervention   = FindViewById <EditText > (Resource.Id.editIntervention  );
			editTraumaMedAdmin   = FindViewById <EditText > (Resource.Id.editMedications  );
			editPainBefore   = FindViewById <EditText > (Resource.Id.editPainBefore  );
			editPainAfter  = FindViewById <EditText > (Resource.Id.editPainAfter  );
		
			buttonSaveTrauma=FindViewById <Button>(Resource .Id.buttonSave );

			txtPatID.Text =Convert.ToString( patient.PatientID);
			chkChiefComplaint.Checked = patient .TraumaChiefComplaint;
			editTraumaType.Text  = patient  .TraumaType ;
			editTraumaDesc.Text  =patient .TraumaDesc;
			editTraumaLocation.Text  = patient .TraumaLocation;
			editTraumaIntervention.Text  = patient .TraumaInterventions;
			editTraumaMedAdmin.Text  = patient .TraumaMedAdmin;
			editPainBefore.Text  = patient .PainScaleBefore;
			editPainAfter.Text  =patient .PainScaleAfter;
	
				
			//buttonCancel .Click += (sender, e) => { CancelDelete(); };

			buttonSaveTrauma.Click += (sender, e) => 
			{ Save(); };




		}

		void Save()
		{
			
			patient .PatientID =Convert.ToInt32 ( txtPatID.Text);
			patient.TraumaChiefComplaint = chkChiefComplaint.Checked;
			patient  .TraumaType = editTraumaType .Text ;
			patient .TraumaDesc = editTraumaDesc.Text;
			patient .TraumaLocation = editTraumaLocation.Text;
			patient .TraumaInterventions = editTraumaIntervention.Text;
			patient .TraumaMedAdmin = editTraumaMedAdmin.Text;
			patient .PainScaleBefore = editPainBefore.Text;
			patient.PainScaleAfter = editPainAfter.Text; 
			PatientDetailsManager.SavePatient(patient );
			Finish();
		}
	}
}

