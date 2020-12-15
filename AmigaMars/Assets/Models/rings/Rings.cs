﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
    public int RingAmount;
    public int ScoreAmount;
    public string tag;
    bool HasActivated;
    void Update()
    {
        if (HasActivated && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
        transform.Rotate(0, 5, 0, Space.Self);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag && !HasActivated)
        {
            other.gameObject.GetComponent<RingManager>().rings += RingAmount;
            other.gameObject.GetComponent<ScoreManager>().score += ScoreAmount;
            HasActivated = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
