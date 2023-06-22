using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInputSystem
{
    /// <summary>
    /// This script gathers the strings from "InputActions" scriptable and processes them through here.
    /// For anything thats not self-explanatory, I went ahead and "translated"
    /// Code needs to be studied more, look at it later
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance; // What does this mean exactly?
        [SerializeField] private InputActions keybinds; // Scriptable object for keybinds. (Reference within editor)

        public List<KeyCode> GetActionKeys(string actionName)
        {
            List<KeyCode> keyCodes = new List<KeyCode>(0); // Declares and initializes a new empty list of KeyCodes with an initial capacity of zero.

            foreach (InputActions.ActionButton actionButton in keybinds.ActionButtons) // Looks for actionbutton string list within input actions scriptable and cycles through them (thats what foreach is for) ("in" refers to nested class?)
            {
                if(actionName == actionButton.ActionName) // If the KeyCode string name is equal to the actionbutton string name in scriptable...
				{
					foreach(string key in actionButton.positiveKeyCodes) // Initiates a "nested" loop that iterates over each string element in positiveKeyCodes list.
					{
						keyCodes.Add(StringToKey(key)); // Calls "StringToKey" method and adds the keycode string from scriptable and converts it into a Key.
					}
				} 
            }

            if(keyCodes.Count == 0)
			{
				Debug.LogError("Cannot find action button with name: " + actionName, instance); 
			}
			
			return keyCodes;
        }

        public float GetActionAxis(string actionAxisName)
		{
			List<KeyCode> positiveKeyCodes = new List<KeyCode>(0);
			List<KeyCode> negativeKeyCodes = new List<KeyCode>(0);

			foreach (InputActions.ActionButton actionButton in keybinds.ActionButtons)
			{
				if(actionAxisName == actionButton.ActionName)
				{
					foreach(string key in actionButton.positiveKeyCodes)
					{
						positiveKeyCodes.Add(StringToKey(key));
					}
					
					foreach(string key in actionButton.negativeKeyCodes)
					{
						negativeKeyCodes.Add(StringToKey(key));
					}
				} 
			}
			
			if(positiveKeyCodes.Count == 0 && negativeKeyCodes.Count == 0)
			{
				Debug.LogError("Cannot find action axis with name: " + actionAxisName, instance); 
			}
			else
			{
				if(positiveKeyCodes.Count == 0)
				{
					positiveKeyCodes.Add(KeyCode.None);
				}
				if(negativeKeyCodes.Count == 0)
				{
					negativeKeyCodes.Add(KeyCode.None);
				}
			}
			
			float axis = 0f;
			
			if(GetKeyList(positiveKeyCodes) && !GetKeyList(negativeKeyCodes)) axis = 1f;
			
			if(!GetKeyList(positiveKeyCodes) && GetKeyList(negativeKeyCodes)) axis = -1f;
			
			return axis;
		}
		
		// Returns true the frame the key is pressed.
		public bool GetActionDown(string action) 
		{
			List<KeyCode> keys = GetActionKeys(action);

			return GetKeyListDown(keys);
		}
		
		// Returns true the frame the key is released.
		public bool GetActionUp(string action) 
		{
			List<KeyCode> keys = GetActionKeys(action);

			return GetKeyListUp(keys);
		}
		
		// Returns true while the key is down.
		public bool GetAction(string action)
		{
			List<KeyCode> keys = GetActionKeys(action);
			
			return GetKeyList(keys);
		}
		
		public bool GetKeyListDown(List<KeyCode> keys)
		{
			bool pressed = false;
			
			foreach(KeyCode key in keys)
			{
				if(Input.GetKeyDown(key)) pressed = true;
			}
			
			return pressed;
		}
		
		public bool GetKeyListUp(List<KeyCode> keys)
		{
			bool pressed = false;
			
			foreach(KeyCode key in keys)
			{
				if(Input.GetKeyUp(key)) pressed = true;
			}
			
			return pressed;
		}
		
		public bool GetKeyList(List<KeyCode> keys)
		{
			bool pressed = false;
			
			foreach(KeyCode key in keys)
			{
				if(Input.GetKey(key)) pressed = true;
			}
			
			return pressed;
		}
        public KeyCode StringToKey(string keyString)
	    {
            // This method is responsible for converting the string names from "KeyBindings" scriptable into actual KeyCodes
            // Example, if the string word is "Space," it will return "KeyCode.Space"

		    return (KeyCode) System.Enum.Parse(typeof(KeyCode), keyString); // Converts string to enum value
	    }
    }
}
