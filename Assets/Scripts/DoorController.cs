using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Rigidbody2D doorRB;
    public float doorMoveSpeed = 3f;
    public float moveDistance = 5f;
    private bool isDoorOpen = false;
    private Vector2 iniDoorPosition;//The initial position of the door ,used for resetting
    public string wallLayerName = "groundLayer";
    void Start()
    {
        doorRB = GetComponent<Rigidbody2D>();
        //get the Rigidbody2D component from the door object,
        //and then use this component to control the physical movement of the door
        iniDoorPosition = transform.position;
        //Record the initial position of the door; it will return to this position when the door closes
        int doorLayer = gameObject.layer; 
        int wallLayer = LayerMask.NameToLayer(wallLayerName);

        // Makes the door ignore collisions with walls so they don't block each other
        Physics2D.IgnoreLayerCollision(doorLayer, wallLayer, true);
    }

    // Update is called once per frame
   
    public void OpenDoor()
    {
        if(!isDoorOpen)
        {
            isDoorOpen = true;
            Debug.Log("The door has received an open command! Current target location.：" + new Vector2(iniDoorPosition.x - moveDistance, iniDoorPosition.y));
        }
       
    }
    public void CloseDoor()
    {
        if (isDoorOpen)
        {
            isDoorOpen = false;
        }
    }
    void FixedUpdate()
    {
        Vector2 targetPosition;
        if ((isDoorOpen))
        {
            targetPosition = new Vector2(iniDoorPosition.x - moveDistance, iniDoorPosition.y);
            //Open position = Initial X - Moved distance, Y-axis remains unchanged
        }
        else
        {
            targetPosition = iniDoorPosition;
        }
       
        doorRB.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, doorMoveSpeed * Time.fixedDeltaTime));
        //Move the door gradually from its current position to the target position, with each movement consisting of a distance=doorMoveSpeed ​​* Time.fixedDeltaTime
    }
}
