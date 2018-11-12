using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource source;

    // ********************************************************************** //

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // ********************************************************************** //

    public void StartGameOnClick()
    {
        source.PlayOneShot(buttonClickSound, 1F);
        GameController.control.StartGame();   // Launch first real trial
    }
}
