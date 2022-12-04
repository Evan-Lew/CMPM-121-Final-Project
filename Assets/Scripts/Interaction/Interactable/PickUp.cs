using Unity.VisualScripting;
using UnityEngine;

public enum PitObjectType
{
    Consumed, Return,
}
public class PickUp : Interactable
{
    private bool pickedUp;
    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    //refer to list build script
    InteractiveObjectManager _script_interactiveObjectManager;

    public PitObjectType objectType;

    private void Awake()
    {
        _script_interactiveObjectManager = GameObject.Find("Interactable Objects").GetComponent<InteractiveObjectManager>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    public override void Interact()
    {
        if (!pickedUp)
        {
            pickedUp = true;
            //transform.SetParent(PlayerMovement.instance.transform);
            _meshCollider.enabled = false;
            _rigidbody.useGravity = false;
            Interactor.instance.currentPickedObject = gameObject;
            //originalRelativePosition = transform.position;
        }
    }

    public void Drop()
    {
        pickedUp = false;
        // transform.SetParent(null); 
        _meshCollider.enabled = true;
        _rigidbody.useGravity = true;
    }

    private void Update()
    {
        if (pickedUp)
        {
            transform.position = ViewingControl.instance.playerLookVector;
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
                    this.gameObject.SetActive(false);
                    _script_interactiveObjectManager.List_Build();
                    break;
                case PitObjectType.Return:
                    float horizontalElement = .33f;
                    Vector2 randomElement = new Vector2(Random.Range(0, 5), Random.Range(0, 5)).normalized * horizontalElement;
                    _rigidbody.velocity = new Vector3(randomElement.x, 1, randomElement.y).normalized * 20f;
                    print(new Vector3(randomElement.x, 1, randomElement.y).normalized * 20f);
                    _script_interactiveObjectManager.List_Build();
                    break;
            }
        }
    }
}