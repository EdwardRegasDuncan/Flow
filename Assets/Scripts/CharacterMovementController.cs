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


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        animatorDefaultSpeed = _animator.speed;
    }

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;
        }

        //_animator.speed = sprint ? animatorDefaultSpeed * 2 : animatorDefaultSpeed;
        _animator.SetBool("Sprint", sprint);
        _speed = sprint ? baseSpeed * 2 : baseSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetDirection = cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDirection, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            direction *= _speed * Time.deltaTime;
            transform.Translate(direction);
        }

        _animator.SetFloat("VelocityX", horizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityZ", vertical, 0.1f, Time.deltaTime);
    }
}
