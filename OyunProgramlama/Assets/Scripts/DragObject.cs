using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody rb;
    private bool isDragging = false;
    private float hoverHeight = 5.0f; 
    private Vector3 offset;
    private float maxRayDistance = 100.0f;

    private InputAction clickAction;

    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        clickAction.performed += OnClickStarted;
        clickAction.canceled += OnClickReleased;
        clickAction.Enable();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Vector3.Distance(mainCamera.transform.position, transform.position); 
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
            targetPosition.y = hoverHeight; 
            rb.MovePosition(targetPosition);
        }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
        {
            Debug.Log("Ray nesneye çarptı: " + hit.transform.name);
        }

        
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        float maxRayDistance = 100.0f; 
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance) && hit.transform == transform)
        {
            isDragging = true;
            rb.useGravity = false;
            Vector3 hitPoint = mainCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), hit.distance));
            offset = transform.position - hitPoint;
        }
    }

    private void OnClickReleased(InputAction.CallbackContext context)
    {
        isDragging = false;
        rb.useGravity = true;
    }

    void OnDestroy()
    {
        clickAction.performed -= OnClickStarted;
        clickAction.canceled -= OnClickReleased;
        clickAction.Disable();
    }
}
