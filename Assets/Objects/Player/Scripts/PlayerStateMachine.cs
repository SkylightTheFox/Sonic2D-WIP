using System.Collections.Generic;
using SonicFramework;
using System;

namespace StateMachine
{
    /// <summary>
    /// This script will process each state one at a time. (Also known as Finite State Machine (FSM))
    /// </summary>
    public class PlayerStateMachine
    {
        private readonly Player player;
        private readonly Dictionary<Type, PlayerBaseState> states = new Dictionary<Type, PlayerBaseState>(); // Be sure to add "using System;" to create "Type" keyword
                                                                                                        // This Dictionary<Type> will store values and acts as a sort of index and new instances will be created
        private Type currentState;
        public int stateId => states[currentState].animationID; // Each state will have its own ID to it, that will be identified in "PlayerBaseState"

        public PlayerStateMachine(Player player)
        {
            this.player = player; // curent instance of this script is equal to player game object (I think it means this)
        }

        public void AddState(PlayerBaseState state)
        {
            var type = state.GetType();

            if (!states.ContainsKey(type))
            {
                states.Add(type, state);
            }
        }

        public void ChangeState<T>() where T : PlayerBaseState
        {
            var type = typeof(T);

            if (states.ContainsKey(type))
            {
                if (currentState != null)
                {
                    states[currentState].Exit(player);
                }

                currentState = type;
                states[currentState].Enter(player);
            }
        }

        public void UpdateState(float deltaTime)
        {
            if (currentState != null)
            {
                states[currentState].Step(player, deltaTime);
            }
        }
    }
}