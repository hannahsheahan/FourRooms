using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class Star : MonoBehaviour
{
    private Timer starTimer;
    private bool starHit = false;

    // ********************************************************************** //

    void Start()
    {
        starTimer = new Timer();
        starTimer.Reset();
       
        // ***HRS Set location of the star based on datafile (eventually), or for now, a randomisation process
    }

    // ********************************************************************** //

    void OnTriggerEnter(Collider other)
    {
        starTimer.Reset(); // record entry time
        starHit = true;
    }

    // ********************************************************************** //

    void OnTriggerExit(Collider other)
    {
        starHit = false;
    }

    // ********************************************************************** //

    void Update()
    {
        if ((starTimer.ElapsedSeconds() > GameController.control.minDwellAtStar) && (starHit))
        {
            GameController.control.StarFound();
            starHit = false;
        }
    }
    // ********************************************************************** //

}
