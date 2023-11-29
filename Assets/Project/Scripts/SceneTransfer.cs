using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(Scenes.MENU);
    }
}
