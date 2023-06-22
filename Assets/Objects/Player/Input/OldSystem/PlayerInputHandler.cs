using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    /// <summary>
    /// This script NEEDS to be referenced in other scripts, such as "PlayerMotor."
    /// Script reads input from device player is using.
    /// DO NOT FORGET to add a "Player Input component" to Player object, and add the Event!
    /// </summary>

    // Declare InputActions
    public InputAction movementAction;
    public InputAction jumpAction;

    /// <summary>
    /// EVENTS:
    /// By having public events, other scripts can "subscribe" to them.
    /// For example, OnJump invokes the event, and then in a physics script, you can subscribe to the event within the Start method, and create that event method to hold actual physics.
    /// Its a great way to keep Input script and Physics separate.
    /// </summary>

    // Events
    public delegate void JumpAction(); // NEED TO LEARN WHAT THIS MEANS
    public event JumpAction HandleJump; // WHAT DOES THIS MEAN?

    [HideInInspector] public Vector2 movementInput;

    // Bools
    [HideInInspector] public bool jumpPressed;

    private void OnEnable()
    {
        // Enables InputActions (Means it will start listening for Input Events)
        movementAction.Enable();
        jumpAction.Enable();
        
        movementAction.performed += OnMovement;
        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        // Disables InputActions (Means these events are "unsubscribed," and prevent input conflicts and memory leaks)
        movementAction.Disable();
        jumpAction.Disable();

        movementAction.performed -= OnMovement;
        jumpAction.performed -= OnJump;
    }

    #region Input Methods
    public void OnMovement(InputAction.CallbackContext context)
    {
        // Reads movement values from controller or Keyboard
        movementInput = context.ReadValue<Vector2>();

        // Check Unity debug console
        Debug.Log("Movement Input: " + movementInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Checks to see if button was pressed
        if (context.performed)
        {
            /// <summary>
            /// The HandleJump Method is referenced in "PlayerPhysics" script
            /// The "JumpMotor" method from the Physics script subscribes to the event... i think...?
            /// In other words, Jump physics is now separate from Input Script
            /// </summary>
            if (HandleJump != null)
            {
                HandleJump();
                jumpPressed = true;
            }
        }
    }
    #endregion
}
