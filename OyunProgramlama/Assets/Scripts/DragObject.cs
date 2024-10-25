using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody rb;
    private bool isDragging = false;
    private float hoverHeight = 5.0f; // Obje ne kadar yükselecek
    private Vector3 offset;

    private InputAction clickAction;

    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

        // Yeni Input System için sol tıklama aksiyonu oluşturma
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        clickAction.performed += OnClickStarted;
        clickAction.canceled += OnClickReleased;
        clickAction.Enable();
    }

    void Update()
    {
        if (isDragging)
        {
            // Objeyi fare hareketleriyle birlikte hareket ettir
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
            targetPosition.y = hoverHeight; // Obje belirlenen yükseklikte durur
            rb.MovePosition(targetPosition);
        }
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        // Sol tıklama başladığında raycast ile obje seçimi yap
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
        {
            isDragging = true;
            rb.useGravity = false;
            offset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), mainCamera.WorldToScreenPoint(transform.position).z));
        }
    }

    private void OnClickReleased(InputAction.CallbackContext context)
    {
        // Sol tıklama bırakıldığında
        isDragging = false;
        rb.useGravity = true;
    }

    void OnDestroy()
    {
        // InputAction'ı devre dışı bırakma
        clickAction.performed -= OnClickStarted;
        clickAction.canceled -= OnClickReleased;
        clickAction.Disable();
    }
}
