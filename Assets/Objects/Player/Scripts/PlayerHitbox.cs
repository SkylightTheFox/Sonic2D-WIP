using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways] // Allows code to run within editor as well
public class PlayerHitbox : MonoBehaviour
{
    // HitBox Transform
    Vector2 position;
    Vector2 size;


    void Editor()
    {   
#if UNITY_EDITOR
        // This code will grab the gameObject (boxCollider) and create a visible Rect around it while within Unity Editor
        if (Selection.Contains (this.gameObject))
        {
            // Include code here that will allow collider to be visible
        }
#endif
    }
}
