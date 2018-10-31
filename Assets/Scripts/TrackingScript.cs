using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrackingScript : MonoBehaviour
{
    public List<string> coords = new List<string>();
    
    void Start ()
    {
        // Track the time, position, rotation of the player at a rate of 25Hz (this seems pretty slow but maybe ok).
        InvokeRepeating("storeLocation", 0f, 0.04f);
    }

    void storeLocation ()
    {
        coords.Add(getLocation());
    }

    string getLocation()
    {
        // Here define location transform.position and euler angles
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;

		string locString = string.Format("{0} {1} {2} {3} {4} {5} {6}", Time.time, pos.x, pos.y, pos.z, rot.x, rot.y, rot.z);
        return locString;
    }

    public List<string> getCoords()
    {
        return coords;
    }
}
