using UnityEngine;
using UnityEngine.UI;

public class MenuLvlVisuals : MonoBehaviour
{
    [SerializeField] private Text _progressPercent;
    [SerializeField] private Text _triesCount;
    [SerializeField] private Text _lvlNumber;

    [SerializeField] private RectTransform _progressPanel;
    [SerializeField] private RectTransform _triesPanel;
    [SerializeField] private RectTransform _lockPanel;


    public void Initialize(bool isOpen, int lvlNumber, float progress = 0, int triesCount = 0)
    {
        _lvlNumber.text = (lvlNumber + 1).ToString();

        if(isOpen)
        {
            _progressPanel.gameObject.SetActive(true);
            _triesPanel.gameObject.SetActive(true);
            _lockPanel.gameObject.SetActive(false);

            _progressPercent.text = LvlData.RoundPercentToString(progress) + "%";
            _progressPercent.color = MenuVisuals.Instance.ColorByProgressPercent(progress);

            _triesCount.text = triesCount.ToString();
        }
        else
        {
            _progressPanel.gameObject.SetActive(false);
            _triesPanel.gameObject.SetActive(false);
            _lockPanel.gameObject.SetActive(true);

            Debug.Log("Closed");
        }
    }
}
