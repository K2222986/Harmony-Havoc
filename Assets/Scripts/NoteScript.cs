using UnityEngine;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class NoteScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float noteSpeed;
    private float previousTime;

    private void Awake()
    {
        Debug.Log("run");
        previousTime = VolumeManager.Instance.musicSource.time;
    }
    public void Update()
    {
        if (SceneManager.GetSceneByName("Game").isLoaded)
        {
            rb.linearVelocity = new Vector2(0, -noteSpeed);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (VolumeManager.Instance.musicSource.time - previousTime) * 500);
            previousTime = VolumeManager.Instance.musicSource.time;
        }
    }
}
