using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public SpriteRenderer[] Columns;
    public Sprite[] Sprites;
    // Update is called once per frame
    void Update()
    {
        int Timing = (int)(Time.time * 1000);
        Columns[0].sprite = Sprites[(Timing / 10000) % 10];
        Columns[1].sprite = Sprites[(Timing / 1000) % 10];
        Columns[2].sprite = Sprites[(Timing / 100) % 10];
        Columns[3].sprite = Sprites[(Timing / 10) % 10];

    }
}
