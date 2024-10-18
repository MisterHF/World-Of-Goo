using System;
using System.Collections.Generic;
using UnityEngine;

public class CellsList : MonoBehaviour
{
    public static CellsList Instance;

    private CellsManager _cellsManager;

    public List<GameObject> cellsUsed = new();
    public List<GameObject> cellsNotUsed = new();

    public List<GameObject> cellsPool = new();

    public Action<GameObject> OnCellLinkCreatedAction;

    private void Awake()
    {
        Instance = this;
        _cellsManager = GetComponent<CellsManager>();
    }

    private void Start()
    {
        OnCellLinkCreatedAction += GameManager.instance.CheckCellsUsed;
    }

    public void AddCell(GameObject cell)
    {
        if (cellsNotUsed.Count < _cellsManager.cellVisibleMax)
        {
            cellsNotUsed.Add(cell);
        }
        else
        {
            cellsPool.Add(cell);
            cell.SetActive(false);
        }
        cell.GetComponent<Cells>().OnLinkCreated += OnCellLinkCreated;
    }

    private void OnCellLinkCreated(GameObject cell)
    {
        cellsNotUsed.Remove(cell);
        cellsUsed.Add(cell);

        if (cellsNotUsed.Count < _cellsManager.cellVisibleMax && cellsPool.Count > 0)
        {
            cellsNotUsed.Add(cellsPool[0]);
            cellsPool[0].SetActive(true);
            cellsPool.RemoveAt(0);
        }

        OnCellLinkCreatedAction?.Invoke(cell);
    }
}
