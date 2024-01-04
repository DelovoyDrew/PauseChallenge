using System.Collections;
using UnityEngine;

public class TextDestroyer : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameObject.Find("Text") != null);    
        var obj = GameObject.Find("Text");
        Destroy(obj);
    }
}
