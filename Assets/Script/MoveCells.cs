using UnityEngine;

public class MoveCells : MonoBehaviour
{
    private Camera _camera;

    private Rigidbody2D _cellRigidbody;

    private float _spawnSpeed = 4f;

    private DragAndDrop _dragAndDrop;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _cellRigidbody = GetComponent<Rigidbody2D>();
        int _direction = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 _force = new Vector2(_direction * _spawnSpeed, 0);
        _cellRigidbody.AddForce(_force, ForceMode2D.Impulse);
        _dragAndDrop = GetComponent<DragAndDrop>();

        RandomizePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.layer == 7))
            return;

        Vector3 _screenPos = _camera.WorldToScreenPoint(transform.position);

        float _rightSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float _leftSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(0f,0f)).x;
        float _upSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        float _downSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(0f, 0f)).y;

       if(!_dragAndDrop._isDrag)
        { 
            if (_screenPos.x <= 0 && _cellRigidbody.velocity.x < 0)
            {
                transform.position = new Vector2(_rightSideOfTheScreen, Random.Range(_downSideOfTheScreen, _upSideOfTheScreen));
            }
            else if (_screenPos.x >= Screen.width && _cellRigidbody.velocity.x > 0)
            {
                transform.position = new Vector2(_leftSideOfTheScreen, Random.Range(_downSideOfTheScreen, _upSideOfTheScreen));
            }

            if (_screenPos.y < 0)
            {
                transform.position = new Vector2(Random.Range(_leftSideOfTheScreen, _rightSideOfTheScreen), _upSideOfTheScreen);
            }
            else if (_screenPos.y > Screen.height)
            {
                transform.position = new Vector2(Random.Range(_leftSideOfTheScreen, _rightSideOfTheScreen), _downSideOfTheScreen);
            }
        }
    }

    void RandomizePosition()
    {
        float _rightSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float _leftSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(0f, 0f)).x;
        float _upSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        float _downSideOfTheScreen = _camera.ScreenToWorldPoint(new Vector2(0f, 0f)).y;
        transform.position = new Vector2(Random.Range(_leftSideOfTheScreen, _rightSideOfTheScreen), Random.Range(_downSideOfTheScreen, _upSideOfTheScreen));
    }
}
