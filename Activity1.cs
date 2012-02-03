using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;

namespace TestAccelerometer
{
	[Activity (Label = "TestAccelerometer", MainLauncher = true)]
	public class Activity1 : Activity
	{
		TextView textX;
		TextView textY;
		TextView textZ;
		
		SensorManager sensorManager;
		SensorListener sensorListener;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			
			textX = FindViewById<TextView> (Resource.Id.textX);
			textY = FindViewById<TextView> (Resource.Id.textY);
			textZ = FindViewById<TextView> (Resource.Id.textZ);
			
			sensorManager = (SensorManager)GetSystemService(Context.SensorService);
			sensorListener = new SensorListener(this);
		}
		
		
		protected override void OnResume ()
		{
			base.OnResume ();
			sensorManager.RegisterListener(sensorListener, sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Normal);
			
		}
		
		protected override void OnPause ()
		{
			base.OnPause ();
			sensorManager.UnregisterListener(sensorListener);
		}
		
		
		class SensorListener : Java.Lang.Object, ISensorEventListener{
			
			Activity1 _activity;
			DateTime created;
			
			public SensorListener(Activity1 activity)
			{
				_activity = activity;
				created = DateTime.Now;
			}
			
			
			
			public void OnSensorChanged (SensorEvent e)
			{
				var val = e.Values;
				Android.Util.Log.Info("-------------","Works for: " + (DateTime.Now - created));
				Android.Util.Log.Info("-------------",string.Format("X: {0} Y: {1} Z: {2}", val[0], val[1], val[2] ));
				
				_activity.RunOnUiThread(() => {
					_activity.textX.Text = string.Format("X: {0}", val[0]);
					_activity.textY.Text = string.Format("Y: {0}", val[1]);
					_activity.textZ.Text = string.Format("Z: {0}", val[2]);
				});
			}
			
			
			
			
			public void OnAccuracyChanged (Sensor sensor, int accuracy)
			{
				
			}
			
		}
	}
}


