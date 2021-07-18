using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    Animator _animator;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    // Update is called once per frame
    void Update()
    {

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

    void UpdateAnimator(Vector3 movement)
    {
        float velocityX = movement.x;
        float velocityZ = movement.z;

        
    }
}
