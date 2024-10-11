using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cells : MonoBehaviour
{
    private Transform _transform;

    [SerializeField] private float _minDistPosed = 1.0f;
    [SerializeField] private float _maxDistPosed = 10f;

    [SerializeField] private int _maxLink = 3;
    [SerializeField] private float _frequency = 100f;
    

    private List<Collider2D> _findCollider = new List<Collider2D>();

    void Start()
    {
        _transform = transform;
        
    }

    
    void Update()
    {
       _findCollider = Physics2D.OverlapCircleAll(_transform.position, _maxDistPosed, 1 << LayerMask.NameToLayer("Anchor")).ToList();           
    }

    public void CreateLink()
    {
        SortList(_findCollider);
        for(int i = 0; i < _findCollider.Count; i++)
        {
            if (i >= _maxLink)
                break;                                                                          // break = permet de sortir de la boucle

            SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();
            joint.connectedBody = _findCollider[i].GetComponent<Rigidbody2D>();
            joint.autoConfigureDistance = false;
            joint.frequency = _frequency;
        }

    }

    private void SortList(List<Collider2D> list)
    {
        for (int i = 0; i < list.Count; i++)                                 // Lorsque i = 0 la deuxième boucle for parcours la liste j jusqu'au bout 
        {                                                                    // Une fois j verifier i = 1 
            for(int j = 0; j < list.Count; j++)
            {
                if(Vector2.Distance(transform.position, list[j].transform.position) > Vector2.Distance(transform.position, list[j++].transform.position))
                {
                    (list[j++], list[j]) = (list[j], list[j++]);
                }
            }
        }
    }
}
