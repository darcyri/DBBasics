using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;


namespace DBBasics
{
	/// <summary>
	/// Adapter that presents Patients in a row-view
	/// </summary>
	public class PatientListAdapter : BaseAdapter<PatientDetails > 
	{
		Activity context = null;
		IList<PatientDetails> patients = new List<PatientDetails>();

		public PatientListAdapter (Activity context, IList<PatientDetails > patients) : base ()
		{
			this.context = context;
			this.patients  = patients ;
		}

		public override PatientDetails  this[int position]
		{
			get { return patients [position]; }
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count
		{
			get { return patients.Count; }
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get our object for position
			var item = patients[position];			

			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// gives us some performance gains by not always inflating a new view
			// will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()

			//			var view = (convertView ?? 
			//					context.LayoutInflater.Inflate(
			//					Resource.Layout.TaskListItem, 
			//					parent, 
			//					false)) as LinearLayout;
			//			// Find references to each subview in the list item's view
			//			var txtName = view.FindViewById<TextView>(Resource.Id.NameText);
			//			var txtDescription = view.FindViewById<TextView>(Resource.Id.NotesText);
			//			//Assign item's values to the various subviews
			//			txtName.SetText (item.Name, TextView.BufferType.Normal);
			//			txtDescription.SetText (item.Notes, TextView.BufferType.Normal);

//			// TODO: use this code to populate the row, and remove the above view
//			var view = (convertView ??
//				context.LayoutInflater.Inflate(
//					Android.Resource.Layout.SimpleListItem1 ,
//					parent,
//					false)) as LinearLayout ;
//			var txtName = view.FindViewById <TextView > (Resource.Id.textPatientName);
//			txtName.SetText (item.PatientFirstName, TextView.BufferType.Normal);


			var view = (convertView ??
				context.LayoutInflater.Inflate(
					Android.Resource.Layout.SimpleListItemChecked ,
					parent,
					false)) as CheckedTextView ;
			view.SetText (item.PatientFirstName ==""?"<new patient>":item.PatientFirstName , TextView.BufferType.Normal);
			view.Checked = item.Transferred ;
			//Finally return the view
			return view;
		}
	}
}