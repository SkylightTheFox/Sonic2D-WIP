using UnityEngine;
using SonicFramework;

namespace StateMachine
{
    /// <summary>
    /// STILL NEED TO UNDERSTAND WHAT BASE STATE IS FOR
    /// The actual states need those 3 voids to be override voids (Enter, Step, Exit) (Exit is sometimes not included)
    /// </summary>
    public abstract class PlayerBaseState : MonoBehaviour
    {
        public int animationID;

        public virtual void Enter(Player player) { }
        public virtual void Step(Player player, float deltaTime) { }
        public virtual void Exit(Player player) { }
    }
}