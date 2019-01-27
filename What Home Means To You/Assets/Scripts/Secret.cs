using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Secret : MonoBehaviour
{

    List<KeyCode> KeyCodepresses;
    List<KeyCode> konamicode;
    public bool unlocked = false;
    public bool gameStarted = false;

    AudioSource audioSource;

    void Start()
    {
        KeyCodepresses = new List<KeyCode>();
        konamicode = new List<KeyCode>(){KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow,
             KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A};

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (!unlocked && !gameStarted)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                    OnInput(keyCode);
            }
        }
    }

    void OnInput(KeyCode KeyCodeValue)
    {
        KeyCodepresses.Add(KeyCodeValue);
        if (KeyCodepresses.Count > konamicode.Count)
            KeyCodepresses.RemoveAt(0);
        
        if(KeyCodepresses.Count == konamicode.Count)
        {
            bool correct = false;
            for (int i = 0; i < KeyCodepresses.Count; ++i)
            {
                if (KeyCodepresses[i] == konamicode[i])
                {
                    if (i == KeyCodepresses.Count - 1)
                    {
                        correct = true;
                    }
                }
                else
                    break;
            }

            if (correct)
            {
                unlocked = true;
                audioSource.Play();
            }
        }
    }
}
