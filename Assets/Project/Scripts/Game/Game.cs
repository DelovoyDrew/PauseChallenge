using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField] private Mistakes _mistakes;

    [SerializeField] private GamePanelUI _loosePanel;
    [SerializeField] private GamePanelUI _winPanel;
    [SerializeField] private GamePanelUI _gamePanel;

    [SerializeField] private float _looseDelay;
    [SerializeField] private float _winDelay;

    private void Start()
    {
        RestartGame();
        VideoStopper.Instance.OnPause += CheckGame;
    }

    public void RestartGame()
    {
        YandexGame.FullscreenShow();
        _mistakes.ResetMistakes();

        _loosePanel.gameObject.SetActive(false);
        _winPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);

        VideoStopper.Instance.RestartVideo();
    }

    public void ReturnToMenu()
    {
        YandexGame.FullscreenShow();
        SceneManager.LoadScene(Scenes.MENU);
    }

    private void CheckGame(float percent)
    {
        bool isMistake = percent != 100;

        if (isMistake)
        {
            if(_mistakes.IsLooseGame())
            {
                StartCoroutine(LooseRoutine());
            }
        }
        else
        {
            StartCoroutine(WinRoutine());   
        }
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(_winDelay);
        _winPanel.gameObject.SetActive(true);
        _gamePanel.gameObject.SetActive(false);
        VideoStopper.Instance.PauseVideo();
    }

    private IEnumerator LooseRoutine()
    {
        yield return new WaitForSeconds(_looseDelay);
        _loosePanel.gameObject.SetActive(true);
        _gamePanel.gameObject.SetActive(false);
        VideoStopper.Instance.PauseVideo();
    }
}
