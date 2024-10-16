using UnityEngine;

public class MoveCells : MonoBehaviour
{
    private Camera _camera;

    private Rigidbody2D _cellRigidbody;

    private float _spawnSpeed = 4f;

    

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _cellRigidbody = GetComponent<Rigidbody2D>();
        int _direction = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 _force = new Vector2(_direction * _spawnSpeed, 0);
        _cellRigidbody.AddForce(_force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.layer == 7))
            return;

        Vector3 _screenPos = _camera.WorldToScreenPoint(transform.position);

        float _rightSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float _leftSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(0f,0f)).x;

        if (_screenPos.x <= 0 && _cellRigidbody.velocity.x < 0)
        {
            transform.position = new Vector2(_rightSideOfTheScreen, transform.position.y);
        }
        else if (_screenPos.x >= Screen.width && _cellRigidbody.velocity.x > 0) 
        {
            transform.position = new Vector2(_leftSideOfTheScreen, transform.position.y);
        }
    }
}
