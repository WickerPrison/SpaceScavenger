using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    Vector2 moveDirection;
    float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.controls.Combat.MoveCamera.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        InputManager.instance.controls.Combat.MoveCamera.canceled += ctx => moveDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection.normalized);
    }
}
