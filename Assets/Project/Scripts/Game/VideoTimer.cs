using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoTimer : MonoBehaviour
{
    [SerializeField] private Text _timer;
    [SerializeField] private VideoPlayer _video;

    private void FixedUpdate()
    {
        _timer.text = _video.time.ToString("0.00") + "s";
    }
}
