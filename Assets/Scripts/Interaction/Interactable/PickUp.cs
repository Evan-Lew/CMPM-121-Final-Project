using UnityEngine;

public enum PitObjectType
{
    Consumed, Return, 
}
public class PickUp : MonoBehaviour, IInteractable
{
    private bool pickedUp;
    private Rigidbody rigidbody;

    public PitObjectType objectType;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        if (!pickedUp)
        {
            pickedUp = true;
            transform.SetParent(PlayerMovement.instance.transform);
            Interactor.instance.currentPickedObject = gameObject;
            //originalRelativePosition = transform.position;
        }
    }

    public void Drop()
    {
        pickedUp = false;
        transform.SetParent(null); 
    }

    private void Update()
    {
        if (pickedUp)
        {
            //transform.localPosition = originalRelativePosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Pit"))
        {
            switch (objectType)
            {
                case PitObjectType.Consumed:
                    break;
                case PitObjectType.Return:
                    rigidbody.velocity = Vector3.up * 20f;
                    break;
            }
        }
    }
}