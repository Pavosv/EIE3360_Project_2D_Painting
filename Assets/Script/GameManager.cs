using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ColorablePart[] colorableParts;
    public TextMeshProUGUI textCounter;
    private int correctlyColored;

    // Start is called before the first frame update
    void Start()
    {
        colorableParts = FindObjectsOfType<ColorablePart>();
    }

    // Update is called once per frame
    void Update()
    {
        updateTextCounter();
    }

    public void updateTextCounter()
    {
        IncrementCounter();
        textCounter.text = $"Correct parts {correctlyColored} / {colorableParts.Length}";
    }

    public void IncrementCounter()
    {
        foreach (ColorablePart part in colorableParts)
        {
            if (part.IsCorrectlyColored())
            {
                correctlyColored++;
            }
            else if (part.CheckIncorrectColor())
            {
                correctlyColored--;
            }
        }
    }
}
