using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log(message: "Opening Book!");
    }
}
