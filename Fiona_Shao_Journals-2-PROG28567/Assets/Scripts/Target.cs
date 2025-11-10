using UnityEngine;

public class Target : MonoBehaviour
{
    public ScoreboardController scoreController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CannonballController1 cannonball = collision.gameObject.GetComponent<CannonballController1>();
        if (cannonball != null)
        {
            scoreController.Score += 100;
        }

    }
}
