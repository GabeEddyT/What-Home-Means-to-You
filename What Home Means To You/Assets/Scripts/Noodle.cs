using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Noodle : MonoBehaviour
{
    public float MovementSpeed = 20f;
    public Slider BACSlider;
    public Text BACLabel;
    public float BloodAlcoholContent = .05f;
    public float BACDecayRate = 0.004f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime) * MovementSpeed);
        UpdateBAC();
    }

    public void ScaleShit(float scale)
    {
        Time.timeScale = scale;
    }

    public void UpdateBAC()
    {
        BloodAlcoholContent -= BACDecayRate * Time.deltaTime;
        BACSlider.value = BloodAlcoholContent;
        BACLabel.text = "" + BloodAlcoholContent;
    }
}
