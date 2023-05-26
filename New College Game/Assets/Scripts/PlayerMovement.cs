// using System.Collections; // Commented because not needed.
// using System.Collections.Generic; // Commented because not needed.
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10f; // Controls velocity multiplier.
    public float playerHealth = 100f; // Controls player health.
    Rigidbody rb; // Defines in the script there is a rigidbody & the variable to call for it is {rb}

    // Jump Merge
    public Vector3 boxSize; // Defines the size of the box that will be drawn in the scene view.
    public float maxDistance; // Defines the maximum distance the player can be from the ground before they are considered to be in the air.
    public LayerMask layerMask; // Defines the layermask that will be used to determine what is considered ground.
    public float jumpforce = 6f; // Defines the force of the jump.
    public float AirAcceleration = 3f; // Defines the acceleration of the player in the air.
	public float AirMaxSpeed = 3f;  // Defines the maximum speed of the player in the air.

    
    // Start is called before the first frame update (assuming this is when the game first starts up)
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Calls for the rigidbody component.
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = 0; // w key changes value to 1, s key changes value to -1

        // Ground movement.
        if (xMove != 0 && GroundCheck()) {
            rb.velocity = new Vector3(xMove * playerSpeed, rb.velocity.y, zMove);
            // rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * playerSpeed; // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 
        }
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck()) {
            rb.velocity = Vector3.up * jumpforce;
        }

        // Airborne movement
        if (xMove != 0 && !GroundCheck()) {
            // Movement direcrtion based on current input.
			int dir = Mathf.Clamp(Mathf.RoundToInt(xMove), -1, 1); // rounds the value of xMove to the nearest integer, then clamps it between -1 and 1
			float xpeed = Mathf.Clamp(rb.velocity.x + AirMaxSpeed * dir * Time.fixedDeltaTime * AirAcceleration, -AirMaxSpeed, AirMaxSpeed); // adds acceleration to the player's velocity, then clamps it between -AirMaxSpeed and AirMaxSpeed
			float downForce = rb.velocity.y>0? 0 : 0; // adds a small downwards force when going down 

            rb.velocity = new Vector3(xpeed, rb.velocity.y - downForce, 0); // sets the player's velocity to the new velocity
        }
     }

    void OnDrawGizmos() { // Draws a box in the scene view to show the ground check.
        Gizmos.color=Color.red; // Sets the color of the box to red.
        Gizmos.DrawCube(transform.position-transform.up*maxDistance, boxSize); // Draws the box.
    }

    bool GroundCheck() { // Checks if the player is on the ground.
        // bool grounded = Physics.BoxCast(transform.position,boxSize,-transform.up,transform.rotation,maxDistance,layerMask);
        return Physics.BoxCast(transform.position,boxSize,-transform.up,transform.rotation,maxDistance,layerMask); // Returns true if the player is on the ground, false if they are not.
    }
}
