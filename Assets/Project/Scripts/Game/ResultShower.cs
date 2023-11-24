using UnityEngine;
using UnityEngine.UI;

public class ResultShower : MonoBehaviour
{
    [SerializeField] private Text _result;
    [SerializeField] private Q_Vignette_Base _vignette;
    [SerializeField] private Animator _animator;

    [SerializeField] private int _afterCommaNumbers;

    private readonly int SHOW_ANIMTION = Animator.StringToHash("Show");


    public void ShowResult(float result, Color color)
    {
        _animator.SetTrigger(SHOW_ANIMTION);

        string resultNumber = (System.Math.Round(result, _afterCommaNumbers)).ToString();

        _vignette.SetVignetteSkyColor(color);
        _vignette.SetVignetteMainColor(color);

        color.a = 255f;
        _result.color = color;

        _result.text = resultNumber + "%";
    }
}
