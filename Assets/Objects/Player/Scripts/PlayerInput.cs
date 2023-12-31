using UnityEngine;
using CustomInputSystem;

namespace SonicFramework
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Player player;
        public InputManager inputManager;

        private void Awake()
        {
            // If script cannot reference player object, then it is not enabled
            if (player == null)
            {
                enabled = false;
                return;
            }
        }

        void Update()
        {
            // Booleans from Player script activate based on input of Keybinds scriptable object
            player.inputUp = inputManager.GetAction("Up");
            player.inputDown = inputManager.GetAction("Down");
            player.inputLeft = inputManager.GetAction("Left");
            player.inputRight = inputManager.GetAction("Right");
            player.inputJump = inputManager.GetAction("Jump");
        }
    }
}

