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
            Sonic = 0
        }

        [Header("Components")]
        public PlayerStats stats;

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

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            TestMovement();
        }

        private void FixedUpdate()
        {
            
        }

        void TestMovement()
        {
            if (inputRight)
            {
                transform.position += 2 * transform.right * Time.deltaTime;
            }
        }
    }
}

