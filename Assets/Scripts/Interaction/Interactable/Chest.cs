using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    // [SerializeField] private string prompt;
    // public string InteractionPrompt => prompt;
    //
    // [SerializeField] private string text;
    // public string paragraphText => text;

    public override void Interact()
    {
        //write your function here 
        Debug.Log(message: "Interact with chest!");
    }
}
