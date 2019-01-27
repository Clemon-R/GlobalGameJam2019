using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScreen : MonoBehaviour
{

    public float duration;
    private float m_pendingFreezeDuration;
    private bool m_isFrozen;

	void Start ()
    {
        m_isFrozen = false;
	}
	
	void Update ()
    {
		if(m_pendingFreezeDuration > 0 && !m_isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
	}

    public void Freeze()
    {
        m_pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        m_isFrozen = true;

        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        m_pendingFreezeDuration = 0;
        m_isFrozen = false; 
    }
}
