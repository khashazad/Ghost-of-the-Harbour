using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    void Start()
    {
        GameObject buttonObject = GameObject.FindGameObjectWithTag("RestartButton");
        if (buttonObject != null)
        {
            Button b = buttonObject.GetComponent<Button>();
            if (b != null)
            {
                b.onClick.AddListener(() => StartGame("Main"));
            }
        }
        else
        {
            Debug.LogError("StartButton tag not found in the scene.");
        }
    }

    public void StartGame(string level)
    {
        SceneManager.LoadScene(level);
    }
}
