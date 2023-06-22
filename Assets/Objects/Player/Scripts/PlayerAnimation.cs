using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Components and References
    private PlayerInputHandler playerInputHandler;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    

    /// <summary>
    /// These variables here determine the speed of the jogging animation.
    /// In the classic Sonic games, his jog animation plays slow, then the animation speed picks up as Sonic gains more speed
    /// The code here is TEMPORARY, but it does serve the purpose.
    /// I want to grab the ACTUAL accerlation and max speed from PlayerStats or from PlayerPhysics
    /// </summary>

    // Variables
    private float speed;
    private float maxSpeed = 2.5f; // Adjust this value to define the maximum speed (Based on motion blend tree total speed)
    private float acceleration = 4f; // Adjust this value to control the acceleration rate

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        // ADD METHODS HERE
        JogAnimation();
    }

    private void IdleAnimation()
    {
        // Idle Blend Tree Animations will be played here
        // Make use of Coroutines?
        // Idle blend tree to play idle animations has not be implemented yet
    }

    private void JogAnimation()
    {
        float movementInput = playerInputHandler.movementInput.x;

        // Determines how quickly player will transition from Idle to Jog.
        // Frames within Motion Blend Tree change speed (Also check those values there)
        if (movementInput != 0)
        {
            speed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.MoveTowards(speed, 0, acceleration * Time.deltaTime);
        }

        animator.SetFloat("Speed", speed);

        // Flips sprite depending on X direction
        if (movementInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
