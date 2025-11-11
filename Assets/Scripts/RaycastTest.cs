using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public float rayMaxDistance = 50f; 
    public LayerMask enemyLayer; 
    // Update is called once per frame
    void Update()
    {
        Vector2 startPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - startPosition;

        //bool hitSomething = Physics2D.Raycast(startPosition, directionToFire);

        RaycastHit2D hit = Physics2D.Raycast(startPosition, startPosition + direction.normalized * rayMaxDistance, rayMaxDistance, enemyLayer);
        Color drawColour;
        if (hit)
        {
            drawColour = Color.red;
        }
        else
        {
            drawColour = Color.green;
        }
        
        Debug.DrawLine(startPosition, mousePosition, drawColour);

        if (Input.GetMouseButtonDown(0) && hit)
        {
            
            Debug.Log("Hit the enemy£º" + hit.collider.gameObject.name);
           
        }

    }
}
