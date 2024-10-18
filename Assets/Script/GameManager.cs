using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CheckCellsUsed(GameObject cell)
    {
        if (CellsList.Instance.cellsUsed.Count == CellsManager.Instance.CellCount)
        {
            //end game
        }
    }

    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }
}
