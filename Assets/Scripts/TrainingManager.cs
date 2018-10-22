using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrainingManager : MonoBehaviour {

	private int i;

	public List<Vector3> locs1 = new List<Vector3>();
	public List<Vector3> rots1 = new List<Vector3>();

	public List<Vector3> locs2 = new List<Vector3>();
	public List<Vector3> rots2 = new List<Vector3>();

	public List<Vector3> locs3 = new List<Vector3>();
	public List<Vector3> rots3 = new List<Vector3>();

	public List<Vector3> locs4 = new List<Vector3>();
	public List<Vector3> rots4 = new List<Vector3>();

	public List<Vector3> locs5 = new List<Vector3>();
	public List<Vector3> rots5 = new List<Vector3>();

	public List<Vector3> locs6 = new List<Vector3>();
	public List<Vector3> rots6 = new List<Vector3>();

	// Use this for initialization
	void Awake ()
	{
		// Ring 1
		locs1.Add (new Vector3 (145, 72.5f, 155)); // 1
		locs1.Add (new Vector3 (145, 72.5f, 165)); // 2
		locs1.Add (new Vector3 (145, 72.5f, 175)); // 3
		locs1.Add (new Vector3 (135, 72.5f, 175)); // 4
		locs1.Add (new Vector3 (125, 72.5f, 175)); // 5
		locs1.Add (new Vector3 (125, 72.5f, 165)); // 6
		locs1.Add (new Vector3 (125, 72.5f, 155)); // 7
		locs1.Add (new Vector3 (135, 72.5f, 155)); // 8


		rots1.Add (new Vector3 (0, 0, 0)); // 1
		rots1.Add (new Vector3 (0, 0, 0)); // 2
		rots1.Add (new Vector3 (0, 0, 0)); // 3
		rots1.Add (new Vector3 (0, 0, 0)); // 4
		rots1.Add (new Vector3 (0, 0, 0)); // 5
		rots1.Add (new Vector3 (0, 0, 0)); // 6
		rots1.Add (new Vector3 (0, 0, 0)); // 7
		rots1.Add (new Vector3 (0, 0, 0)); // 8


		// Ring 2
		locs2.Add (new Vector3 (155, 72.5f, 145)); // 1
		locs2.Add (new Vector3 (155, 72.5f, 155)); // 2
		locs2.Add (new Vector3 (155, 72.5f, 165)); // 3
		locs2.Add (new Vector3 (155, 72.5f, 175)); // 4
		locs2.Add (new Vector3 (155, 72.5f, 185)); // 5
		locs2.Add (new Vector3 (145, 72.5f, 185)); // 6
		locs2.Add (new Vector3 (135, 72.5f, 185)); // 7
		locs2.Add (new Vector3 (125, 72.5f, 185)); // 8
		locs2.Add (new Vector3 (115, 72.5f, 185)); // 1
		locs2.Add (new Vector3 (115, 72.5f, 175)); // 2
		locs2.Add (new Vector3 (115, 72.5f, 165)); // 3
		locs2.Add (new Vector3 (115, 72.5f, 155)); // 4
		locs2.Add (new Vector3 (115, 72.5f, 145)); // 5
		locs2.Add (new Vector3 (125, 72.5f, 145)); // 6
		locs2.Add (new Vector3 (135, 72.5f, 145)); // 7
		locs2.Add (new Vector3 (145, 72.5f, 145)); // 8

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
		rots2.Add (new Vector3 (0, 0, 0)); // 3
		rots2.Add (new Vector3 (0, 0, 0)); // 4
		rots2.Add (new Vector3 (0, 0, 0)); // 5
		rots2.Add (new Vector3 (0, 0, 0)); // 6
		rots2.Add (new Vector3 (0, 0, 0)); // 7
		rots2.Add (new Vector3 (0, 0, 0)); // 8

		// Ring 3
		locs3.Add (new Vector3 (165, 72.5f, 135)); // 1
		locs3.Add (new Vector3 (165, 72.5f, 145)); // 2
		locs3.Add (new Vector3 (165, 72.5f, 155)); // 3
		locs3.Add (new Vector3 (165, 72.5f, 165)); // 4
		locs3.Add (new Vector3 (165, 72.5f, 175)); // 5
		locs3.Add (new Vector3 (165, 72.5f, 185)); // 6
		locs3.Add (new Vector3 (165, 72.5f, 195)); // 7
		locs3.Add (new Vector3 (155, 72.5f, 195)); // 8
		locs3.Add (new Vector3 (145, 72.5f, 195)); // 9
		locs3.Add (new Vector3 (135, 72.5f, 195)); // 10
		locs3.Add (new Vector3 (135, 72.5f, 195)); // 1
		locs3.Add (new Vector3 (115, 72.5f, 195)); // 2
		locs3.Add (new Vector3 (105, 72.5f, 195)); // 3
		locs3.Add (new Vector3 (105, 72.5f, 185)); // 4
		locs3.Add (new Vector3 (105, 72.5f, 175)); // 5
		locs3.Add (new Vector3 (105, 72.5f, 165)); // 6
		locs3.Add (new Vector3 (105, 72.5f, 155)); // 7
		locs3.Add (new Vector3 (105, 72.5f, 145)); // 8
		locs3.Add (new Vector3 (105, 72.5f, 135)); // 9
		locs3.Add (new Vector3 (115, 72.5f, 135)); // 10
		locs3.Add (new Vector3 (125, 72.5f, 135)); // 1
		locs3.Add (new Vector3 (135, 72.5f, 135)); // 2
		locs3.Add (new Vector3 (145, 72.5f, 135)); // 3
		locs3.Add (new Vector3 (155, 72.5f, 135)); // 4

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
		rots3.Add (new Vector3 (0, 0, 0)); // 1
		rots3.Add (new Vector3 (0, 0, 0)); // 2
		rots3.Add (new Vector3 (0, 0, 0)); // 3
		rots3.Add (new Vector3 (0, 0, 0)); // 4

		// Ring 4
		locs4.Add (new Vector3 (105, 72.5f, 125)); // 1
		locs4.Add (new Vector3 (115, 72.5f, 125)); // 2
		locs4.Add (new Vector3 (125, 72.5f, 125)); // 3
		locs4.Add (new Vector3 (135, 72.5f, 125)); // 4
		locs4.Add (new Vector3 (145, 72.5f, 125)); // 5
		locs4.Add (new Vector3 (155, 72.5f, 125)); // 6
		locs4.Add (new Vector3 (165, 72.5f, 125)); // 7
		locs4.Add (new Vector3 (175, 72.5f, 125)); // 8
		locs4.Add (new Vector3 (175, 72.5f, 135)); // 9
		locs4.Add (new Vector3 (175, 72.5f, 145)); // 10
		locs4.Add (new Vector3 (175, 72.5f, 155)); // 1
		locs4.Add (new Vector3 (175, 72.5f, 165)); // 2
		locs4.Add (new Vector3 (175, 72.5f, 175)); // 3
		locs4.Add (new Vector3 (175, 72.5f, 185)); // 4
		locs4.Add (new Vector3 (175, 72.5f, 195)); // 5

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
		rots4.Add (new Vector3 (0, 0, 0)); // 1
		rots4.Add (new Vector3 (0, 0, 0)); // 2
		rots4.Add (new Vector3 (0, 0, 0)); // 3
		rots4.Add (new Vector3 (0, 0, 0)); // 4
		rots4.Add (new Vector3 (0, 0, 0)); // 5

		// Ring 5
		locs5.Add (new Vector3 (105, 72.5f, 115)); // 1
		locs5.Add (new Vector3 (115, 72.5f, 115)); // 2
		locs5.Add (new Vector3 (125, 72.5f, 115)); // 3
		locs5.Add (new Vector3 (135, 72.5f, 115)); // 4
		locs5.Add (new Vector3 (145, 72.5f, 115)); // 5
		locs5.Add (new Vector3 (155, 72.5f, 115)); // 6
		locs5.Add (new Vector3 (165, 72.5f, 115)); // 7
		locs5.Add (new Vector3 (175, 72.5f, 115)); // 8
		locs5.Add (new Vector3 (185, 72.5f, 115)); // 9
		locs5.Add (new Vector3 (185, 72.5f, 125)); // 10
		locs5.Add (new Vector3 (185, 72.5f, 135)); // 1
		locs5.Add (new Vector3 (185, 72.5f, 145)); // 2
		locs5.Add (new Vector3 (185, 72.5f, 155)); // 3
		locs5.Add (new Vector3 (185, 72.5f, 165)); // 4
		locs5.Add (new Vector3 (185, 72.5f, 175)); // 5
		locs5.Add (new Vector3 (185, 72.5f, 185)); // 6
		locs5.Add (new Vector3 (185, 72.5f, 195)); // 7

		rots5.Add (new Vector3 (0, 0, 0)); // 1
		rots5.Add (new Vector3 (0, 0, 0)); // 2
		rots5.Add (new Vector3 (0, 0, 0)); // 3
		rots5.Add (new Vector3 (0, 0, 0)); // 4
		rots5.Add (new Vector3 (0, 0, 0)); // 5
		rots5.Add (new Vector3 (0, 0, 0)); // 6
		rots5.Add (new Vector3 (0, 0, 0)); // 7
		rots5.Add (new Vector3 (0, 0, 0)); // 8
		rots5.Add (new Vector3 (0, 0, 0)); // 9
		rots5.Add (new Vector3 (0, 0, 0)); // 10
		rots5.Add (new Vector3 (0, 0, 0)); // 1
		rots5.Add (new Vector3 (0, 0, 0)); // 2
		rots5.Add (new Vector3 (0, 0, 0)); // 3
		rots5.Add (new Vector3 (0, 0, 0)); // 4
		rots5.Add (new Vector3 (0, 0, 0)); // 5
		rots5.Add (new Vector3 (0, 0, 0)); // 6
		rots5.Add (new Vector3 (0, 0, 0)); // 7

		// Config 6
		locs6.Add (new Vector3 (105, 72.5f, 105)); // 1
		locs6.Add (new Vector3 (115, 72.5f, 105)); // 2
		locs6.Add (new Vector3 (125, 72.5f, 105)); // 3
		locs6.Add (new Vector3 (135, 72.5f, 105)); // 4
		locs6.Add (new Vector3 (145, 72.5f, 105)); // 5
		locs6.Add (new Vector3 (155, 72.5f, 105)); // 6
		locs6.Add (new Vector3 (165, 72.5f, 105)); // 7
		locs6.Add (new Vector3 (175, 72.5f, 105)); // 8
		locs6.Add (new Vector3 (185, 72.5f, 105)); // 9
		locs6.Add (new Vector3 (195, 72.5f, 105)); // 10
		locs6.Add (new Vector3 (195, 72.5f, 115)); // 1
		locs6.Add (new Vector3 (195, 72.5f, 125)); // 2
		locs6.Add (new Vector3 (195, 72.5f, 135)); // 3
		locs6.Add (new Vector3 (195, 72.5f, 145)); // 4
		locs6.Add (new Vector3 (195, 72.5f, 155)); // 5
		locs6.Add (new Vector3 (195, 72.5f, 165)); // 6
		locs6.Add (new Vector3 (195, 72.5f, 175)); // 7
		locs6.Add (new Vector3 (195, 72.5f, 185)); // 8
		locs6.Add (new Vector3 (195, 72.5f, 195)); // 9

		rots6.Add (new Vector3 (0, 0, 0)); // 1
		rots6.Add (new Vector3 (0, 0, 0)); // 2
		rots6.Add (new Vector3 (0, 0, 0)); // 3
		rots6.Add (new Vector3 (0, 0, 0)); // 4
		rots6.Add (new Vector3 (0, 0, 0)); // 5
		rots6.Add (new Vector3 (0, 0, 0)); // 6
		rots6.Add (new Vector3 (0, 0, 0)); // 7
		rots6.Add (new Vector3 (0, 0, 0)); // 8
		rots6.Add (new Vector3 (0, 0, 0)); // 9
		rots6.Add (new Vector3 (0, 0, 0)); // 10
		rots6.Add (new Vector3 (0, 0, 0)); // 1
		rots6.Add (new Vector3 (0, 0, 0)); // 2
		rots6.Add (new Vector3 (0, 0, 0)); // 3
		rots6.Add (new Vector3 (0, 0, 0)); // 4
		rots6.Add (new Vector3 (0, 0, 0)); // 5
		rots6.Add (new Vector3 (0, 0, 0)); // 6
		rots6.Add (new Vector3 (0, 0, 0)); // 7
		rots6.Add (new Vector3 (0, 0, 0)); // 8
		rots6.Add (new Vector3 (0, 0, 0)); // 9
	}

	void Start () 
	{
		if (TrainCount.ring == 0) {
			transform.position = new Vector3 (145, 72.5f, 105);
			transform.eulerAngles = new Vector3 (0, 0, 0);
		}

		if (TrainCount.ring == 1) {
			i = UnityEngine.Random.Range (0, 8);
			transform.position = locs1 [i];
			transform.eulerAngles = rots1 [i];
		}

		if (TrainCount.ring == 2) {
			i = UnityEngine.Random.Range (0, 16);
			transform.position = locs2 [i];
			transform.eulerAngles = rots2 [i];
		}

		if (TrainCount.ring == 3) {
			i = UnityEngine.Random.Range (0, 24);
			transform.position = locs3 [i];
			transform.eulerAngles = rots3 [i];
		}

		if (TrainCount.ring == 4) {
			i = UnityEngine.Random.Range (0, 15);
			transform.position = locs4 [i];
			transform.eulerAngles = rots4 [i];
		}

		if (TrainCount.ring == 5) {
			i = UnityEngine.Random.Range (0, 17);
			transform.position = locs5 [i];
			transform.eulerAngles = rots5 [i];
		}

		if (TrainCount.ring == 6) {
			i = UnityEngine.Random.Range (0, 19);
			transform.position = locs6 [i];
			transform.eulerAngles = rots6 [i];
		}
	}
}
