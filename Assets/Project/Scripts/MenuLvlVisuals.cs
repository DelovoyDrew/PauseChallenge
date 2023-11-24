using UnityEngine;
using UnityEngine.UI;

public class MenuLvlVisuals : MonoBehaviour
{
    [SerializeField] private Text _progressPercent; 

    public void Initialize(bool isOpen, float progress)
    {
        if(isOpen)
        {
            _progressPercent.text = progress.ToString("0.00" + "%");
        }
        else
        {
            Debug.Log("Closed");
        }
    }
}
