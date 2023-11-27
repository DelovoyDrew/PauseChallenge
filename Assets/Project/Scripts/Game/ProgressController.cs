using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private ResultShower _shower;
    [SerializeField] private Gradient _resultColor;

    [SerializeField] private Text _triesCount;
    [SerializeField] private Text _progressPercent;

    private void Start()
    {
        VideoStopper.Instance.OnPause += ProcessPercent;
        VideoStopper.Instance.OnTry += NewTry;

        UpdatePercent(ColorByProgressPercent(GlobalSaver.Instance.CurrentLvlData.DonePercent), GlobalSaver.Instance.CurrentLvlData.DonePercent);
        UpdateTries();
    }

    public void NewTry()
    {
        GlobalSaver.Instance.CurrentLvlData.Tries++;
        UpdateTries();
    }

    public void ProcessPercent(float percent)
    {
        var lvlData = GlobalSaver.Instance.CurrentLvlData;
        var color = ColorByProgressPercent(percent);

        if (lvlData.DonePercent < percent)
        {
            lvlData.DonePercent = percent;
            UpdatePercent(color, percent);

            if(percent >= 100)
                GlobalSaver.Instance.Save();
        }

        _shower.ShowResult(percent, color);
    }

    public Color ColorByProgressPercent(float percent)
    {
        return _resultColor.Evaluate((int)percent / 100f);
    }

    private void UpdatePercent(Color color, float percent)
    {
        var progressPercentColor = color;
        progressPercentColor.a = 255;
        _progressPercent.color = progressPercentColor;
        _progressPercent.text = LvlData.RoundPercentToString(percent) + "%";
    }

    private void UpdateTries()
    {
        _triesCount.text = GlobalSaver.Instance.CurrentLvlData.Tries.ToString();
    }
}
