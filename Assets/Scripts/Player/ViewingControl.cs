using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingControl : MonoBehaviour
{
    private GameObject Player;
    private PlayerMovement _script_PlayerMovement;

    public static ViewingControl instance;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool enableCursor;
    private Transform lookVectorObject;
    [HideInInspector]public Vector3 playerLookVector;

    float mouseX ;
    float mouseY ;

    float xRotation = 0f;

    private void Awake()
    {
        instance = this;
        Player = GameObject.Find("Player");
        _script_PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lookVectorObject = transform.Find("LookVector");
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = enableCursor;

        //camera enable only if player is enable to act
        if (_script_PlayerMovement.enableControl)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;



            //mouse X will rotate player
            playerBody.Rotate(Vector3.up * mouseX);

            //mouse Y will NOT rotate player but rotate camera
            xRotation -= mouseY;
            //limit the angle, so it won't look behind player
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerLookVector = lookVectorObject.position;
        }



    }
}