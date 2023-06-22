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
        public PlayerStats stats;
        #endregion

        #region Variables, Vectors, and More
        float stepDelta;
        [Space(5)]
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
        }

        void Start()
        {
            
        }

        public void Player_Update(float deltaTime)
        {
            StateMachine_Run(nextState); // When this method is called on, it will run the next player state
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
    }
}

