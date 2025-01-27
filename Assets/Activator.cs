using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note;
    Color old;

    private void Awake()
    {
        {
            sr = GetComponent<SpriteRenderer>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        old = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            StartCoroutine(Pressed());
        }
        if (Input.GetKeyDown(key) && active)
        {
            Destroy(note);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        if (collision.gameObject.tag == "Note")
            note = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0.7f, 0.7f, 0.7f);
        yield return new WaitForSeconds(0.2f);
        sr.color = old;
    }
}
