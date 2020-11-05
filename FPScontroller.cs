using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPScontroller : MonoBehaviour
{
    public float walkingSpeed = 10f;
    public float runningSpeed = 30f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public GameObject gc;
    public bool started = false;
    public float runTimer = 0f;
    public bool isRunning = false;

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (!gc.GetComponent<GameController>().HasStarted)
        {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Confined;
            transform.position = new Vector3(0f, 12f, 0f);
            Cursor.visible = true;
            started = false;
        }
        if (!started && gc.GetComponent<GameController>().HasStarted)
        {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            started = true;
        }
        else if (started)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            if (isRunning)
            {
                runTimer += Time.deltaTime;
                if (runTimer < 0.5f)
                {
                    playerCamera.fieldOfView = Mathf.Lerp(70f, 105f, (runTimer * 2));
                }
                if (runTimer > 2.5f)
                {
                    playerCamera.fieldOfView = Mathf.Lerp(105f, 70f, ((runTimer - 2.5f) * 2.0f));
                }
                if (runTimer > 3f)
                {
                    playerCamera.fieldOfView = 70f;
                    isRunning = false;
                    runTimer = 0f;
                }
            }

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            if (transform.position.y > 38)
            {
                moveDirection.y -= 1;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
        
    }
}
