using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurboAdjustment : MonoBehaviour
{
    public List<float> TurboList;
    public Text TurboMultiplier;

    float currentSetting = Data.TURBO_SETTING;
    int currentIndex = 0;
    bool hitLastFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        TurboList.Sort();
        currentIndex = TurboList.FindIndex(x => x == currentSetting);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (!hitLastFrame)
            {
                currentSetting = TurboList[(currentIndex = (currentIndex + TurboList.Count + (int)Input.GetAxisRaw("Horizontal")) % TurboList.Count)];
            }
            hitLastFrame = true;
        }
        else
        {
            hitLastFrame = false;
        }
        TurboMultiplier.text = currentSetting + " x";
        Data.TURBO_SETTING = currentSetting;
        Time.timeScale = Data.TURBO_SETTING;
    }
}
