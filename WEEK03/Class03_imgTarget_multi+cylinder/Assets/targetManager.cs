using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class targetManager : MonoBehaviour
{

	private bool Target1, Target2 = false;
	private Vector3 Target1_POS, Target2_POS;

    // create a line renderer for our line
	private LineRenderer line;


	void Start()
	{
        // set up the line renderer
		line = gameObject.AddComponent<LineRenderer>();
		line.widthMultiplier = .25f;
		line.positionCount = 2;
		line.material = new Material(Shader.Find("Mobile/Particles/Additive"));
		line.startColor = Color.blue;
		line.endColor = Color.red;
	}

	void Update()
	{

        // get an instance of the stateManager Singleton
        // to know more about singletons:
        // https://en.wikipedia.org/wiki/Singleton_pattern

		StateManager sm = TrackerManager.Instance.GetStateManager();

        // Get all our trackable behaviours
		IEnumerable<TrackableBehaviour> allTrackables = sm.GetTrackableBehaviours();

        // loop the trackables and get their data - status, name, position,...
		foreach (TrackableBehaviour tb in allTrackables)
		{

			if (tb.CurrentStatus == TrackableBehaviour.Status.DETECTED ||
			   tb.CurrentStatus == TrackableBehaviour.Status.TRACKED)
			{
                // 
				//				Debug.Log ("--> Trackable is  " + tb.TrackableName + " :: " + tb.CurrentStatus);
			}


            // validate target 1
			if (tb.TrackableName == "stones")
			{
				if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED)
				{
					Target1 = true;
					Target1_POS = tb.transform.position;
				}
				else
				{
					Target1 = false;
				}
				//Debug.Log ("---> Stones  " + Target1);

			}

			// validate target 2
			if (tb.TrackableName == "chips")
			{
				if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED)
				{
					Target2 = true;
					Target2_POS = tb.transform.position;
				}
				else
				{
					Target2 = false;
				}
				//Debug.Log ("---> Chips  " + Target2);

			}
		}

		if (Target1 == true && Target2 == true)
		{
			Debug.Log("!! yay we're tracking 2 targets !!");
			line.enabled = true;
			var points = new Vector3[]{Target1_POS, Target2_POS};
			line.SetPositions(points);
		}
		else
		{
			line.enabled = false;
			Debug.Log("!! nay we're NOT !!");

		}
	}
}
