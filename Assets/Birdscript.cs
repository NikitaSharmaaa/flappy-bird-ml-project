using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Birdscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myRigidbody2D;
    public float flapspeed;
    public LogicScript LogicScript;
    public bool isBirdAlive = true;
    Sound_Manager soundManager;
    public Go_Between_Pipe Go_Between_Pipe;
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Soundfx").GetComponent<Sound_Manager>();
        LogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        if (Go_Between_Pipe == null)
        {
            Go_Between_Pipe = GetComponent<Go_Between_Pipe>();
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            jump();
        }

        else if (Input.GetKeyDown(KeyCode.Escape)){
            LogicScript.pause();
        }

        if ((myRigidbody2D.transform.localPosition.y > 4.4f || myRigidbody2D.transform.localPosition.y < -5f) && isBirdAlive == true)
        {
            Debug.Log("Bird Died Out Of Screen Check");
            Go_Between_Pipe.death();
        }

    }
    public void jump()
    {
        if (isBirdAlive)
        {
            myRigidbody2D.linearVelocity = Vector2.up * flapspeed;
            //soundManager.PlaySFX(soundManager.flap);
        }
        else
        {
            Debug.Log("Bird Not Alive");
        }
    }
    public bool getbird()
    {
        return isBirdAlive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBirdAlive == true && collision.gameObject.layer == LayerMask.NameToLayer("Hittable"))
        {
            //soundManager.PlaySFX(soundManager.death);
            Debug.Log("Bird Died At Birdscript collison()");
            Go_Between_Pipe.death();
        }
    }
}
