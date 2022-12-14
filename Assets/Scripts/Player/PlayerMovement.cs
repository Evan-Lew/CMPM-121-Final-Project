using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    //speed of movement
    public float speed;
    //current controller
    public CharacterController controller;


    //check collision with ground for jumping
    public Transform groundCheck;
    public LayerMask groundMask;
    public float jumpHeight = 3f;




    //variable used to control switcher
    [HideInInspector] public bool firstPersonEnabled;
    [HideInInspector] public bool thirdPersonEnabled;

    //control if player controller is enabled or not
    [HideInInspector]public bool enableControl;

    float xMoveVector;
    float zMoveVector;

    float angle;
    float smoothAngle;
    float turnSmooth_time = 0.1f;
    float turnSmooth_velocity;
    const float gravity = -18f;
    Vector3 direction;
    Vector3 actual_moveDirection;
    Vector3 velocity;
    Vector3 teleportPos;
    bool isGrounded;
    public float collidingDistance = 0.4f;

    public LayerMask pitBottomMask;
    [HideInInspector] public Vector3 startPos;
    bool isPitted;
    public bool enablePitFallFeature;
    public GameObject FadeOut;
    FadeOut FadeOut_Script;

    //third person camera that you want the player to follow
    GameObject thirdPersonCamera;
    Transform thirdPersonCameraTransform;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        firstPersonEnabled = true;
        thirdPersonEnabled = false;
        //enableControl will be init by gameManager
        //enableControl = true;
        enablePitFallFeature = true;
        startPos = transform.position;
        FadeOut_Script = FadeOut.GetComponent<FadeOut>();
        

    }


    private void FixedUpdate()
    {
        //physic related
        if (enableControl)
        {

            //=================================================================
            /*                      player movement setup                    */
            PlayerMovementSetup();
            /*                             end                               */
            //=================================================================
            playerPosValidationCheck();



        }
    }

    private void Update()
    {


        if (enableControl)
        {
            //=================================================================
            /*                           Jumping                             */
            JumpFeature();
            /*                             end                               */
            //=================================================================

            //player control in first person camera
            if (firstPersonEnabled)
            {


                //=================================================================
                /*                       movement control                        */
                FirstPersonControl();

                playMovementSound();

                /*                             end                               */
                //=================================================================
            }

            //player control in third person camera
            if (thirdPersonEnabled)
            {
                //=================================================================
                /*                       movement control                        */
                ThirdPersonControl();
                /*                             end                               */
                //=================================================================
            }

        }
    }

    void playMovementSound()
    {
 
        if (!SoundManager.audioSrc_footStep.isPlaying)
        {
            if (isGrounded && controller.velocity.magnitude != 0)
            {
                SoundManager.PlaySound("sfx_Footstep", 1);
            }

        }

        //during the playtime if player stop, the sound end 
        if (SoundManager.audioSrc_footStep.isPlaying)
        {

            if (!isGrounded || controller.velocity.magnitude == 0)
            {

                SoundManager.audioSrc_footStep.Pause();
            }
        }
    }

void playerPosValidationCheck()
    {
        isPitted = Physics.CheckSphere(groundCheck.position, collidingDistance, pitBottomMask);
        if (isPitted && enablePitFallFeature)
        {
            SoundManager.PlaySound("sfx_WilhemScream", 1);
            enablePitFallFeature = false;
            FadeOut.SetActive(true);
            FadeOut_Script.FadingEvent();
        }
    }



    void PlayerMovementSetup()
    {
        //get key wasd and arrow key
        xMoveVector = Input.GetAxisRaw("Horizontal");
        zMoveVector = Input.GetAxisRaw("Vertical");



        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, collidingDistance);
        foreach (var hitCollider in hitColliders)
        {
            //check collison
        }
    }

    void JumpFeature()
    {

        //create an invisiable sphere use to check if it's colliding with certain layer
        isGrounded = Physics.CheckSphere(groundCheck.position, collidingDistance, groundMask);


        //reset y velocity when it's on ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -100f;
        }

        //jump features
        //applies to both first person and third person camera
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            CameraShaker.Instance.ShakeOnce(2f, 2f, .5f, 0.5f);
            //v = sqrt (h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            SoundManager.PlaySound("sfx_Jump", 2);
        }


        if (!isGrounded)
        {
            //apply gravity
            velocity.y += gravity * Time.deltaTime;

        }
        //x = vt
        if (velocity.y != -100f)
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void FirstPersonControl()
    {



        direction = transform.right * xMoveVector + transform.forward * zMoveVector;
        controller.Move(direction.normalized * speed * Time.deltaTime);
    }

    void ThirdPersonControl()
    {

        //find moving angle in vector
        direction = new Vector3(xMoveVector, 0f, zMoveVector).normalized;

        if (direction.magnitude >= 0.1f)
        {

            //convert vector3 into angle + add camera angle, so the player will go toward
            angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + thirdPersonCameraTransform.eulerAngles.y;
            //get smooth angle for turning
            smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmooth_velocity, turnSmooth_time);
            //make player rotate
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            //make finding moving direction of camera
            actual_moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            //make player move
            controller.Move(actual_moveDirection.normalized * speed * Time.deltaTime);

        }

    }



    //old code about teleporting
    //IEnumerator Teleport()
    //{
    //    //avoid overlapping pos control
    //    enableControl = false;
    //    yield return new WaitForSeconds(0.1f);
    //    //teleport to location
    //    gameObject.transform.position = teleportPos;
    //    //regenerate the room objects
    //    global_setting.regeneratePos = true;
    //    yield return new WaitForSeconds(0.1f);
    //    enableControl = true;

    //}
}




