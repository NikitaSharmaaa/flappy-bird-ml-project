using UnityEngine;

public class Pipemove : MonoBehaviour
{
    public float movespeed = 5f;
    public float deadpoint = 20f; // Distance after which the pipe should be destroyed
    private float startXPosition;

    void Start()
    {
        startXPosition = transform.position.x;
        Debug.Log("Pipe spawned at X: " + startXPosition);
    }

    void Update()
    {
        transform.position = transform.position + (Vector3.left * movespeed) * Time.deltaTime;
    }
}
