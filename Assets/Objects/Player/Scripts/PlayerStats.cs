using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Ground")]
    public float topGroundSpeed;
    public float groundAcceleration;
    public float groundDeceleration;
    public float groundFriction;
    public float slopeFactor;

    [Header("Roll")]
    public float rollFriction;
    public float rollDeceleration;

    [Header("Air")]
    public float topYSpeed;
    public float airAcceleration;
    public float airDrag;
    public float gravityForce;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCut;
}
