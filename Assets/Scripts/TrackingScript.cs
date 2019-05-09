using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrackingScript : MonoBehaviour
{
    public List<string> coords = new List<string>();


    // ********************************************************************** //

    void Start ()
    {
        // Track the time, position, rotation of the player at a rate of 25Hz (this seems pretty slow but maybe ok).
        coords.Add(string.Format("{0} {1} {2} {3}", "Time", "x-position", "z-position", "y-rotation"));
        InvokeRepeating("StoreLocation", 0f, GameController.control.dataRecordFrequency);
    }

    // ********************************************************************** //

    void StoreLocation ()
    {
        coords.Add(GetLocation());
    }

    // ********************************************************************** //

    string GetLocation()
    {
        // Here define location transform.position and euler angles
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;
        // Note that the ':0.0#' part of the formatting means "require at least one DP but not more than 2"
        string locString = string.Format("{0:0.00} {1:0.0#} {2:0.0#} {3:0.0#}", Time.time, pos.x, pos.z, rot.y);
        return locString;
    }

    // ********************************************************************** //

    public List<string> getCoords()
    {
        return coords;
    }
}
