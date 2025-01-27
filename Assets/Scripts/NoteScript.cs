using UnityEngine;
using System.Collections;

public class NoteScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float noteSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.linearVelocity = new Vector2(0, -noteSpeed);
    }
}
