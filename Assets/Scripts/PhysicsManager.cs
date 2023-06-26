using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicFramework;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager instance; // Singleton, meaning you don't need to reference the exact PhysicsManager object in the scene
    public int substeps; // repeats physics in a "for" loop. Moves things at a fraction of the distance (stepDelta variable) (Should be a value of 1 within editor)
    public Player[] players; // Holds a list of how many players are in the game (Also an Array or a Collection)

    void OnEnable()
    {
        // Creates instance of physics manager upon load
        if (instance == null)
		{
			instance = this;
		}
		else
		{
			if (instance != this)
			Destroy(this);
		}
    }

    void Start()
    {
        players = FindObjectsOfType<Player>(); // Searches for objects with Player script component and adds it to array
    }

    void FixedUpdate()
    {
        // This method will run continually
        float substepDelta = Time.deltaTime * 60f / substeps;  // Calculates the time interval for each substep in the game loop
        for (int i = 0; i < substeps; i ++) // Starts a loop that will execute a specific number of substeps. "i" stands for interation
		{
			foreach (Player player in players) // loop iterates over each Player object in "players" collection
			{
				player.Player_Update(substepDelta);
				player.Player_Late_Update();
			}
		}
    }
}
