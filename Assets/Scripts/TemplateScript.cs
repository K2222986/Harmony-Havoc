using UnityEngine;
using UnityEngine.InputSystem;

public class TemplateScript : MonoBehaviour
{
    private Vector2 gridSize;
    private Vector2 gridAdjustment;
    [SerializeField]
    private GameObject finalObject;

    private Vector2 mousePos;

    [SerializeField]
    private LayerMask allTilesLayer;

    private void Start()
    {
        gridSize.x = 130;
        gridSize.y = 20;
        gridAdjustment.x = 50;
        gridAdjustment.y = 10;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        if (Input.mousePosition.x > 635 && Input.mousePosition.x < 1285 && Input.mousePosition.y > 180)
        {
            transform.position = new Vector2(Mathf.Round((mousePos.x - gridAdjustment.x) / gridSize.x) * gridSize.x + gridAdjustment.x, 
                Mathf.Round((mousePos.y - gridAdjustment.y) / gridSize.y) * gridSize.y + gridAdjustment.y);
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseRay = transform.position;
                RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, allTilesLayer);

                if (rayHit.collider == null)
                {
                    GameObject newNote = Instantiate(finalObject, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                    SaveEditor.Instance.AddToList(newNote);
                }
            }
        }
    }
}
