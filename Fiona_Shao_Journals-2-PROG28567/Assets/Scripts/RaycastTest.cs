using UnityEngine;

public class RaycastTest : MonoBehaviour
{

    public float rayMaxDistance = 50f; 
    public LayerMask enemyLayer; 
    public Color rayColorNormal = Color.red;
    public Color rayColorHit = Color.green; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 startPosition = Vector2.zero;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - startPosition;

        //bool hitSomething = Physics2D.Raycast(startPosition, directionToFire);

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, rayMaxDistance, enemyLayer);
        Color drawColour = Color.red;
        if (hit)
        {
            drawColour = Color.green;
        }
        
        Debug.DrawLine(startPosition, direction, drawColour);

        if (Input.GetMouseButtonDown(0) && hit)
        {
            
            Debug.Log("Hit enemy£º" + hit.collider.gameObject.name);
            Debug.Log("Hit the enemy£¡");
        }

    }
}
