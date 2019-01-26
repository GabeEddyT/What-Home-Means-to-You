using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BACAdjustment : MonoBehaviour
{
    public List<float> BACList;

    public Text BACSettingText;
    public Text SecondsText;

    float currentSetting = Data.BAC_MAX;
    int currentIndex = 0;
    bool hitLastFrame = false;
    // Start is called before the first frame update
    void Start()
    {
        BACList.Sort();
        currentIndex = BACList.FindIndex(x => x == currentSetting);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (!hitLastFrame)
            {
                currentSetting = BACList[(currentIndex = (currentIndex + BACList.Count + (int)Input.GetAxisRaw("Horizontal")) % BACList.Count)];
            }
            hitLastFrame = true;
        }
        else
        {
            hitLastFrame = false;
        }
        BACSettingText.text = currentSetting.ToString("0.00") + " %";
        SecondsText.text = "(" + (currentSetting / .004).ToString("0") + " seconds)";
        Data.BAC_MAX = currentSetting;
    }
}
