using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitializer : MonoBehaviour
{
    public void InitializeGameLevel()
    {
        SceneManager.LoadScene("Secondary");
    }
}
