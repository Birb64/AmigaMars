using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
    public int Amount;
    public string tag;
    bool HasActivated;
    void Update()
    {
        if (HasActivated && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tag && !HasActivated)
        other.gameObject.GetComponent<RingManager>().rings += Amount;
        HasActivated = true;
        GetComponent<AudioSource>().Play();
    }
}
