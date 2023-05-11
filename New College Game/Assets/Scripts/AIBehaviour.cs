using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBehaviour : MonoBehaviour
{

    public static void DumpToConsole(object obj)
    {
        var output = JsonUtility.ToJson(obj, true);
        Debug.Log(output);
    }

    bool isPlayerReady = false; // Automatically is set, just adds potential for me to add a main-menu in the future.
    bool isAiReady = false; // Gets set when the AI has calculated everything it needs to calculate (player distance, fight strategy, etc).

    public GameObject PlayerOne; // Defines in the script there is a gameobject & the variable to call for it is {PlayerOne}

    Rigidbody playerTwo; // Defines in the script there is a rigidbody & the variable to call for it is {playerTwo}
    Rigidbody playerOne; // Defines in the script there is a rigidbody & the variable to call for it is {playerOne}

    void Start()
    {   
        PlayerOne = GameObject.Find("PlayerOne"); // Finds the player object.
        playerOne = PlayerOne.GetComponent<Rigidbody>(); // Gets the rigidbody component of the player object.
        playerTwo = GetComponent<Rigidbody>(); // Gets the rigidbody component of the AI object.
        
        // playerOne can now be used as a variable to call for the player's rigidbody component.
        // playerTwo can now be used as a variable to call for the AI's rigidbody component.

        isPlayerReady = true; // Sets the player to ready.
        isAiReady = true; // Sets the AI to ready.
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerReady && isAiReady) { // If both the player and the AI are ready to move, then the AI will move.
            // AI is ready to move.
            // AI is ready to fight.
            // AI is ready to jump.
            // AI is ready to do everything.
        }
    }
}
