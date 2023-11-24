using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private ResultShower _shower;
    [SerializeField] private Gradient _resultColor;

    private void Start()
    {
        VideoStopper.Instance.OnPause += ProcessPercent;
    }

    public void ProcessPercent(float percent)
    {
        var color = _resultColor.Evaluate((int)percent / 100f);
        _shower.ShowResult(percent, color);
    }
}
