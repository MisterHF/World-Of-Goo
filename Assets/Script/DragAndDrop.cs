using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    [HideInInspector] public bool _isPosed = false;

    [HideInInspector] public Cells Cells;

    [HideInInspector] public bool _isDrag = false;
    private Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (!_isPosed)
        {
            _isDrag = true;
            mousePosition = gameObject.transform.position - GetMousePos();
            GetComponent<Collider2D>().enabled = false;
            //GetComponent<SpriteRenderer>().color = Color.yellow;
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
            _isDrag = false;
            Cells.CreateLink(gameObject);
            GetComponent<Collider2D>().enabled = true;
            LinkPreview.instance.DeletePreviews();


        }
    }
    private void Update()
    {
        if (_isDrag) 
        {
            LinkPreview.instance.PreviewUpdate(Cells);
        }
    }
}
