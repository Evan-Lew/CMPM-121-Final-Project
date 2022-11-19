using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interactable
{
    public override void Interact()
    {
        Debug.Log(message: "Opening Book!");
    }
}
