using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Camera camera;
    public float speed;
    public float jumpPower;
    public float rotationSpeed;
    private Vector2 movementValue;
    private float lookValue;
    private float cameraLook;
    private Rigidbody rb;

    private bool doubleJump = false;
    private bool isFloor = true;

    public Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>() * speed;
        anim.SetFloat("X", value.Get<Vector2>().x);
        anim.SetFloat("Y", value.Get<Vector2>().y);
        anim.SetBool("Aiming", true);
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x * rotationSpeed;
        cameraLook = - value.Get<Vector2>().y * rotationSpeed;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isFloor)
        {
            rb.AddRelativeForce(0, jumpPower, 0);
            isFloor = false;
            doubleJump = true;
        }
    }

    public void OnDoubleJump(InputValue value)
    {
        if (value.isPressed && doubleJump && !isFloor)
        {
            DoubleJump();
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime);
        rb.AddRelativeTorque(0, lookValue * 20 * Time.deltaTime, 0);
    }

    private void LateUpdate()
    {
        camera.transform.Rotate(Mathf.Clamp(cameraLook, -30, 30) * Time.deltaTime, 0, 0);
    }

    void DoubleJump()
    {
        rb.AddRelativeForce(0, jumpPower, 0);
        doubleJump = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isFloor = true;
        }
    }
}
