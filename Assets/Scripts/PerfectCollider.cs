using UnityEngine;

public class PerfectCollider : MonoBehaviour
{
    public bool active = false;
    public string trigger = "Perfect";
    public GameObject note;


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
}
