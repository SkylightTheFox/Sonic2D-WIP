using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInputSystem
{
    /// <summary>
    /// The following code allows for the creation of a scriptable object that allows keybinds to be added
    /// Create "KeyBindings" asset, and start assigning names/actions to the strings
    /// </summary>

    [CreateAssetMenu(fileName = "KeyBindings", menuName = "ScriptableObjects/KeyBindings", order = 0)]
    public class InputActions : ScriptableObject
    {
        [System.Serializable] // Tells instances of this type to be saved or loaded
        public class ActionButton
        {
            // Will be visually represented within scriptable object
            public string ActionName; // In editor, specify what action it is (Up, Down, Left, Action, Accept).
            public List<string> positiveKeyCodes = new List<string>(1); // These are the ACTUAL keys or control names (LeftArrow, RightStick, etc). 
            public List<string> negativeKeyCodes = new List<string>(1); // These are... I don't really know. They don't seem to be used. Returns negative value
        }

        public List<ActionButton> ActionButtons; // This will be visible within editor, and you can hit the "+" icon to add these bindings to the list
    }
}

