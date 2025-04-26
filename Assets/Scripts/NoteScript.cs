using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NoteScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float noteSpeed;
    private float previousTime;
    public int noteID;

    private void Awake()
    {
        previousTime = VolumeManager.Instance.musicSource.time;
    }
    public void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (VolumeManager.Instance.musicSource.time - previousTime) * 500);
        previousTime = VolumeManager.Instance.musicSource.time;
    }
}
