using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource source;

    // ********************************************************************** //

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // ********************************************************************** //

    public void ExitGameOnClick()
    {
            source.PlayOneShot(buttonClickSound, 1F);
            GameController.control.ExitGame();   // Exit the application
    }

}
