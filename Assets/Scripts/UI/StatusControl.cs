using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusControl : MonoBehaviour
{



    public List<Image> statusImagesList;
    //refer to list build script
    InteractiveObjectManager _script_interactiveObjectManager;

    private void Awake()
    {
        _script_interactiveObjectManager = GameObject.Find("Interactable Objects").GetComponent<InteractiveObjectManager>();
        buildList();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buildList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            statusImagesList.Add(transform.GetChild(i).gameObject.GetComponent<Image>());
            statusImagesList[i].enabled = true;
        }
    }

    //this will be called whenthe list is changing 
    public void DisplayStatus(List<GameObject> list_CluesInPit)
    {
        int count = list_CluesInPit.Count;
        for(int i = count - 1; i >= 0; i--)
        {
            statusImagesList[i].enabled = false;
        }
    }
}
