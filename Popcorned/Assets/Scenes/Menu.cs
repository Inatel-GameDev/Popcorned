using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
