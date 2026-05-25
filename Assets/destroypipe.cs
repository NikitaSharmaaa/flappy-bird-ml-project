using UnityEngine;

public class destroypipe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Hittable")
        {
            Destroy(other.transform.root.gameObject); // destroys the entire pipe prefab
        }
    }
}
