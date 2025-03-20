using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float currentSpeed = 0, acceleration = 40;
    [SerializeField] private float turnSpeed = 100, minAcceleration, maxAcceleration, minSpeed, maxSpeed;
    [SerializeField] private KeyCode leftInput, rightInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isGrounded = Physics.Linecast(transform.position, groundCheck.position, groundLayer);

        if (isGrounded)
        {
            if (Input.GetKey(leftInput) && transform.eulerAngles.y < 269)
            {
                transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0), Space.Self);
            }
            if (Input.GetKey(rightInput) && transform.eulerAngles.y > 91)
            {
                transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0), Space.Self);
            }
        }
    }

    private float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) / oldRange) * newRange + newMin);
        return newValue;
    }

    private void FixedUpdate()
    {
        
        float angle = Mathf.Abs(180 - transform.eulerAngles.y);
        acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        animator.SetFloat("playerSpeed", currentSpeed);
        Vector3 velocity = transform.forward * currentSpeed * Time.fixedDeltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }
}
