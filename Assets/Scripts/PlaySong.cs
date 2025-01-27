using UnityEngine;

public class PlaySong : MonoBehaviour
{
    public float delay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
