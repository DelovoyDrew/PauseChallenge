using UnityEngine;
using UnityEngine.Events;

public class ObjectEnabler : MonoBehaviour
{
    public UnityEvent OnEnableEvent;

    public void StartEvent()
    {
        OnEnableEvent?.Invoke();
    }    
}
