using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinkPreview : MonoBehaviour
{
    public List<Transform> linkPreviewRenderer = new List<Transform>();

    public static LinkPreview instance;

    [HideInInspector] public Cells cell;

    [SerializeField] private GameObject _prefabPreviewLink;

    private void Awake()
    {
        instance = this;
    }
    public void LinkPreviewManager(List<Transform> linkRenderer)
    {
        if (cell._findFarCollider.Count <= 1 || cell._findCloseCollider.Count > 0)
            return;

        for (int i = 0; i < cell._findFarCollider.Count; i++)
        {
            if (i >= cell.maxLink)
                break;

            Transform node1 = linkRenderer[i].GetComponent<LinkCellsManager>().node1;
            Transform node2 = linkRenderer[i].GetComponent<LinkCellsManager>().node2;

            float dist = Vector2.Distance(node1.position, node2.position) - linkRenderer[i].localScale.x;
            linkRenderer[i].localScale += new Vector3(dist, 0, 0);

            Vector2 newPos = (node1.position - node2.position) / 2 + node2.position;
            linkRenderer[i].position = newPos;

            Vector3 dir = node1.position - linkRenderer[i].position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            linkRenderer[i].rotation = Quaternion.AngleAxis(angle, new Vector3(0, linkRenderer[i].rotation.y, 1));
        }
    }
    private void LinkPreviewRenderer()
    {
        if (cell._findFarCollider.Count <= 1 || cell._findCloseCollider.Count > 0)
            return;

        for (int i = 0; i < cell._findFarCollider.Count; i++)
        {
            GameObject linkRenderer = Instantiate(_prefabPreviewLink);
            linkPreviewRenderer.Add(linkRenderer.transform);
            LinkCellsManager linkCellsManager = linkRenderer.GetComponent<LinkCellsManager>();
            linkCellsManager.node1 = cell.transform;
            linkCellsManager.node2 = cell._findFarCollider[i].transform;
        }
    }

    public void PreviewUpdate(Cells cellPreview)
    {
        cell = cellPreview;
        DeletePreviews();
        LinkPreviewRenderer();
        LinkPreviewManager(linkPreviewRenderer);
    }

    public void DeletePreviews()
    {
        foreach (Transform linePreview in linkPreviewRenderer.ToList())
        {
            linkPreviewRenderer.Remove(linePreview);
            Destroy(linePreview.gameObject);
        }
    }
}
