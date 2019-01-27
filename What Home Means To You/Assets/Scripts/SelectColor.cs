﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    public enum Modes
    {
        Hair,
        Shirt,
        Shorts,
        Shoes
    }
    public Gradient gradient;
    public Color color;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public string replaceParam = "_First_Replace";
    public Text partLabel;
    float hue, saturation, value;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Image>().material;
        redSlider.value = mat.GetColor(replaceParam).r;
        greenSlider.value = mat.GetColor(replaceParam).g;
        blueSlider.value = mat.GetColor(replaceParam).b;
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    {
        // Save the changed colors to the actual material
        GetComponent<SpriteRenderer>().sharedMaterial = mat;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mat.SetColor(replaceParam, new Color(redSlider.value, greenSlider.value, blueSlider.value));        
    }

    public void ChangeColor()
    {
        
    }

    public void SwitchParam(float param)
    {
        switch ((int)param)
        {
            case 0:
                replaceParam = "_First_Replace";
                break;
            case 1:
                replaceParam = "_Second_Replace";
                break;
            case 2:
                replaceParam = "_Third_Replace";
                break;
            case 3:
                replaceParam = "_Fourth_Replace";
                break;
            default:
                break;
        }
        partLabel.text = ((Modes)param).ToString();
        redSlider.value = mat.GetColor(replaceParam).r;
        greenSlider.value = mat.GetColor(replaceParam).g;
        blueSlider.value = mat.GetColor(replaceParam).b;
    }
}
