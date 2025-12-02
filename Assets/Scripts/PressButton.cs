using UnityEngine;

public class PressButton : MonoBehaviour
{
    public DoorController door;
    private Vector2 iniButtonPosition;
    public float pressDownDistance = 1f;
    
    private bool isButtonPressed = false;

    void Start()
    {
        iniButtonPosition = transform.position;
         GetComponent<SpriteRenderer>().color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D triggeringObject)
    {
        if (triggeringObject.gameObject.CompareTag("WoodenBox") && !isButtonPressed)//Determine if it's a wooden crate + the button hasn't been pressed yet.
        {
            door.OpenDoor();
            GetComponent<SpriteRenderer>().color = Color.green;
            transform.position = new Vector2(iniButtonPosition.x, iniButtonPosition.y-pressDownDistance);
            //move the button down

            isButtonPressed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D triggeringObject)
    {
        if (triggeringObject.gameObject.CompareTag("WoodenBox") && isButtonPressed)//Determine if the wooden crate + button has been pressed.
        {
            door.CloseDoor();
            GetComponent<SpriteRenderer>().color = Color.red;//restore red color
            transform.position = iniButtonPosition;
            //restore button position
            isButtonPressed = false;
        }
    }

}
