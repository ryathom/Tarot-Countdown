using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputAction pointAction;
    private InputAction clickAction;
    private InputAction middleClickAction;
    private InputAction scrollAction;
    private InputAction cancelAction;

    // Events
    public Action OnClickAction;
    public Action OnMiddleClickAction;
    public Action OnCancelAction;

    public static InputManager Instance;

    // Main Unity methods
    //---------------------------------------------------------------------------------------------------------
    private void Awake() 
    {
        if (Instance == null) {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pointAction = InputSystem.actions.FindAction("Point");
        clickAction = InputSystem.actions.FindAction("Click");
        middleClickAction = InputSystem.actions.FindAction("MiddleClick");
        scrollAction = InputSystem.actions.FindAction("ScrollWheel");
        cancelAction = InputSystem.actions.FindAction("Cancel");
    }

    private void Update()
    {
        if (clickAction.WasPressedThisFrame())
        {
            OnClickAction?.Invoke();
            Debug.Log("Cancel");
        }

        if (middleClickAction.WasPressedThisFrame())
        {
            OnMiddleClickAction?.Invoke();
        }

        if (cancelAction.WasPressedThisFrame())
        {
            OnCancelAction?.Invoke();
        }
    }

    // Expose inputs
    //---------------------------------------------------------------------------------------------------------
    public Vector2 GetPointInput()
    {
        // return pointAction.ReadValue<Vector2>();
        return Mouse.current.position.ReadValue();
    }

    public Vector2 GetScrollInput()
    {
        return scrollAction.ReadValue<Vector2>();
    }

    public bool GetMiddleClick()
    {
        return middleClickAction.IsPressed();
    }
}