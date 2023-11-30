using UnityEngine;
using UnityEngine.UI;

public class VideoTimer : MonoBehaviour
{
    [SerializeField] private Text _timer;

    private void FixedUpdate()
    {
        _timer.text = VideoStopper.Instance.showTime.ToString("0.00");
    }
}
