﻿using UnityEngine;

public enum PitObjectType
{
    Consumed, Return, 
}
public class PickUp : Interactable 
{
    private bool pickedUp;
    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    public PitObjectType objectType;

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
            transform.SetParent(PlayerMovement.instance.transform);
            _meshCollider.enabled = false;
            _rigidbody.useGravity = false;
            Interactor.instance.currentPickedObject = gameObject;
            //originalRelativePosition = transform.position;
        }
    }

    public void Drop()
    {
        pickedUp = false;
        transform.SetParent(null); 
        _meshCollider.enabled = true;
        _rigidbody.useGravity = true;
    }

    private void Update()
    {
        if (pickedUp)
        {
            transform.localPosition = ViewingControl.instance.playerLookVector * 5f;
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
                    _rigidbody.velocity = Vector3.up * 20f;
                    break;
            }
        }
    }
}