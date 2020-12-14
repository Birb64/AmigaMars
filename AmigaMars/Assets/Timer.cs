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
        int Timing = (int)Time.time;
        int Timing2 = (int)(Time.time * 10 % 10);
        int Timing3 = (int)(Time.time * 100 % 10);
        Columns[0].sprite = Sprites[Timing / 10];
        Columns[1].sprite = Sprites[Timing % 10];
        Columns[2].sprite = Sprites[Timing2];
        Columns[3].sprite = Sprites[Timing3];

    }
}
