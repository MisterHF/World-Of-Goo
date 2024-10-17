using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsManager : MonoBehaviour
{
    public static CellsManager Instance;

    private CellsList _cellsList;

    [SerializeField] private GameObject _cellPrefab;

    public int CellCount = 0;
    public int cellVisibleMax = 10;

    private void Awake()
    {
        Instance = this;
        _cellsList = GetComponent<CellsList>();
    }

    private void Start()
    {
        for (int i = 0; i < CellCount; i++)
        {
            GameObject newCell = Instantiate(_cellPrefab);
            _cellsList.AddCell(newCell);
        }
    }
}
