using UnityEngine;

public class GoodCollider : MonoBehaviour
{
    public bool active = false;
    public string trigger = "Good";
    public GameObject note;
    public GameObject miss;
    GameObject noteMessage;
    GameObject messageContainer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        if (collision.gameObject.tag == "Note")
            note = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
        if (note.activeSelf)
        {
            messageContainer = GameObject.FindGameObjectWithTag("NoteMessages");
            if (messageContainer.transform.childCount > 0)
            {
                foreach (Transform message in messageContainer.transform)
                {
                    Destroy(message.gameObject);
                }
            }
            noteMessage = Instantiate(miss, new Vector3(510, 180), transform.rotation, GameObject.FindGameObjectWithTag("NoteMessages").transform);
            Destroy(noteMessage, 1f);
            Combo.Instance.streak = 0;
        }
    }
}
