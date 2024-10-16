using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit ?");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Anchor"))
        {
            Debug.Log("Exit scene");
            SceneManager.LoadScene("Lvl1");
        }
    }


}
