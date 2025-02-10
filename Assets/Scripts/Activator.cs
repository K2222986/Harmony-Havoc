using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    UnityEngine.UI.Image img;
    public KeyCode key;
    bool active = false;
    GameObject note;
    Color old;

    private void Awake()
    {
        {
            img = GetComponent<UnityEngine.UI.Image>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        old = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            StartCoroutine(Pressed());
            Debug.Log("Pressed");
        }
        if (Input.GetKeyDown(key) && active)
        {
            note.SetActive(false);
            ScoreScript.instance.IncrementScore(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        if (collision.gameObject.tag == "Note")
            note = collision.gameObject;
        Debug.Log("collided");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
        Debug.Log("left collision");
    }

    IEnumerator Pressed()
    {
        img.color = new Color(0.7f, 0.7f, 0.7f);
        yield return new WaitForSeconds(0.2f);
        img.color = old;
    }
}
