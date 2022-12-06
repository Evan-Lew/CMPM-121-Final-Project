using Unity.VisualScripting;
using UnityEngine;
using EZCameraShake;

public enum PitObjectType
{
    Consumed, Return,
}
public class PickUp : Interactable
{
    private bool pickedUp;
    private Rigidbody _rigidbody;
    private Collider _collider;

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
        _collider = GetComponent<Collider>();
    }

    public override void Interact()
    {
        if (!pickedUp)
        {
            pickedUp = true;
            //transform.SetParent(PlayerMovement.instance.transform);
            //_collider.enabled = false;
            _rigidbody.useGravity = false;
            Interactor.instance.currentPickedObject = gameObject;
            //originalRelativePosition = transform.position;
        }
    }

    public void Drop()
    {
        pickedUp = false;
        // transform.SetParent(null); 
        //_collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (pickedUp)
        {
            Vector3 moveDir = (ViewingControl.instance.playerLookVector - transform.position);
            _rigidbody.velocity = moveDir * 5;
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
                    CameraShaker.Instance.ShakeOnce(4f, 4f, 1.5f, 1.5f);
                    gameObject.SetActive(false);
                    _script_interactiveObjectManager.List_Build();
                    break;
                case PitObjectType.Return:
                    CameraShaker.Instance.ShakeOnce(4f, 4f, 1.5f, 1.5f);
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