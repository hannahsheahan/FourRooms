using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;


    void Awake()
    {
        SharedInstance = this;
    }

}
