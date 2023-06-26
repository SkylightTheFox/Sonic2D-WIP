using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CustomInputSystem;

namespace SonicFramework
{
    public class Player : MonoBehaviour
    {
        public enum Character
        {
            Sonic = 0,
            Tails = 1,
            Knuckles = 2,
        }

        #region Components
        public static Player leader; // Static means it belongs to the class, and not the object
        public static PlayerInput input;
        public static InputManager inputManager;
        public PlayerStats stats;
        #endregion

        #region Variables, Vectors, and More
        float stepDelta;
        
        public Vector2 position;
        public Vector2 velocity;
        [Space(5)]
        public float groundSpeed;
        public float groundAngle;
        [Space(5)]
        #endregion

        #region Ground Check
        public bool isGrounded;
        #endregion

        #region Booleans
        public bool isLeader;
        public bool isRolling;
        #endregion

        #region Input Booleans
        // These booleans will activate when Actions from Keybinds are used (processed through InputManager and InputActions scripts)
        [System.NonSerialized] public bool inputJump;
        [System.NonSerialized] public bool inputJumpLastFrame;

        [System.NonSerialized] public bool inputLeft;
        [System.NonSerialized] public bool inputRight;
        [System.NonSerialized] public bool inputUp;
        [System.NonSerialized] public bool inputUpLastFrame;
        [System.NonSerialized] public bool inputDown;
        [System.NonSerialized] public bool inputDownLastFrame;
        #endregion

        void OnEnable()
        {
            Initialize_Player_State();

            // Leader is main player (Controls primary input)
            // Other players connected won't be leader
            // Other players are secondary characters
            if (isLeader)
            {
                if (leader == null)
                {
                    leader = this;
                }
            }

            if (isLeader)
            {
                // This doesn't work
                // input = GetComponent<PlayerInput>();
                // inputManager = GetComponent<InputManager>();
            }
        }

        void Start()
        {
            Initialize_Player_Position();
        }

        public void Player_Update(float deltaTime)
        {
            stepDelta = deltaTime;
            StateMachine_Run(nextState); // When this method is called on, it will run the next player state

            Apply_Player_Movement();
        }

        public void Player_Late_Update()
        {

        }

        #region State Machine and States
        public delegate void state();
        public state currentState;
        public state nextState;

        void Initialize_Player_State()
        {
            nextState = Player_State_Grounded; // When loaded into game, this is the initial state of player
        }

        void StateMachine_Run(state stateMethod)
        {
            currentState = stateMethod;
            stateMethod();
        }

        /// <summary>
        /// This is where all player states are listed
        /// Depending on condition, transition to next state from current one
        /// </summary>
        void Player_State_Grounded()
        {
            Player_Handle_Ground_Movement();
            Debug.Log("ground state");

            // If no longer grounded, switch to airborne state
            if (!isGrounded) nextState = Player_State_Airborne;
        }
        
        void Player_State_Airborne()
        {
            Debug.Log("air state");

            // If no longer airborne, switch to grounded state
            if (isGrounded) nextState = Player_State_Grounded;
        }

        void Player_State_Roll()
        { 
            Debug.Log("roll state");
        }
        #endregion

        #region Ground Handlers
        void UpdatePosition()
        {
            position += velocity * stepDelta / 16f; // Velocity is added gradually to transform positon over time
        }
        
        void Apply_Player_Movement()
        {
            transform.position = position;
        }

        void Initialize_Player_Position()
        {
            position = transform.position;
        }

        void Player_Handle_Ground_Movement()
        {
            // Ground speed is decreased when going up steep slopes
            groundSpeed -= stats.slopeFactor * Mathf.Sin(groundAngle * Mathf.Deg2Rad) * stepDelta;
            
            // Ground speed is updated based on Input and applies acceleration/deceleration
            Handle_Ground_Input();

            // Move player
            velocity.x = groundSpeed * Mathf.Cos(groundAngle * Mathf.Deg2Rad);
            velocity.y = groundSpeed * Mathf.Sin(groundAngle * Mathf.Deg2Rad);
            UpdatePosition();
        }
        #endregion

        #region Input Handler
        void Handle_Ground_Input()
        {
            if (inputRight && !inputLeft)
            {
                Debug.Log("Right");
                if (groundSpeed < 0) // If left input is applied, the following code is executed in this IF statement
                {
                    if (!isRolling) // (Emulates skiding to a stop)
                    {
                        groundSpeed += stats.groundDeceleration * stepDelta; // Apply ground deceleration (Adding negative value to positive X direction)
                    }
                    else
                    {
                        groundSpeed += stats.rollDeceleration * stepDelta; // Apply roll deceleration
                    }

                    if (groundSpeed >= 0)
                    {
                        groundSpeed = 0.5f * stepDelta; // Apply gradual deceleration
                    }
                }
                else // When normally moving right
                {
                    if (groundSpeed < stats.topGroundSpeed) // If not at top speed, allow code to be run
                    {
                        if (!isRolling)
                        {
                            groundSpeed += stats.groundAcceleration * stepDelta; // Apply ground acceleration
                        }
                        if (groundSpeed >= stats.topGroundSpeed) // If top speed is reached, apply speed limit
                        {
                            groundSpeed = stats.topGroundSpeed;
                        }
                    }
                }
            }
                
            if (inputLeft && !inputRight)
            {
                Debug.Log("Left");
                if (groundSpeed > 0) // When moving right, but initially facing left, this code is executed
                {
                    if (!isRolling) // (Emulates skiding to a stop)
                    {
                        groundSpeed -= stats.groundDeceleration * stepDelta; // Apply ground deceleration (Subtracing negative value to negative X direction)
                    }
                    else
                    {
                        groundSpeed -= stats.rollDeceleration * stepDelta; // Apply roll deceleration
                    }

                    if (groundSpeed <= 0)
                    {
                        groundSpeed = -0.5f * stepDelta; // apply gradual deceleration (Remove step delta from this?)
                    }
                }
                else // When normally moving left
                {
                    if (groundSpeed > -stats.topGroundSpeed) // If not at negative top speed, allow code to be run
                    {
                        if (!isRolling)
                        {
                            groundSpeed -= stats.groundAcceleration * stepDelta; // Apply ground acceleration
                        }
                        if (groundSpeed <= -stats.topGroundSpeed) // If top speed reached, apply speed limit
                        {
                            groundSpeed = -stats.topGroundSpeed;
                        }
                    }
                }
            }

            if (!inputRight && !inputLeft)
            {
                // Artificial Friction
                if (Mathf.Abs(groundSpeed) > 0)
                {
                    float groundSpeedSign = Mathf.Sign(groundSpeed);
                    groundSpeed -= stats.groundFriction * groundSpeedSign * stepDelta;

                    if (Mathf.Sign(groundSpeed) != groundSpeedSign)
                    {
                        groundSpeed = 0;
                    }
                }
            }          
        }
        #endregion
    }
}

