using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    public float duration;
    float pendingFreezeDuration=0f;
    public bool isFrozen = false;

    // Update is called once per frame
    void Update()
    {
        if (pendingFreezeDuration > 0 && !isFrozen)
            StartCoroutine(DoFreeze());
    }

    public void Freeze()
	{
        pendingFreezeDuration = duration;
	}
    IEnumerator DoFreeze()
	{
        isFrozen = true;
        var originial = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = originial;
        pendingFreezeDuration = 0;
        isFrozen = false;
	}
}
