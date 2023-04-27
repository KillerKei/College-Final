// using System.Collections; // Commented because not needed.
// using System.Collections.Generic; // Commented because not needed.
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10f; // Controls velocity multiplier.
    Rigidbody rb; // Defines in the script there is a rigidbody & the variable to call for it is {rb}

    // Jump Merge
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public float jumpforce = 6f;
    
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

        print(xMove);
        if (xMove != 0 && GroundCheck()) {
            rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * playerSpeed; // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 
        }

        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck()) {
            rb.AddForce(transform.up*jumpforce,ForceMode.Impulse);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color=Color.red;
        Gizmos.DrawCube(transform.position-transform.up*maxDistance,boxSize);
    }

    bool GroundCheck() {
        return Physics.BoxCast(transform.position,boxSize,-transform.up,transform.rotation,maxDistance,layerMask);
    }
}
