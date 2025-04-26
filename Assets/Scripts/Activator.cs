using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    UnityEngine.UI.Image img;
    public KeyCode key;
    GameObject note;
    GameObject noteMessage;
    public GameObject perfect;
    public GameObject great;
    public GameObject good;
    public GameObject miss;
    public GameObject messageContainer;
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
        GoodCollider goodScript = gameObject.GetComponentInChildren<GoodCollider>();
        GreatCollider greatScript = gameObject.GetComponentInChildren<GreatCollider>();
        PerfectCollider perfectScript = gameObject.GetComponentInChildren<PerfectCollider>();
        if (Input.GetKeyDown(key))
        {
            StartCoroutine(Pressed());
            if (messageContainer.transform.childCount > 0)
            { 
                foreach (Transform message in messageContainer.transform)
                {
                    Destroy(message.gameObject);
                }
            }
        }
        if (Input.GetKeyDown(key) && goodScript.active)
        {
            if (perfectScript.active) 
            {
                perfectScript.note.SetActive(false);
                ScoreScript.instance.IncrementScore(150);
                noteMessage = Instantiate(perfect, new Vector3(510, 180), transform.rotation, GameObject.FindGameObjectWithTag("NoteMessages").transform);
            }
            else if (greatScript.active)
            {
                greatScript.note.SetActive(false);
                ScoreScript.instance.IncrementScore(100);
                noteMessage = Instantiate(great, new Vector3(510, 180), transform.rotation, GameObject.FindGameObjectWithTag("NoteMessages").transform);
            }
            else
            {
                goodScript.note.SetActive(false);
                ScoreScript.instance.IncrementScore(50);
                noteMessage = Instantiate(good, new Vector3(510, 180), transform.rotation, GameObject.FindGameObjectWithTag("NoteMessages").transform);
            }
            Destroy(noteMessage, 1f);
        }
        else if (Input.GetKeyDown(key))
        {
            noteMessage = Instantiate(miss, new Vector3(510, 180), transform.rotation, GameObject.FindGameObjectWithTag("NoteMessages").transform);
            Destroy(noteMessage, 100f);
        }
    }


    IEnumerator Pressed()
    {
        img.color = new Color(0.7f, 0.7f, 0.7f);
        yield return new WaitForSeconds(0.2f);
        img.color = old;
    }
}
