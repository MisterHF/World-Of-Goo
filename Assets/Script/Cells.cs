using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cells : MonoBehaviour
{
    private Transform _transform;

    [SerializeField] private float _minDistPosed = 1.0f;
    [SerializeField] private float _maxDistPosed = 2f;

    public int maxLink = 2;
    [SerializeField] private float _frequency = 6f;



    [HideInInspector] public List<Collider2D> _findFarCollider = new List<Collider2D>();
    [HideInInspector] public List<Collider2D> _findCloseCollider = new List<Collider2D>();

    [SerializeField] private GameObject _prefabLink;

    public Action<GameObject> OnLinkCreated;

    void Start()
    {
        _transform = transform;
    }

    
    void Update()
    {
       _findFarCollider = Physics2D.OverlapCircleAll(_transform.position, _maxDistPosed, 1 << LayerMask.NameToLayer("Anchor")).ToList();
       _findCloseCollider = Physics2D.OverlapCircleAll(_transform.position, _minDistPosed, 1 << LayerMask.NameToLayer("Anchor")).ToList();
    }

    public void CreateLink(GameObject Cell)
    {
        SortList(_findFarCollider);
        if (_findFarCollider.Count <= 1 || _findCloseCollider.Count > 0)
            return; 
        for (int i = 0; i < _findFarCollider.Count; i++)
        {
            
            if (i >= maxLink)
                break;                                                                          // break = permet de sortir de la boucle

            SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();
            joint.connectedBody = _findFarCollider[i].GetComponent<Rigidbody2D>();
            joint.autoConfigureDistance = false;
            joint.frequency = _frequency;
            joint.dampingRatio = 1;

            GetComponent<Rigidbody2D>().gravityScale = 1f;

            GameObject linkRenderer = Instantiate(_prefabLink);
            LinkTransformManager.instance.linkRenderer.Add(linkRenderer.transform);
            LinkCellsManager linkCellsManager = linkRenderer.GetComponent<LinkCellsManager>();
            linkCellsManager.node1 = Cell.transform;
            linkCellsManager.node2 = _findFarCollider[i].transform;
        }
        Cell.layer = 7;
        Cell.GetComponent<DragAndDrop>()._isPosed = true;
        gameObject.tag = "Anchor";
                                                                                                //Cell.GetComponent<CircleCollider2D>().excludeLayers -= 1 << LayerMask.NameToLayer("Wall");
        Cell.GetComponent<CircleCollider2D>().excludeLayers -= 256;                             //256 = valeur binaire de la position du layer 8 "Wall"
        OnLinkCreated?.Invoke(gameObject);
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
