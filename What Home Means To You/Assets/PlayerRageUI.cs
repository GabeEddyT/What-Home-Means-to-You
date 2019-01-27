using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class PlayerRageUI : MonoBehaviour
{

    public float RageMeter = 0.0f;
    Slider RageSlider;

    public Text RageText;
    public float RageDecayRate = 0.004f;
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        RageSlider = GetComponent<Slider>();
        RageSlider.maxValue = Data.RAGE_MAX;
        RageMeter = 0;// Data.BAC_MAX;
    }

    // Update is called once per frame
    void Update()
    {
        RageMeter -= RageDecayRate * Time.unscaledDeltaTime;
        RageSlider.value = RageMeter;
        RageText.text = "Rage: " + RageMeter.ToString("0.00") + " %";
        RageSlider.colors = new ColorBlock
        {
            normalColor = gradient.Evaluate(1 - ((Data.RAGE_MAX - RageMeter) / Data.RAGE_MAX)),
            highlightedColor = gradient.Evaluate(1 - ((Data.RAGE_MAX - RageMeter) / Data.RAGE_MAX)),
            disabledColor = gradient.Evaluate(1 - ((Data.RAGE_MAX - RageMeter) / Data.RAGE_MAX)),
            pressedColor = gradient.Evaluate(1 - ((Data.RAGE_MAX - RageMeter) / Data.RAGE_MAX)),
            colorMultiplier = 1
        };

        if (RageMeter <= 0.0f)
        {
            RageMeter = 0f;
        }   
        
    }
    public void AddRage(float numToAdd, GameObject playerBeingDamaged)
    {
        RageMeter += numToAdd;
        if (RageMeter >= 100.0f)
        {
            if (playerBeingDamaged.gameObject.name.Contains("P1"))
            {
                Camera.main.gameObject.GetComponent<TwoPlayerCameraLogic>().PlayerWinCameraEffect(2);
            }
            else
            {
                Camera.main.gameObject.GetComponent<TwoPlayerCameraLogic>().PlayerWinCameraEffect(1);
            }
        }
    }

}
