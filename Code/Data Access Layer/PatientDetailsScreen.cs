
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
		EditText editDateOfBirth;
		EditText editPatAge;
		RadioButton rbGenderMale;
		RadioButton rbGenderFemale;
		RadioButton rbGenderOther;

		EditText editNextOfKin;
		EditText editNOKContact;
		EditText editLocation;
		EditText editGridReference;
		EditText editPatInfo;
		CheckBox checkBoxTransferred;

		TextView txtPatID;

		Button buttonSave;
		Button buttonCancel;

		Button buttonSampleHistory;

		Button buttonTrauma;
		Button buttonMedical;
		Button buttonVital;
		Button buttonHandover;

	



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
			editDateOfBirth = FindViewById <EditText > (Resource.Id.editDateOfBirth);
			editPatAge = FindViewById <EditText > (Resource.Id.editPatAge);
			rbGenderMale = FindViewById <RadioButton > (Resource.Id.rbGenderMale);
			rbGenderFemale = FindViewById <RadioButton > (Resource.Id.rbGenderFemale);
			rbGenderOther = FindViewById <RadioButton > (Resource.Id.rbGenderOther);

			editNextOfKin = FindViewById <EditText > (Resource.Id.editNextOfKin);
			editNOKContact = FindViewById <EditText > (Resource.Id.editNOKContactDetails);
			editLocation = FindViewById <EditText > (Resource.Id.editLocation);
			editGridReference = FindViewById <EditText > (Resource.Id.editGridReference);
			editPatInfo = FindViewById <EditText > (Resource.Id.editPatOtherInfo);
			buttonSave = FindViewById <Button> (Resource.Id.buttonSave);
			txtPatID = FindViewById <TextView > (Resource.Id.textViewPatientID );



			checkBoxTransferred = FindViewById <CheckBox > (Resource.Id.chkTransferred ); 
			checkBoxTransferred.Checked = patient.Transferred ;

			//spinHandover = FindViewById <Spinner > (Resource.Id.spinnerHandover);

		

//				spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
//				var adapter = ArrayAdapter.CreateFromResource (
//					this, Resource.Array.HandoverAgency , Android.Resource.Layout.SimpleSpinnerItem);
//
//				adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
//				spinner.Adapter = adapter;
	

		

			// set the cancel delete based on whether or not it's an existing task
			buttonCancel = FindViewById <Button> (Resource.Id.buttonCancel);
			buttonCancel .Text = (patient.PatientID == 0 ? "Cancel" : "Delete");


			editPatName.Text = patient.PatientFirstName; 
			editPatSurname.Text = patient.PatientLastName;
			editDateOfBirth.Text = patient.PatientDateOfBirth;
			editPatAge.Text = Convert.ToString(patient.PatientAge); 
			//check to see if existing patient
			if (patientID > 0) {
				var PatientText = "Patient ID: ";
				txtPatID.Text = PatientText + Convert.ToString (patient.PatientID);
			}

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

			editNextOfKin.Text = patient.NextOfKin;
			editNOKContact.Text = patient.NOKContactDetails;
			editLocation.Text = patient.Location;
			editGridReference.Text = patient.GridReference;
			editPatInfo.Text = patient.PatientInfo;
			//spinHandover.SelectedItem    = patient.HandOverTo;
		


			buttonCancel .Click += (sender, e) => { CancelDelete(); };

			buttonSave.Click += (sender, e) => 
			{ Save(); };
	
			buttonSampleHistory = FindViewById<Button> (Resource.Id.btnSample );

			// wire up add task button handler
			if(buttonSampleHistory  != null) {
				buttonSampleHistory .Click += (sender, e) => {
					var sample = new Intent (this, typeof (SampleHistoryScreen ));
					sample .PutExtra ("PatientID", patientID );
					StartActivity(sample );
				};
			}
		
			buttonTrauma = FindViewById<Button> (Resource.Id.buttonTrauma);

			// wire up add task button handler
			if(buttonTrauma  != null) {
				buttonTrauma .Click += (sender, e) => {
					var trauma = new Intent (this, typeof (TraumaScreen  ));
					trauma .PutExtra ("PatientID", patientID );
					StartActivity(trauma );
				};
			}

			buttonMedical = FindViewById <Button> (Resource.Id.buttonMedical);
			// wire up add task button handler
			if(buttonMedical  != null) {
				buttonMedical .Click += (sender, e) => {
					var med = new Intent (this, typeof (Medicals   ));
					med .PutExtra ("PatientID", patientID );
					StartActivity(med );
				};
			}

			buttonVital = FindViewById <Button> (Resource.Id.buttonVitals);
			// wire up add task button handler
			if(buttonVital  != null) {
				buttonVital .Click += (sender, e) => {
					var vit = new Intent (this, typeof (VitalsScreen   ));
					vit .PutExtra ("PatientID", patientID );
					StartActivity(vit );
				};
			}

			buttonHandover = FindViewById <Button> (Resource.Id.btnHandover );
			// wire up add task button handler
			if(buttonHandover  != null) {
				buttonHandover.Click += (sender, e) => {
					var hand = new Intent (this, typeof (Handover    ));
					hand .PutExtra ("PatientID", patientID );
					StartActivity(hand );
				};
			}

		}

		void Save()
		{
			patient.PatientFirstName = editPatName.Text;
			patient.PatientLastName = editPatSurname.Text;
			patient.PatientDateOfBirth = editDateOfBirth.Text ;
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
					
			patient.NextOfKin = editNextOfKin.Text;
			patient.NOKContactDetails = editNOKContact.Text;
			patient.Location = editLocation.Text;
			patient.GridReference = editGridReference.Text;
			patient.PatientInfo= editPatInfo .Text;
			patient.Transferred  = checkBoxTransferred.Checked;

			//Save initial Start Assessment Date and Time


			if (patient.PatientID == 0) {
				patient.AssessStartDate = DateTime.Today.ToString("dd-MM-yyyy");
				patient.AssessStartTime = DateTime.Now.ToString ("HH:mm:ss");
			}


			//Save Handover date and time if Transferred ticked
			if ((patient.PatientID == 0) & (patient.Transferred ==true)){
				//patient.HandOverTo = spinHandover.SelectedItem;
				patient.HandOverDate  = DateTime.Today.ToString("dd-MM-yyyy");
				patient.HandOverTime  = DateTime.Now.ToString ("HH:mm:ss");
			}


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






