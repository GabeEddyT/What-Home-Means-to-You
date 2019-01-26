using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BACMeter : MonoBehaviour
{
    public float BloodAlcoholContent = 0.40f;
    Slider BACSlider;
    public Text BACLabel;
    public float BACDecayRate = 0.004f;
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        BACSlider = GetComponent<Slider>();
        BACSlider.maxValue = Data.BAC_MAX;
        BloodAlcoholContent = Data.BAC_MAX;
    }

    // Update is called once per frame
    void Update()
    {
        BloodAlcoholContent -= BACDecayRate * Time.unscaledDeltaTime;
        BACSlider.value = BloodAlcoholContent;
        BACLabel.text = "BAC: " + BloodAlcoholContent.ToString("0.000") + " %";
        BACSlider.colors = new ColorBlock
        {
            normalColor = gradient.Evaluate((Data.BAC_MAX - BloodAlcoholContent) / Data.BAC_MAX),
            disabledColor = gradient.Evaluate((Data.BAC_MAX - BloodAlcoholContent) / Data.BAC_MAX),
            highlightedColor = gradient.Evaluate((Data.BAC_MAX - BloodAlcoholContent) / Data.BAC_MAX),
            pressedColor = gradient.Evaluate((Data.BAC_MAX - BloodAlcoholContent) / Data.BAC_MAX),
            colorMultiplier = 1
        };
    }
}
