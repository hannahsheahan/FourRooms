﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentRevealScript : MonoBehaviour {

    public GameObject present;
    public int presentIndex;

    // Define the indices for the different presents
    //private const int GREEN1  = 0;
    //private const int GREEN2  = 1;
    //private const int GREEN3  = 2;
    //private const int RED1    = 3;
    //private const int RED2    = 4;
    //private const int RED3    = 5;
    //private const int YELLOW1 = 6;
    //private const int YELLOW2 = 7;
    //private const int YELLOW3 = 8;
    //private const int BLUE1   = 9;
    //private const int BLUE2   = 10;
    //private const int BLUE3   = 11;

    // ********************************************************************** //

    void Start()
    {
        // Load the present spawn location from the configured datafile
        transform.position = GameController.control.presentPositions[presentIndex];
    }

    // ********************************************************************** //

    private void OnTriggerEnter(Collider other)
    {
        GameController.control.OpenBoxQuestion(true);
    }

    // ********************************************************************** //

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.control.OpenBox();
            GameController.control.OpenBoxQuestion(false);
            present.SetActive(false);
            GameController.control.giftWrapState[presentIndex] = 0; // effectively a bool, but shorter to write as string to file 
            GameController.control.RecordGiftStates();              // save a timestamp and the gift states
        }
    }

    // ********************************************************************** //

    private void OnTriggerExit(Collider other)
    {
        GameController.control.OpenBoxQuestion(false);
    }

}
