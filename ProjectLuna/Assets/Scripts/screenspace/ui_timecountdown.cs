using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_timecountdown : MonoBehaviour
{

    public Image time;
    public gm_Primary gm;
    public float interspeed;
    // Use this for initialization
    void Start()
    {
        time = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentValue = time.fillAmount;
        float desiredValue = (gm._waveTimeNext / gm.timeAtStartOfwave);

        time.fillAmount = Mathf.Lerp(currentValue, desiredValue, interspeed);
    }
}
