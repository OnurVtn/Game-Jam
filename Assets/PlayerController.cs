using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardMovementSpeed, sideMovementSensitivity;
    [SerializeField] private Transform leftLimit, rightLimit;

    private Vector2 inputDrag, previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x;

    private float rightLimitX => rightLimit.localPosition.x;

    void Start()
    {
        
    }

    void Update()
    {
        HandleForwardMovement();
        HandleInput();
        HandleSideMovement();
    }

    private void HandleForwardMovement()
    {
        transform.localPosition += Vector3.forward * Time.deltaTime * forwardMovementSpeed;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var deltaMouse = (Vector2)Input.mousePosition - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = Input.mousePosition;
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }

    private void HandleSideMovement()
    {
        var localPos = transform.localPosition;
        localPos += Vector3.right * inputDrag.x * sideMovementSensitivity;

        localPos.x = Mathf.Clamp(localPos.x, leftLimitX, rightLimitX);

        transform.localPosition = localPos;
    }
}
