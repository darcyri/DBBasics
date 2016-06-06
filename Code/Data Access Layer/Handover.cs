
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
	[Activity (Label = "HandOver")]			
	public class Handover : Activity
	{
		PatientDetails patient = new PatientDetails ();
		TextView txtPatID;
		TextView txtIdentification;
		TextView txtMOI;
		TextView txtInjuries;
		TextView txtSigns;
		TextView txtTreatment;

		TextView txtAllergies;
		TextView txtMedications;
		TextView txtBackground;
		TextView txtOtherInfo;

		//Handover agency
		RadioButton rbHSE;
		RadioButton rbICG;
		RadioButton rbAGS;
		RadioButton rbOther;


		Button buttonSaveHandover;
		//Button buttonCancel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			int patientID = Intent.GetIntExtra("PatientID", 0);
			if(patientID > 0) {
				patient = PatientDetailsManager.GetPatient(patientID);
			}


			// set our layout to be the home screen
			SetContentView(Resource.Layout.HandOver  );
			txtPatID = FindViewById <TextView > (Resource.Id.txtPatientID );
			txtIdentification  = FindViewById <TextView > (Resource.Id.txtIdentification  );
			txtMOI  = FindViewById <TextView > (Resource.Id.txtMOI  );
			txtInjuries  = FindViewById <TextView > (Resource.Id.txtInjuries  );
			txtSigns = FindViewById <TextView > (Resource.Id.txtSigns );
			txtTreatment  = FindViewById <TextView > (Resource.Id.txtTreatment  );
			txtAllergies  = FindViewById <TextView > (Resource.Id.txtAllergies );
			txtMedications  = FindViewById <TextView > (Resource.Id.txtMedication   );
			txtBackground  = FindViewById <TextView > (Resource.Id.txtBackground  );
			txtOtherInfo  = FindViewById <TextView > (Resource.Id.txtOtherInfo  );
			rbHSE  = FindViewById <RadioButton > (Resource.Id.rbHSE);
			rbICG = FindViewById <RadioButton > (Resource.Id.rbICG);
			rbAGS = FindViewById <RadioButton > (Resource.Id.rbAGS);
			rbOther = FindViewById <RadioButton > (Resource.Id.rbOther);



			// set the cancel delete based on whether or not it's an existing task
			//buttonCancel = FindViewById <Button> (Resource.Id.buttonCancel);
			//buttonCancel .Text = (patient.PatientID == 0 ? "Cancel" : "Delete");


			txtIdentification.Text  = patient.PatientFirstName + " " + patient.PatientLastName;
			txtMOI.Text  = patient.MOI;
			txtInjuries.Text  = patient.TraumaType + "," + patient.MedType;
			txtSigns.Text  = patient.Signs;
			txtTreatment.Text  = patient.TraumaInterventions + "," + patient.MedInterventions;
			txtAllergies.Text  = patient.Allergies;
			txtMedications.Text  = patient.Medications;
			txtBackground.Text  = patient.PastHistory + "," + patient.Event;
			txtOtherInfo.Text  = patient.PatientInfo;
		
			txtPatID.Text = Convert.ToString (patient.PatientID) ;
			//buttonCancel .Click += (sender, e) => { CancelDelete(); };
			buttonSaveHandover =FindViewById <Button>(Resource .Id.buttonSave );
			if (patient.HandOverTo  =="HSE Ambulance")
			{

				rbHSE .PerformClick ()  ;
			}
			else if (patient.HandOverTo  =="ICG Helicopter")
			{
				rbICG .PerformClick() ;
			}
			else if (patient.HandOverTo  =="An Garda Siochana")
			{
				rbAGS .PerformClick() ;
			}
			else if (patient.HandOverTo  =="Other/None")
			{
				rbOther  .PerformClick() ;
			}


			buttonSaveHandover.Click += (sender, e) => 
			{ Save(); };




		}

		void Save()
		{
			//Only save handover details
			patient.PatientID =Convert.ToInt32 ( txtPatID.Text);

			if (rbHSE .Checked)
			{
				patient.HandOverTo  =rbHSE .Text;
			}
			else if (rbICG .Checked)
			{
				patient.HandOverTo  = rbICG .Text;
			}
			else if (rbAGS .Checked)
			{
				patient.HandOverTo  = rbAGS .Text;
			}
			else if (rbOther  .Checked)
			{
				patient.HandOverTo  = rbOther  .Text;
			}

			patient.HandOverDate  = DateTime.Today.ToString("dd-MM-yyyy");
			patient.HandOverTime  = DateTime.Now.ToString ("HH:mm:ss");


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



