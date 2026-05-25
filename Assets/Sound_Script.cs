using UnityEngine;

public class Sound_Script : MonoBehaviour
{
    public AudioSource Soundsource;
    public AudioClip bg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Soundsource.clip = bg;
        Soundsource.Play();
    }

}
