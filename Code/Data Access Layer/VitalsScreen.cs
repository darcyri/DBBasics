

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
	[Activity (Label = "Vitals")]			
	public class VitalsScreen : Activity
	{
		PatientDetails patient = new PatientDetails ();
		TextView txtPatID;

		EditText editPulseRate;
		EditText editPulseQuality;
		EditText editBloodPressure;
		EditText editRespRate;
		EditText editRespQuality;
		EditText editSPO;
		EditText editTemperature;
		EditText editColour;
		EditText editCSM;
		EditText editBloodSugar;

		RadioButton rbAlert;
		RadioButton rbVerbal;
		RadioButton rbPain;
		RadioButton rbUncon;
		RadioButton rbPERLL;
		RadioButton rbNotPerll;
		RadioButton rbStable;
		RadioButton rbUnstable;



		Button buttonSaveVital;
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
			SetContentView(Resource.Layout.VitalsScreen );
			txtPatID = FindViewById <TextView > (Resource.Id.textViewPatientID );
			editPulseRate   = FindViewById<EditText>(Resource.Id.editPulseRate );
			editPulseQuality  = FindViewById <EditText > (Resource.Id.editPulseQuality  );
			editBloodPressure  = FindViewById <EditText > (Resource.Id.editBloodPressure  );
			editRespRate  = FindViewById <EditText > (Resource.Id.editRespiratoryRate  );
			editRespQuality  = FindViewById <EditText > (Resource.Id.editRespiratoryQuality );
			editSPO  = FindViewById <EditText > (Resource.Id.editSPO );
			editTemperature  = FindViewById <EditText > (Resource.Id.editTemperature  );
			editColour  = FindViewById <EditText > (Resource.Id.editColour );
			editCSM  = FindViewById <EditText > (Resource.Id.editCSM );
			editBloodSugar  = FindViewById <EditText > (Resource.Id.editBloodSugar );

			rbAlert = FindViewById <RadioButton > (Resource.Id.rbAlert);
			rbVerbal = FindViewById <RadioButton > (Resource.Id.rbVerbal );
			rbPain = FindViewById <RadioButton > (Resource.Id.rbPain);
			rbUncon = FindViewById <RadioButton > (Resource.Id.rbUnresponsive );
			rbPERLL = FindViewById <RadioButton > (Resource.Id.rbPERLL );
			rbNotPerll  = FindViewById <RadioButton > (Resource.Id.rbNotPerll );
			rbStable  = FindViewById <RadioButton > (Resource.Id.rbStable );
			rbUnstable  = FindViewById <RadioButton > (Resource.Id.rbUnstable );


			// set the cancel delete based on whether or not it's an existing task
			//buttonCancel = FindViewById <Button> (Resource.Id.buttonCancel);
			//buttonCancel .Text = (patient.PatientID == 0 ? "Cancel" : "Delete");
			buttonSaveVital =FindViewById <Button>(Resource .Id.buttonSave );

			editPulseRate.Text = patient.PulseRate ; 
			editPulseQuality .Text = patient.PulseQuality ;
			editBloodPressure  .Text = patient.BloodPressure  ;
			editRespRate  .Text = patient.RespiratoryRate  ;
			editRespQuality  .Text = patient.RespiratoryQuality  ;
			editSPO  .Text = patient.SPO ;
			editTemperature  .Text = patient.Temperature  ;
			editColour  .Text = patient.Colour  ;
			editCSM  .Text = patient.CSM ;
			editBloodSugar  .Text = patient.BloodSugar   ;

			//LOC

			if (patient.LOC  =="Alert")
			{

				rbAlert .PerformClick ()  ;
			}
			else if (patient.LOC =="Verbal")
			{
				rbVerbal .PerformClick() ;
			}
			else if (patient.LOC =="Pain")
			{
				rbPain.PerformClick() ;
			}
			else if (patient.LOC =="Unresponsive")
			{
				rbUncon .PerformClick() ;
			}

			//Pupils
			if (patient.Pupils   =="PERLL")
			{

				rbPERLL  .PerformClick ()  ;
			}
			else if (patient.Pupils  =="Not PERLL")
			{
				rbNotPerll .PerformClick() ;
			}

			//Status
			if (patient.Status    =="Stable")
			{

				rbStable   .PerformClick ()  ;
			}
			else if (patient.Status   =="Unstable")
			{
				rbUnstable  .PerformClick() ;
			}

			txtPatID.Text = Convert.ToString (patient.PatientID) ;
			//buttonCancel .Click += (sender, e) => { CancelDelete(); };

		



			buttonSaveVital.Click += (sender, e) => 
			{ Save(); };

					}

		void Save()
		{
			patient.PatientID =Convert.ToInt32 ( txtPatID.Text);
			patient.PulseRate  = editPulseRate .Text;
			patient.PulseQuality   = editPulseQuality  .Text;
			patient.BloodPressure  = editBloodPressure  .Text;
			patient.RespiratoryRate   = editRespRate  .Text ;
			patient.RespiratoryQuality   = editRespQuality  .Text;
			patient.SPO = editSPO.Text;
			patient.Temperature = editTemperature.Text;
			patient.Colour = editColour.Text;
			patient.CSM = editCSM.Text;
			patient.BloodSugar = editBloodSugar.Text;


			//LOC

			if (rbAlert .Checked)
			{
				patient.LOC  =rbAlert .Text;
			}
			else if (rbVerbal.Checked)
			{
				patient.LOC = rbVerbal.Text;
			}
			else if (rbPain .Checked)
			{
				patient.LOC = rbPain.Text;
			}
			else if (rbUncon .Checked)
			{
				patient.LOC = rbUncon .Text;
			}

			//Pupils

			if (rbPERLL.Checked)
			{
				patient.Pupils   =rbPERLL  .Text;
			}
			else if (rbNotPerll .Checked)
			{
				patient.Pupils  = rbPERLL .Text;
			}
			//Status
			if (rbStable.Checked)
			{
				patient.Status   =rbStable  .Text;
			}
			else if (rbUnstable  .Checked)
			{
				patient.Status  = rbUnstable .Text;
			}


			patient.VitalSetDate  = DateTime.Today.ToString("dd-MM-yyyy");
			patient.VitalSetTime  = DateTime.Now.ToString ("HH:mm:ss");



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


