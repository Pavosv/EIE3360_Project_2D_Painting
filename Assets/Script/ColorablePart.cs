using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorablePart : MonoBehaviour
{
    public int colorIndex;
    private SpriteRenderer sp;
    public PaintBucketScript paintBucket;

    private bool correctlyColored = false;

    // Start is called before the first frame update
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    public bool IsCorrectlyColored()
    {
        if(!correctlyColored && sp.color == paintBucket.colorList[colorIndex])
        {
            correctlyColored = true;
            return true;
        }
        return false;
    }

    public bool CheckIncorrectColor()
    {
        if (correctlyColored && sp.color != paintBucket.colorList[colorIndex])
        {
            correctlyColored = false;
            return true;
        }
        return false;
    }

}
