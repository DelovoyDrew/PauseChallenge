using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Mistakes _mistakes;

    [SerializeField] private GamePanelUI _loosePanel;
    [SerializeField] private GamePanelUI _winPanel;
    [SerializeField] private GamePanelUI _gamePanel;

    [SerializeField] private float _looseDelay;

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
                StartCoroutine(LooseRoutine());
            }
        }
        else
        {
            _winPanel.gameObject.SetActive(true);
            _gamePanel.gameObject.SetActive(false);
        }
    }

    private IEnumerator LooseRoutine()
    {
        yield return new WaitForSeconds(_looseDelay);
        _loosePanel.gameObject.SetActive(true);
        _gamePanel.gameObject.SetActive(false);
    }
}
