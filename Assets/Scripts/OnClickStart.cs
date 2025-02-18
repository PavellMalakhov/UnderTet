using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickStart : MonoBehaviour
{
    private string _mainScene = "MainGame";

    public void StartGame()
    {
        SceneManager.LoadScene(_mainScene);
    }
}
