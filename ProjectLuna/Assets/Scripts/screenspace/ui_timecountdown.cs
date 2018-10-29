/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls UI timer fill
 *  
 */

using UnityEngine;
using UnityEngine.UI;

public class ui_timecountdown : MonoBehaviour
{

    public Image _time;
    public gm_Primary _gm;
    public float _interspeed;

    void Start()
    {
        _time = gameObject.GetComponent<Image>();
    }

    void OnGUI()
    {
        float currentValue = _time.fillAmount;
        float desiredValue = (_gm._waveTimeNext / _gm._timeAtStartOfwave);

        _time.fillAmount = Mathf.Lerp(currentValue, desiredValue, _interspeed);
    }
}
