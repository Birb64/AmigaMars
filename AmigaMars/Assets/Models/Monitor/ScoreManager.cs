using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public SpriteRenderer[] Columns;
    public Sprite[] Sprites;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1)) { score += 1; }
        if (Input.GetKey(KeyCode.Keypad2)) { score += 5; }
        if (Input.GetKey(KeyCode.Keypad3)) { score += 10; }
        Columns[0].sprite = Sprites[score % 10];
        Columns[1].sprite = Sprites[(score % 100) / 10];
        Columns[2].sprite = Sprites[(score % 1000) / 100];
        Columns[3].sprite = Sprites[(score % 10000) / 1000];
        Columns[4].sprite = Sprites[(score % 100000) / 10000];
        Columns[5].sprite = Sprites[(score % 1000000) / 100000];
        Columns[6].sprite = Sprites[(score % 10000000) / 1000000];
        Columns[7].sprite = Sprites[(score % 100000000) / 10000000];
    }
}
