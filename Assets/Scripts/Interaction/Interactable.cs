using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // public string InteractionPrompt { get; }
    // public string paragraphText { get; }
    public abstract void Interact();
}
