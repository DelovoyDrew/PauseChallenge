using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void StartLvl(int lvl)
    {
        GlobalSaver.Instance.StartGame(lvl);
        SceneManager.LoadScene(Scenes.GAME);
    }
}
