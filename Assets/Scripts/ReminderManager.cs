using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ReminderManager : MonoBehaviour {

	private int i;

	public List<Vector3> locs1 = new List<Vector3>();
	public List<Vector3> rots1 = new List<Vector3>();

	public List<Vector3> locs2 = new List<Vector3>();
	public List<Vector3> rots2 = new List<Vector3>();

	public List<Vector3> locs3 = new List<Vector3>();
	public List<Vector3> rots3 = new List<Vector3>();

	public List<Vector3> locs4 = new List<Vector3>();
	public List<Vector3> rots4 = new List<Vector3>();


	// Use this for initialization
	void Awake ()
	{
		// Ring 1
		locs1.Add (new Vector3 (105, 72.5f, 105)); // 1
		locs1.Add (new Vector3 (105, 72.5f, 115)); // 2
		locs1.Add (new Vector3 (105, 72.5f, 125)); // 3
		locs1.Add (new Vector3 (105, 72.5f, 135)); // 4
		locs1.Add (new Vector3 (105, 72.5f, 145)); // 5
		locs1.Add (new Vector3 (105, 72.5f, 155)); // 6
		locs1.Add (new Vector3 (105, 72.5f, 165)); // 7
		locs1.Add (new Vector3 (105, 72.5f, 175)); // 8
        locs1.Add(new Vector3(105, 72.5f, 185)); // 9
        locs1.Add(new Vector3(105, 72.5f, 195)); // 10

        rots1.Add (new Vector3 (0, 0, 0)); // 1
		rots1.Add (new Vector3 (0, 0, 0)); // 2
		rots1.Add (new Vector3 (0, 0, 0)); // 3
		rots1.Add (new Vector3 (0, 0, 0)); // 4
		rots1.Add (new Vector3 (0, 0, 0)); // 5
		rots1.Add (new Vector3 (0, 0, 0)); // 6
		rots1.Add (new Vector3 (0, 0, 0)); // 7
		rots1.Add (new Vector3 (0, 0, 0)); // 8
        rots1.Add(new Vector3 (0, 0, 0)); // 9
        rots1.Add(new Vector3 (0, 0, 0)); // 10


        // Ring 2
        locs2.Add (new Vector3 (105, 72.5f, 105)); // 1
		locs2.Add (new Vector3 (115, 72.5f, 105)); // 2
		locs2.Add (new Vector3 (125, 72.5f, 105)); // 3
		locs2.Add (new Vector3 (135, 72.5f, 105)); // 4
		locs2.Add (new Vector3 (145, 72.5f, 105)); // 5
		locs2.Add (new Vector3 (155, 72.5f, 105)); // 6
		locs2.Add (new Vector3 (165, 72.5f, 105)); // 7
		locs2.Add (new Vector3 (175, 72.5f, 105)); // 8
		locs2.Add (new Vector3 (185, 72.5f, 105)); // 9
		locs2.Add (new Vector3 (195, 72.5f, 105)); // 10


		rots2.Add (new Vector3 (0, 0, 0)); // 1
		rots2.Add (new Vector3 (0, 0, 0)); // 2
		rots2.Add (new Vector3 (0, 0, 0)); // 3
		rots2.Add (new Vector3 (0, 0, 0)); // 4
		rots2.Add (new Vector3 (0, 0, 0)); // 5
		rots2.Add (new Vector3 (0, 0, 0)); // 6
		rots2.Add (new Vector3 (0, 0, 0)); // 7
		rots2.Add (new Vector3 (0, 0, 0)); // 8
		rots2.Add (new Vector3 (0, 0, 0)); // 1
		rots2.Add (new Vector3 (0, 0, 0)); // 2


		// Ring 3
		locs3.Add (new Vector3 (195, 72.5f, 105)); // 1
		locs3.Add (new Vector3 (195, 72.5f, 115)); // 2
		locs3.Add (new Vector3 (195, 72.5f, 125)); // 3
		locs3.Add (new Vector3 (195, 72.5f, 135)); // 4
		locs3.Add (new Vector3 (195, 72.5f, 145)); // 5
		locs3.Add (new Vector3 (195, 72.5f, 155)); // 6
		locs3.Add (new Vector3 (195, 72.5f, 165)); // 7
		locs3.Add (new Vector3 (195, 72.5f, 175)); // 8
		locs3.Add (new Vector3 (195, 72.5f, 185)); // 9
		locs3.Add (new Vector3 (195, 72.5f, 195)); // 10


		rots3.Add (new Vector3 (0, 0, 0)); // 1
		rots3.Add (new Vector3 (0, 0, 0)); // 2
		rots3.Add (new Vector3 (0, 0, 0)); // 3
		rots3.Add (new Vector3 (0, 0, 0)); // 4
		rots3.Add (new Vector3 (0, 0, 0)); // 5
		rots3.Add (new Vector3 (0, 0, 0)); // 6
		rots3.Add (new Vector3 (0, 0, 0)); // 7
		rots3.Add (new Vector3 (0, 0, 0)); // 8
		rots3.Add (new Vector3 (0, 0, 0)); // 9
		rots3.Add (new Vector3 (0, 0, 0)); // 10


		// Ring 4
		locs4.Add (new Vector3 (105, 72.5f, 195)); // 1
		locs4.Add (new Vector3 (115, 72.5f, 195)); // 2
		locs4.Add (new Vector3 (125, 72.5f, 195)); // 3
		locs4.Add (new Vector3 (135, 72.5f, 195)); // 4
		locs4.Add (new Vector3 (145, 72.5f, 195)); // 5
		locs4.Add (new Vector3 (155, 72.5f, 195)); // 6
		locs4.Add (new Vector3 (165, 72.5f, 195)); // 7
		locs4.Add (new Vector3 (175, 72.5f, 195)); // 8
		locs4.Add (new Vector3 (185, 72.5f, 195)); // 9
		locs4.Add (new Vector3 (195, 72.5f, 195)); // 10


		rots4.Add (new Vector3 (0, 0, 0)); // 1
		rots4.Add (new Vector3 (0, 0, 0)); // 2
		rots4.Add (new Vector3 (0, 0, 0)); // 3
		rots4.Add (new Vector3 (0, 0, 0)); // 4
		rots4.Add (new Vector3 (0, 0, 0)); // 5
		rots4.Add (new Vector3 (0, 0, 0)); // 6
		rots4.Add (new Vector3 (0, 0, 0)); // 7
		rots4.Add (new Vector3 (0, 0, 0)); // 8
		rots4.Add (new Vector3 (0, 0, 0)); // 9
		rots4.Add (new Vector3 (0, 0, 0)); // 10

	}

	void Start () 
	{

		if (ReminderCount.Count == 1) {
			i = UnityEngine.Random.Range (0, 9);
			transform.position = locs1 [i];
			transform.eulerAngles = rots1 [i];
		}

		if (ReminderCount.Count == 2) {
			i = UnityEngine.Random.Range (0, 9);
			transform.position = locs2 [i];
			transform.eulerAngles = rots2 [i];
		}

		if (ReminderCount.Count == 3) {
			i = UnityEngine.Random.Range (0, 9);
			transform.position = locs3 [i];
			transform.eulerAngles = rots3 [i];
		}

		if (ReminderCount.Count == 4) {
			i = UnityEngine.Random.Range (0, 9);
			transform.position = locs4 [i];
			transform.eulerAngles = rots4 [i];
		}
	}
}
