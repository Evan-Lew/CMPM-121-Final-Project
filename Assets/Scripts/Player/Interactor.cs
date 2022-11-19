using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactor : MonoBehaviour
{

    public static Interactor instance;
    [SerializeField] private Transform interactionPoint;

    [SerializeField] private LayerMask interactableMask;
    //[SerializeField] private InteractionPromptUI _interactionPromptUI;
    private readonly Collider[] colliders = new Collider[3];
    private int numFound;

    public GameObject currentPickedObject;

    [Header("Radius of Interaction Sphere")]
    [Tooltip("This will change the sphere in front of the player for interaction check")]
    [SerializeField] private float interactionPointRadius = 0.5f;

    [Header("Setup Interaction Button")]
    [Tooltip("This button will be used for interaction. Once hit the button, the interaction function in object will be run")]
    public KeyCode interactionKey;


    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            if (currentPickedObject != null)
            {
                currentPickedObject.GetComponent<PickUp>().Drop();
                currentPickedObject = null;
                return;
            }

            numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders,
                interactableMask);

            if (numFound > 0)
            {
                Interactable interactable = colliders[0].GetComponent<Interactable>();
                if (interactable != null)
                {
                    //if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(interactable.InteractionPrompt);
                    interactable.Interact();
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
