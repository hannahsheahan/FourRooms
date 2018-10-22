using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LocationManager : MonoBehaviour {

	public List<Vector3> locs = new List<Vector3>();
	public List<Vector3> rots = new List<Vector3>();

	// Use this for initialization
	void Awake ()
	{
		// Config 1
		locs.Add (new Vector3 (195, 72.5f, 195)); // 1
		locs.Add (new Vector3 (155, 72.5f, 105)); // 2
		locs.Add (new Vector3 (195, 72.5f, 105)); // 3
		locs.Add (new Vector3 (105, 72.5f, 105)); // 4
		locs.Add (new Vector3 (135, 72.5f, 195)); // 5
		locs.Add (new Vector3 (155, 72.5f, 165)); // 6
		locs.Add (new Vector3 (195, 72.5f, 165)); // 7
		locs.Add (new Vector3 (175, 72.5f, 105)); // 8
		locs.Add (new Vector3 (155, 72.5f, 135)); // 9
		locs.Add (new Vector3 (195, 72.5f, 125)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 2
		locs.Add (new Vector3 (105, 72.5f, 155)); // 1
		locs.Add (new Vector3 (115, 72.5f, 105)); // 2
		locs.Add (new Vector3 (195, 72.5f, 105)); // 3
		locs.Add (new Vector3 (195, 72.5f, 195)); // 4
		locs.Add (new Vector3 (105, 72.5f, 115)); // 5
		locs.Add (new Vector3 (195, 72.5f, 175)); // 6
		locs.Add (new Vector3 (135, 72.5f, 125)); // 7
		locs.Add (new Vector3 (105, 72.5f, 155)); // 8
		locs.Add (new Vector3 (195, 72.5f, 145)); // 9
		locs.Add (new Vector3 (165, 72.5f, 125)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 3
		locs.Add (new Vector3 (195, 72.5f, 195)); // 1
		locs.Add (new Vector3 (155, 72.5f, 195)); // 2
		locs.Add (new Vector3 (105, 72.5f, 195)); // 3
		locs.Add (new Vector3 (195, 72.5f, 125)); // 4
		locs.Add (new Vector3 (155, 72.5f, 145)); // 5
		locs.Add (new Vector3 (175, 72.5f, 165)); // 6
		locs.Add (new Vector3 (195, 72.5f, 105)); // 7
		locs.Add (new Vector3 (195, 72.5f, 155)); // 8
		locs.Add (new Vector3 (155, 72.5f, 165)); // 9
		locs.Add (new Vector3 (135, 72.5f, 185)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 4
		locs.Add (new Vector3 (105, 72.5f, 145)); // 1
		locs.Add (new Vector3 (195, 72.5f, 165)); // 2
		locs.Add (new Vector3 (125, 72.5f, 135)); // 3
		locs.Add (new Vector3 (175, 72.5f, 145)); // 4
		locs.Add (new Vector3 (105, 72.5f, 115)); // 5
		locs.Add (new Vector3 (195, 72.5f, 135)); // 6
		locs.Add (new Vector3 (125, 72.5f, 105)); // 7
		locs.Add (new Vector3 (195, 72.5f, 115)); // 8
		locs.Add (new Vector3 (155, 72.5f, 105)); // 9
		locs.Add (new Vector3 (145, 72.5f, 145)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 5
		locs.Add (new Vector3 (195, 72.5f, 155)); // 1
		locs.Add (new Vector3 (145, 72.5f, 135)); // 2
		locs.Add (new Vector3 (125, 72.5f, 105)); // 3
		locs.Add (new Vector3 (165, 72.5f, 155)); // 4
		locs.Add (new Vector3 (145, 72.5f, 105)); // 5
		locs.Add (new Vector3 (165, 72.5f, 135)); // 6
		locs.Add (new Vector3 (165, 72.5f, 105)); // 7
		locs.Add (new Vector3 (175, 72.5f, 135)); // 8
		locs.Add (new Vector3 (185, 72.5f, 105)); // 9
		locs.Add (new Vector3 (195, 72.5f, 125)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 6
		locs.Add (new Vector3 (155, 72.5f, 105)); // 1
		locs.Add (new Vector3 (155, 72.5f, 195)); // 2
		locs.Add (new Vector3 (165, 72.5f, 125)); // 3
		locs.Add (new Vector3 (165, 72.5f, 175)); // 4
		locs.Add (new Vector3 (175, 72.5f, 105)); // 5
		locs.Add (new Vector3 (185, 72.5f, 195)); // 6
		locs.Add (new Vector3 (195, 72.5f, 105)); // 7
		locs.Add (new Vector3 (195, 72.5f, 165)); // 8
		locs.Add (new Vector3 (195, 72.5f, 135)); // 9
		locs.Add (new Vector3 (165, 72.5f, 155)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 7
		locs.Add (new Vector3 (145, 72.5f, 135)); // 1
		locs.Add (new Vector3 (145, 72.5f, 195)); // 2
		locs.Add (new Vector3 (145, 72.5f, 105)); // 3
		locs.Add (new Vector3 (155, 72.5f, 175)); // 4
		locs.Add (new Vector3 (165, 72.5f, 105)); // 5
		locs.Add (new Vector3 (165, 72.5f, 165)); // 6
		locs.Add (new Vector3 (195, 72.5f, 145)); // 7
		locs.Add (new Vector3 (195, 72.5f, 195)); // 8
		locs.Add (new Vector3 (195, 72.5f, 105)); // 9
		locs.Add (new Vector3 (165, 72.5f, 145)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 8
		locs.Add (new Vector3 (195, 72.5f, 155)); // 1
		locs.Add (new Vector3 (175, 72.5f, 195)); // 2
		locs.Add (new Vector3 (195, 72.5f, 135)); // 3
		locs.Add (new Vector3 (155, 72.5f, 195)); // 4
		locs.Add (new Vector3 (195, 72.5f, 115)); // 5
		locs.Add (new Vector3 (125, 72.5f, 195)); // 6
		locs.Add (new Vector3 (165, 72.5f, 105)); // 7
		locs.Add (new Vector3 (135, 72.5f, 135)); // 8
		locs.Add (new Vector3 (115, 72.5f, 105)); // 9
		locs.Add (new Vector3 (105, 72.5f, 155)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 9
		locs.Add (new Vector3 (135, 72.5f, 125)); // 1
		locs.Add (new Vector3 (145, 72.5f, 195)); // 2
		locs.Add (new Vector3 (145, 72.5f, 105)); // 3
		locs.Add (new Vector3 (165, 72.5f, 175)); // 4
		locs.Add (new Vector3 (175, 72.5f, 105)); // 5
		locs.Add (new Vector3 (185, 72.5f, 195)); // 6
		locs.Add (new Vector3 (195, 72.5f, 115)); // 7
		locs.Add (new Vector3 (195, 72.5f, 165)); // 8
		locs.Add (new Vector3 (165, 72.5f, 155)); // 9
		locs.Add (new Vector3 (155, 72.5f, 135)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 10
		locs.Add (new Vector3 (175, 72.5f, 175)); // 1
		locs.Add (new Vector3 (105, 72.5f, 145)); // 2
		locs.Add (new Vector3 (145, 72.5f, 105)); // 3
		locs.Add (new Vector3 (105, 72.5f, 125)); // 4
		locs.Add (new Vector3 (125, 72.5f, 105)); // 5
		locs.Add (new Vector3 (195, 72.5f, 165)); // 6
		locs.Add (new Vector3 (135, 72.5f, 125)); // 7
		locs.Add (new Vector3 (195, 72.5f, 175)); // 8
		locs.Add (new Vector3 (125, 72.5f, 125)); // 9
		locs.Add (new Vector3 (105, 72.5f, 105)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 11
		locs.Add (new Vector3 (155, 72.5f, 105)); // 1
		locs.Add (new Vector3 (155, 72.5f, 195)); // 2
		locs.Add (new Vector3 (175, 72.5f, 105)); // 3
		locs.Add (new Vector3 (155, 72.5f, 175)); // 4
		locs.Add (new Vector3 (195, 72.5f, 105)); // 5
		locs.Add (new Vector3 (185, 72.5f, 195)); // 6
		locs.Add (new Vector3 (195, 72.5f, 125)); // 7
		locs.Add (new Vector3 (195, 72.5f, 165)); // 8
		locs.Add (new Vector3 (155, 72.5f, 145)); // 9
		locs.Add (new Vector3 (195, 72.5f, 145)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 12
		locs.Add (new Vector3 (135, 72.5f, 145)); // 1
		locs.Add (new Vector3 (155, 72.5f, 155)); // 2
		locs.Add (new Vector3 (145, 72.5f, 105)); // 3
		locs.Add (new Vector3 (195, 72.5f, 165)); // 4
		locs.Add (new Vector3 (135, 72.5f, 135)); // 5
		locs.Add (new Vector3 (155, 72.5f, 135)); // 6
		locs.Add (new Vector3 (175, 72.5f, 105)); // 7
		locs.Add (new Vector3 (165, 72.5f, 155)); // 8
		locs.Add (new Vector3 (195, 72.5f, 135)); // 9
		locs.Add (new Vector3 (195, 72.5f, 115)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 13
		locs.Add (new Vector3 (135, 72.5f, 135)); // 1
		locs.Add (new Vector3 (165, 72.5f, 105)); // 2
		locs.Add (new Vector3 (135, 72.5f, 105)); // 3
		locs.Add (new Vector3 (195, 72.5f, 115)); // 4
		locs.Add (new Vector3 (105, 72.5f, 115)); // 5
		locs.Add (new Vector3 (195, 72.5f, 135)); // 6
		locs.Add (new Vector3 (105, 72.5f, 155)); // 7
		locs.Add (new Vector3 (195, 72.5f, 165)); // 8
		locs.Add (new Vector3 (105, 72.5f, 185)); // 9
		locs.Add (new Vector3 (145, 72.5f, 195)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 14
		locs.Add (new Vector3 (135, 72.5f, 145)); // 1
		locs.Add (new Vector3 (195, 72.5f, 115)); // 2
		locs.Add (new Vector3 (135, 72.5f, 105)); // 3
		locs.Add (new Vector3 (145, 72.5f, 145)); // 4
		locs.Add (new Vector3 (185, 72.5f, 125)); // 5
		locs.Add (new Vector3 (145, 72.5f, 105)); // 6
		locs.Add (new Vector3 (155, 72.5f, 135)); // 7
		locs.Add (new Vector3 (195, 72.5f, 105)); // 8
		locs.Add (new Vector3 (155, 72.5f, 115)); // 9
		locs.Add (new Vector3 (175, 72.5f, 105)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 15
		locs.Add (new Vector3 (195, 72.5f, 145)); // 1
		locs.Add (new Vector3 (105, 72.5f, 145)); // 2
		locs.Add (new Vector3 (195, 72.5f, 115)); // 3
		locs.Add (new Vector3 (105, 72.5f, 115)); // 4
		locs.Add (new Vector3 (165, 72.5f, 135)); // 5
		locs.Add (new Vector3 (125, 72.5f, 135)); // 6
		locs.Add (new Vector3 (125, 72.5f, 105)); // 7
		locs.Add (new Vector3 (175, 72.5f, 105)); // 8
		locs.Add (new Vector3 (145, 72.5f, 105)); // 9
		locs.Add (new Vector3 (145, 72.5f, 135)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 16
		locs.Add (new Vector3 (135, 72.5f, 105)); // 1
		locs.Add (new Vector3 (135, 72.5f, 195)); // 2
		locs.Add (new Vector3 (155, 72.5f, 105)); // 3
		locs.Add (new Vector3 (155, 72.5f, 195)); // 4
		locs.Add (new Vector3 (175, 72.5f, 125)); // 5
		locs.Add (new Vector3 (165, 72.5f, 175)); // 6
		locs.Add (new Vector3 (175, 72.5f, 105)); // 7
		locs.Add (new Vector3 (195, 72.5f, 195)); // 8
		locs.Add (new Vector3 (195, 72.5f, 135)); // 9
		locs.Add (new Vector3 (195, 72.5f, 165)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 17
		locs.Add (new Vector3 (105, 72.5f, 145)); // 1
		locs.Add (new Vector3 (195, 72.5f, 145)); // 2
		locs.Add (new Vector3 (105, 72.5f, 125)); // 3
		locs.Add (new Vector3 (175, 72.5f, 155)); // 4
		locs.Add (new Vector3 (125, 72.5f, 145)); // 5
		locs.Add (new Vector3 (135, 72.5f, 105)); // 6
		locs.Add (new Vector3 (195, 72.5f, 115)); // 7
		locs.Add (new Vector3 (155, 72.5f, 105)); // 8
		locs.Add (new Vector3 (175, 72.5f, 105)); // 9
		locs.Add (new Vector3 (145, 72.5f, 145)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 18
		locs.Add (new Vector3 (105, 72.5f, 145)); // 1
		locs.Add (new Vector3 (195, 72.5f, 165)); // 2
		locs.Add (new Vector3 (105, 72.5f, 115)); // 3
		locs.Add (new Vector3 (165, 72.5f, 155)); // 4
		locs.Add (new Vector3 (125, 72.5f, 135)); // 5
		locs.Add (new Vector3 (195, 72.5f, 135)); // 6
		locs.Add (new Vector3 (115, 72.5f, 105)); // 7
		locs.Add (new Vector3 (165, 72.5f, 135)); // 8
		locs.Add (new Vector3 (165, 72.5f, 105)); // 9
		locs.Add (new Vector3 (145, 72.5f, 135)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 19
		locs.Add (new Vector3 (155, 72.5f, 105)); // 1
		locs.Add (new Vector3 (165, 72.5f, 175)); // 2
		locs.Add (new Vector3 (175, 72.5f, 105)); // 3
		locs.Add (new Vector3 (185, 72.5f, 195)); // 4
		locs.Add (new Vector3 (195, 72.5f, 105)); // 5
		locs.Add (new Vector3 (195, 72.5f, 175)); // 6
		locs.Add (new Vector3 (175, 72.5f, 155)); // 7
		locs.Add (new Vector3 (195, 72.5f, 135)); // 8
		locs.Add (new Vector3 (175, 72.5f, 145)); // 9
		locs.Add (new Vector3 (195, 72.5f, 155)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 20
		locs.Add (new Vector3 (105, 72.5f, 145)); // 1
		locs.Add (new Vector3 (195, 72.5f, 155)); // 2
		locs.Add (new Vector3 (105, 72.5f, 125)); // 3
		locs.Add (new Vector3 (195, 72.5f, 125)); // 4
		locs.Add (new Vector3 (155, 72.5f, 145)); // 5
		locs.Add (new Vector3 (115, 72.5f, 105)); // 6
		locs.Add (new Vector3 (185, 72.5f, 105)); // 7
		locs.Add (new Vector3 (135, 72.5f, 105)); // 8
		locs.Add (new Vector3 (175, 72.5f, 145)); // 9
		locs.Add (new Vector3 (165, 72.5f, 105)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 21
		locs.Add (new Vector3 (135, 72.5f, 105)); // 1
		locs.Add (new Vector3 (195, 72.5f, 155)); // 2
		locs.Add (new Vector3 (135, 72.5f, 135)); // 3
		locs.Add (new Vector3 (165, 72.5f, 175)); // 4
		locs.Add (new Vector3 (115, 72.5f, 105)); // 5
		locs.Add (new Vector3 (175, 72.5f, 195)); // 6
		locs.Add (new Vector3 (105, 72.5f, 125)); // 7
		locs.Add (new Vector3 (145, 72.5f, 195)); // 8
		locs.Add (new Vector3 (105, 72.5f, 165)); // 9
		locs.Add (new Vector3 (115, 72.5f, 195)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 22
		locs.Add (new Vector3 (155, 72.5f, 195)); // 1
		locs.Add (new Vector3 (155, 72.5f, 105)); // 2
		locs.Add (new Vector3 (175, 72.5f, 195)); // 3
		locs.Add (new Vector3 (175, 72.5f, 105)); // 4
		locs.Add (new Vector3 (165, 72.5f, 165)); // 5
		locs.Add (new Vector3 (195, 72.5f, 115)); // 6
		locs.Add (new Vector3 (195, 72.5f, 185)); // 7
		locs.Add (new Vector3 (195, 72.5f, 135)); // 8
		locs.Add (new Vector3 (165, 72.5f, 145)); // 9
		locs.Add (new Vector3 (195, 72.5f, 155)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 23
		locs.Add (new Vector3 (195, 72.5f, 155)); // 1
		locs.Add (new Vector3 (105, 72.5f, 145)); // 2
		locs.Add (new Vector3 (195, 72.5f, 135)); // 3
		locs.Add (new Vector3 (105, 72.5f, 115)); // 4
		locs.Add (new Vector3 (125, 72.5f, 105)); // 5
		locs.Add (new Vector3 (135, 72.5f, 145)); // 6
		locs.Add (new Vector3 (195, 72.5f, 105)); // 7
		locs.Add (new Vector3 (155, 72.5f, 105)); // 8
		locs.Add (new Vector3 (155, 72.5f, 145)); // 9
		locs.Add (new Vector3 (165, 72.5f, 165)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 24
		locs.Add (new Vector3 (195, 72.5f, 155)); // 1
		locs.Add (new Vector3 (105, 72.5f, 155)); // 2
		locs.Add (new Vector3 (105, 72.5f, 125)); // 3
		locs.Add (new Vector3 (195, 72.5f, 125)); // 4
		locs.Add (new Vector3 (125, 72.5f, 145)); // 5
		locs.Add (new Vector3 (125, 72.5f, 105)); // 6
		locs.Add (new Vector3 (185, 72.5f, 105)); // 7
		locs.Add (new Vector3 (155, 72.5f, 105)); // 8
		locs.Add (new Vector3 (175, 72.5f, 135)); // 9
		locs.Add (new Vector3 (145, 72.5f, 135)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 25
		locs.Add (new Vector3 (165, 72.5f, 125)); // 1
		locs.Add (new Vector3 (195, 72.5f, 155)); // 2
		locs.Add (new Vector3 (165, 72.5f, 155)); // 3
		locs.Add (new Vector3 (145, 72.5f, 105)); // 4
		locs.Add (new Vector3 (165, 72.5f, 135)); // 5
		locs.Add (new Vector3 (145, 72.5f, 145)); // 6
		locs.Add (new Vector3 (195, 72.5f, 125)); // 7
		locs.Add (new Vector3 (165, 72.5f, 105)); // 8
		locs.Add (new Vector3 (145, 72.5f, 135)); // 9
		locs.Add (new Vector3 (195, 72.5f, 105)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 26
		locs.Add (new Vector3 (105, 72.5f, 145)); // 1
		locs.Add (new Vector3 (195, 72.5f, 145)); // 2
		locs.Add (new Vector3 (105, 72.5f, 115)); // 3
		locs.Add (new Vector3 (175, 72.5f, 135)); // 4
		locs.Add (new Vector3 (125, 72.5f, 135)); // 5
		locs.Add (new Vector3 (195, 72.5f, 115)); // 6
		locs.Add (new Vector3 (125, 72.5f, 105)); // 7
		locs.Add (new Vector3 (175, 72.5f, 105)); // 8
		locs.Add (new Vector3 (145, 72.5f, 105)); // 9
		locs.Add (new Vector3 (155, 72.5f, 135)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 27
		locs.Add (new Vector3 (135, 72.5f, 105)); // 1
		locs.Add (new Vector3 (145, 72.5f, 195)); // 2
		locs.Add (new Vector3 (195, 72.5f, 145)); // 3
		locs.Add (new Vector3 (125, 72.5f, 105)); // 4
		locs.Add (new Vector3 (165, 72.5f, 175)); // 5
		locs.Add (new Vector3 (195, 72.5f, 165)); // 6
		locs.Add (new Vector3 (125, 72.5f, 125)); // 7
		locs.Add (new Vector3 (175, 72.5f, 195)); // 8
		locs.Add (new Vector3 (175, 72.5f, 175)); // 9
		locs.Add (new Vector3 (195, 72.5f, 185)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 28
		locs.Add (new Vector3 (145, 72.5f, 195)); // 1
		locs.Add (new Vector3 (145, 72.5f, 125)); // 2
		locs.Add (new Vector3 (165, 72.5f, 175)); // 3
		locs.Add (new Vector3 (165, 72.5f, 105)); // 4
		locs.Add (new Vector3 (175, 72.5f, 195)); // 5
		locs.Add (new Vector3 (155, 72.5f, 145)); // 6
		locs.Add (new Vector3 (195, 72.5f, 175)); // 7
		locs.Add (new Vector3 (195, 72.5f, 115)); // 8
		locs.Add (new Vector3 (165, 72.5f, 155)); // 9
		locs.Add (new Vector3 (195, 72.5f, 135)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 29
		locs.Add (new Vector3 (125, 72.5f, 125)); // 1
		locs.Add (new Vector3 (135, 72.5f, 105)); // 2
		locs.Add (new Vector3 (145, 72.5f, 195)); // 3
		locs.Add (new Vector3 (165, 72.5f, 105)); // 4
		locs.Add (new Vector3 (165, 72.5f, 175)); // 5
		locs.Add (new Vector3 (185, 72.5f, 105)); // 6
		locs.Add (new Vector3 (175, 72.5f, 195)); // 7
		locs.Add (new Vector3 (195, 72.5f, 115)); // 8
		locs.Add (new Vector3 (175, 72.5f, 175)); // 9
		locs.Add (new Vector3 (195, 72.5f, 195)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 30
		locs.Add (new Vector3 (195, 72.5f, 155)); // 1
		locs.Add (new Vector3 (105, 72.5f, 155)); // 2
		locs.Add (new Vector3 (175, 72.5f, 135)); // 3
		locs.Add (new Vector3 (105, 72.5f, 115)); // 4
		locs.Add (new Vector3 (195, 72.5f, 115)); // 5
		locs.Add (new Vector3 (125, 72.5f, 135)); // 6
		locs.Add (new Vector3 (155, 72.5f, 125)); // 7
		locs.Add (new Vector3 (125, 72.5f, 105)); // 8
		locs.Add (new Vector3 (165, 72.5f, 105)); // 9
		locs.Add (new Vector3 (135, 72.5f, 125)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 31
		locs.Add (new Vector3 (145, 72.5f, 195)); // 1
		locs.Add (new Vector3 (145, 72.5f, 105)); // 2
		locs.Add (new Vector3 (155, 72.5f, 175)); // 3
		locs.Add (new Vector3 (135, 72.5f, 125)); // 4
		locs.Add (new Vector3 (175, 72.5f, 195)); // 5
		locs.Add (new Vector3 (185, 72.5f, 105)); // 6
		locs.Add (new Vector3 (165, 72.5f, 165)); // 7
		locs.Add (new Vector3 (195, 72.5f, 185)); // 8
		locs.Add (new Vector3 (155, 72.5f, 135)); // 9
		locs.Add (new Vector3 (195, 72.5f, 145)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 32
		locs.Add (new Vector3 (145, 72.5f, 145)); // 1
		locs.Add (new Vector3 (145, 72.5f, 105)); // 2
		locs.Add (new Vector3 (155, 72.5f, 165)); // 3
		locs.Add (new Vector3 (165, 72.5f, 105)); // 4
		locs.Add (new Vector3 (175, 72.5f, 165)); // 5
		locs.Add (new Vector3 (195, 72.5f, 115)); // 6
		locs.Add (new Vector3 (165, 72.5f, 145)); // 7
		locs.Add (new Vector3 (195, 72.5f, 155)); // 8
		locs.Add (new Vector3 (185, 72.5f, 105)); // 9
		locs.Add (new Vector3 (195, 72.5f, 135)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

		// Config 33
		locs.Add (new Vector3 (165, 72.5f, 165)); // 1
		locs.Add (new Vector3 (105, 72.5f, 155)); // 2
		locs.Add (new Vector3 (195, 72.5f, 155)); // 3
		locs.Add (new Vector3 (105, 72.5f, 125)); // 4
		locs.Add (new Vector3 (155, 72.5f, 135)); // 5
		locs.Add (new Vector3 (125, 72.5f, 125)); // 6
		locs.Add (new Vector3 (185, 72.5f, 105)); // 7
		locs.Add (new Vector3 (105, 72.5f, 105)); // 8
		locs.Add (new Vector3 (195, 72.5f, 115)); // 9
		locs.Add (new Vector3 (145, 72.5f, 105)); // 10

		rots.Add (new Vector3 (0, 0, 0)); // 1
		rots.Add (new Vector3 (0, 0, 0)); // 2
		rots.Add (new Vector3 (0, 0, 0)); // 3
		rots.Add (new Vector3 (0, 0, 0)); // 4
		rots.Add (new Vector3 (0, 0, 0)); // 5
		rots.Add (new Vector3 (0, 0, 0)); // 6
		rots.Add (new Vector3 (0, 0, 0)); // 7
		rots.Add (new Vector3 (0, 0, 0)); // 8
		rots.Add (new Vector3 (0, 0, 0)); // 9
		rots.Add (new Vector3 (0, 0, 0)); // 10

	}

	void Start () 
	{
		transform.position = locs [(LevelManager.level)*10 + LevelManager.trial];
		transform.eulerAngles= rots [(LevelManager.level)*10 + LevelManager.trial];
	}
}
