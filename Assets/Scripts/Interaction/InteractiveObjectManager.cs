using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectManager : MonoBehaviour
{
    [Tooltip("This script is used to test all status of children objects from the parents objects that is referenced." +
        "\n\n Bool isFoundAllKey can be used to check the game status")]
    public string scriptDescription = "Read Description";

    [Space(20)]

    [SerializeField] private GameObject clueObjectsParent;
    [SerializeField] private GameObject nonClueObjectsParent;


    [HideInInspector]public StatusControl script_StatusControl;

    public List<GameObject> List_Clues;
    public List<GameObject> List_CluesInPit;



    //for reserved 
    public List<GameObject> List_NonClues;

    [HideInInspector] public bool isFoundAllKey;

    private void Awake()
    {
        script_StatusControl = GameObject.Find("UI/Status Bar").GetComponent<StatusControl>();
        List_Build();
    }

    // Update is called once per frame
    void Update()
    {
        checkKeys();
    }

    //check if all keys are in the pit (simply check if list is empty because consumed keys will be removed)
    private void checkKeys()
    {
        if (List_Clues.Count == 0)
        {
            isFoundAllKey = true;
        }
        else
        {
            isFoundAllKey = false;
        }

    }

    //build the list based on the current objects' status 
    public void List_Build()
    {

        //clean before build
        List_Clues.Clear();
        List_NonClues.Clear();
        List_CluesInPit.Clear();

        //put all children into the list
        for (int i = 0; i < clueObjectsParent.transform.childCount; i++)
        {
            //check if children is active
            if (clueObjectsParent.transform.GetChild(i).gameObject.activeSelf)
            {
                //if it's active put into the list
                GameObject temp = new GameObject();
                temp = clueObjectsParent.transform.GetChild(i).gameObject;
                List_Clues.Add(temp);
            }
            else
            {
                //add those disabled objects into another list
                GameObject temp = new GameObject();
                temp = clueObjectsParent.transform.GetChild(i).gameObject;
                List_CluesInPit.Add(temp);
                script_StatusControl.DisplayStatus(List_CluesInPit);
            }
        }//for end

        for (int i = 0; i < nonClueObjectsParent.transform.childCount; i++)
        {
            //check if children is active
            if (nonClueObjectsParent.transform.GetChild(i).gameObject.activeSelf)
            {
                //if it's active put into the list
                GameObject temp = new GameObject();
                temp = nonClueObjectsParent.transform.GetChild(i).gameObject;
                List_NonClues.Add(temp);
            }
        }//for end
    }
}
