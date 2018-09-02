using UnityEngine;
using UnityEngine.UI;

public class ui_timecountdown : MonoBehaviour
{

    public Image _time;
    public gm_Primary _gm;
    public float _interspeed;
    // Use this for initialization
    void Start()
    {
        _time = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentValue = _time.fillAmount;
        float desiredValue = (_gm._waveTimeNext / _gm._timeAtStartOfwave);

        _time.fillAmount = Mathf.Lerp(currentValue, desiredValue, _interspeed);
    }
}
