using System.Collections.Generic;
using UnityEngine;


public class LinkTransformManager : MonoBehaviour
{
    public List<Transform> linkRenderer = new List<Transform>();

    public static LinkTransformManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void LinkManager(List<Transform> linkRenderer)
    {
        for (int i = 0; i < linkRenderer.Count; i++)
        {
            Transform node1 = linkRenderer[i].GetComponent<LinkCellsManager>().node1;
            Transform node2 = linkRenderer[i].GetComponent<LinkCellsManager>().node2;

            float dist = Vector2.Distance(node1.position, node2.position) - linkRenderer[i].localScale.x;
            linkRenderer[i].localScale += new Vector3(dist, 0, 0);

            Vector2 newPos = (node1.position - node2.position) / 2 + node2.position;
            linkRenderer[i].position = newPos;

            Vector3 dir = node1.position - linkRenderer[i].position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            linkRenderer[i].rotation = Quaternion.AngleAxis(angle, new Vector3(0, linkRenderer[i].rotation.y,1));
        }
    }
    // Update is called once per frame
    void Update()
    {
        LinkManager(linkRenderer);
    }
}
