using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : Interactable
{
    private GameObject global;
    private GameManager global_init;

    private void Start()
    {
        global = GameObject.Find("Global");
        global_init = global.GetComponent<GameManager>();
    }



    public override void Interact()
    {
        global_init.keyList.Find(x => x.name == "Trophy").isInteracted = true;
  
        Debug.Log(message: "Opening Trophy!");
    }
}
