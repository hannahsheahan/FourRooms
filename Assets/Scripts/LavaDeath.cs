using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDeath : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        // You've fallen into the lava, so restart the trial.
        GameController.control.FallDeath();
    }
}
