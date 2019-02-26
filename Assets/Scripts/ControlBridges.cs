using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ControlBridges : MonoBehaviour
{
    // the walkable bridges
    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject bridge3;
    public GameObject bridge4;

    // the freeze triggers
    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;
    public GameObject trigger4;

    // ********************************************************************** //

    private void Update()
    {
        // could put a thing here where you check for the scene, but would 
        // require more changes updating for future scenes so a bit annoying

        if (GameController.control.State != GameController.STATE_EXIT) 
        { 
            bridge1.SetActive(GameController.control.bridgeStates[0]);
            trigger1.SetActive(GameController.control.bridgeStates[0]);

            bridge2.SetActive(GameController.control.bridgeStates[1]);
            trigger2.SetActive(GameController.control.bridgeStates[1]);

            bridge3.SetActive(GameController.control.bridgeStates[2]);
            trigger3.SetActive(GameController.control.bridgeStates[2]);

            bridge4.SetActive(GameController.control.bridgeStates[3]);
            trigger4.SetActive(GameController.control.bridgeStates[3]);
        }
    }
}
