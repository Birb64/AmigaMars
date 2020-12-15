using System;
using UnityEngine;

public class MonitorScore : MonoBehaviour
{
    public string tag = "Player";
    public int ScoreAmount;
    public int RingAmount;
    public float Bounce;
    bool HasActivated;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag && !HasActivated && !other.gameObject.GetComponent<PlayerController>().isGrounded) { 
        GetComponent<AudioSource>().Play();
        other.gameObject.GetComponent<ScoreManager>().score += ScoreAmount;
        other.gameObject.GetComponent<Rigidbody>().velocity += other.gameObject.transform.up * Bounce;
        HasActivated = true;
            other.gameObject.GetComponent<RingManager>().rings += RingAmount;
        }
    }
    void Update()
    {
        if(HasActivated && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
