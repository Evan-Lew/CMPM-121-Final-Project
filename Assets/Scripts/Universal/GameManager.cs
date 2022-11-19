using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public class keyObjects
    {
        public string name;
        public GameObject obj;
        public bool isInteracted; 
    }

    public  List<keyObjects> keyList = new List<keyObjects>();
    public keyObjects tempKey;

    private void Awake()
    {
        instance = this;
        
        tempKey = new keyObjects();
        tempKey.name = "Notepad";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);


        tempKey = new keyObjects();
        tempKey.name = "Trophy";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);


        tempKey = new keyObjects();
        tempKey.name = "Blox";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);


        tempKey = new keyObjects();
        tempKey.name = "Box";
        tempKey.isInteracted = false;
        tempKey.obj = null;
        keyList.Add(tempKey);
    }


}
