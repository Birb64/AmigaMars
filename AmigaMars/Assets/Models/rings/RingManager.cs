using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    public int rings;
    public SpriteRenderer RingColumn1;
    public SpriteRenderer RingColumn2;
    public Sprite[] Sprites;
    void Update()
    {
        RingColumn1.sprite = Sprites[rings % 10];
        RingColumn2.sprite = Sprites[rings / 10];
    }
}
