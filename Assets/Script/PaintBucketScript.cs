using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketScript: MonoBehaviour
{

    public Color[] colorList;
    public Color currentColor;
    public int colorCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentColor = colorList[colorCount];
        //Debug.Log(currentColor);
        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

        if (Input.GetButton("Fire1"))
        {
            if (hit.collider!= null)
            {
                SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                //Debug.Log(hit.collider.name);

                sp.color = currentColor;
            }
        }
    }

    public void paint(int colorCode)
    {
        colorCount = colorCode;
        //Debug.Log(colorCode);
    }
}
