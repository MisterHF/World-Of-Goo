using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    public bool _isPosed = false;

    public Cells Cells;


    private Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (!_isPosed)
        {
            mousePosition = gameObject.transform.position - GetMousePos();
            GetComponent<Collider2D>().enabled = false;
        }
  
    }

    private void OnMouseDrag()
    {
        if (!_isPosed)
        {
            transform.position = GetMousePos() + mousePosition;
        }
        
    }

    private void OnMouseUp()
    {
        if (!_isPosed) 
        { 
            Cells.CreateLink(gameObject);
            GetComponent<Collider2D>().enabled = true;
            gameObject.tag = "Anchor";
        }
    }
}
