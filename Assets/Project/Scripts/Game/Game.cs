using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Mistakes _mistakes;

    [SerializeField] private GamePanelUI _loosePanel;
    [SerializeField] private GamePanelUI _winPanel;
    [SerializeField] private GamePanelUI _gamePanel;

    private void Start()
    {
        RestartGame();
        VideoStopper.Instance.OnPause += CheckGame;
    }

    public void RestartGame()
    {
        _mistakes.ResetMistakes();

        _loosePanel.gameObject.SetActive(false);
        _winPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);

        VideoStopper.Instance.RestartVideo();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(Scenes.MENU);
    }

    private void CheckGame(float percent)
    {
        bool isMistake = percent != 100;

        if (isMistake)
        {
            if(_mistakes.IsLooseGame())
            {
                _loosePanel.gameObject.SetActive(true);
                _gamePanel.gameObject.SetActive(false);
            }
        }
        else
        {
            _winPanel.gameObject.SetActive(true);
            _gamePanel.gameObject.SetActive(false);
        }
    }
}
