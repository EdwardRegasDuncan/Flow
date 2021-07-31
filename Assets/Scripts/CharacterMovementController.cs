using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] float _speed;
    float baseSpeed = 5f;
    Animator _animator;
    float animatorDefaultSpeed;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public bool sprint = false;

    //calculate player velocity variables
    Vector3 playerVelocity;
    Vector3 prevPos;
    Vector3 currPos;


    private void Awake()
    {
        //get the animator component that's attached to the same GameObject as this script
        //GetComponent is an expensive function so we 'cashe' the reference to the animator component in _animator
        _animator = GetComponent<Animator>();

        //cashe the default animator speed for when we increase it during the sprint function
        animatorDefaultSpeed = _animator.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        //lock the mouse cursor to the middle of the screen and hide it during gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //values for calculating the player velocity
        prevPos = currPos = transform.position;
    }

    //code is run at the begining of the frame before the physics is calculated
    private void FixedUpdate()
    {
        //calculate the player's current velocity
        currPos = transform.position;
        playerVelocity = (currPos - prevPos) / Time.fixedDeltaTime;
        prevPos = currPos;
    }

    // Update is called once per frame
    private void Update()
    {
        //check if the player is pressing left shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprint = true;
        }
        //check if the player is not pressing left shift
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;
        }

        //_animator.speed = sprint ? animatorDefaultSpeed * 2 : animatorDefaultSpeed;
        _animator.SetBool("Sprint", sprint);
        _speed = sprint ? baseSpeed * 2 : baseSpeed;

        //pressing 'a' sets horizontal to -1, pressing 'd' sets it to 1, pressing both or neither sets it to 0
        float horizontal = Input.GetAxis("Horizontal");
        //same but with 'w' and 's'
        float vertical = Input.GetAxis("Vertical");

        //create a vector3 that represents the direction to send the player
        //normalized is used to limit the values to 1 to stop the player moving twice as fast on a diagonal 
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //check if the player
        if (direction.magnitude > 0.1f)
        {
            //tell the animator the character is moving
            _animator.SetBool("Moving", true);

            //get the forward direction based on where the camera is looking
            float targetDirection = cam.eulerAngles.y;

            //smooth the turn speed of the player model so they dont snap to forward direction after idle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDirection, ref turnSmoothVelocity, turnSmoothTime);

            //rotate the player model towards the camera direction
            transform.rotation = Quaternion.Euler(0, angle, 0);

            //add speed to direction and scale by delta time
            //use delta time to run indipendant of the frame rate
            direction *= _speed * Time.deltaTime;

            //apply direction to the player model
            transform.Translate(direction);
        }
        else
        {
            //tell the animator that the player is not moving
            _animator.SetBool("Moving", false);
        }

        //Vector3 localPlayerVelocity = transform.InverseTransformDirection(playerVelocity);
        //float velocityX = Vector3.Dot(direction.normalized, transform.InverseTransformDirection(transform.forward));
        //float velocityZ = Vector3.Dot(direction.normalized, transform.InverseTransformDirection(transform.right));

        //pass the horizontal and vertical values to the animator for calculating the blend space
        _animator.SetFloat("VelocityX", horizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityZ", vertical, 0.1f, Time.deltaTime);
    }
}
