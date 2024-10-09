using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;

    private Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        mousePosition = gameObject.transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos() + mousePosition;
    }
}
