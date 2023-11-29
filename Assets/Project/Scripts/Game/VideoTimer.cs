using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoTimer : MonoBehaviour
{
    [SerializeField] private Text _timer;
    [SerializeField] private VideoPlayer _player;

    private void FixedUpdate()
    {
        _timer.text = _player.time.ToString("0.00");
    }
}
