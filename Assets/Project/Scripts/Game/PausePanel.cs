using UnityEngine;
using DG.Tweening;

public class PausePanel : GamePanelUI
{
    [SerializeField] private float _fadeDelay;
    [SerializeField] private CanvasGroup _group;

    public void Open(bool isOpen)
    {
        var targetValue = isOpen ? 1 : 0;
        _group.DOFade(targetValue, _fadeDelay);
    }
}
