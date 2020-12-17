using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public Transform Level1;
    public Transform Level1Showoff;
    public Transform Level2;
    public Transform Level2Showoff;
    public SpriteRenderer FadeToBlack;
    bool ChangeRight;
    bool ChangeLeft;
    public Vector3[] placement;
    int dontGoTooHigh;
    bool EnterLevel;
    bool EnterLevel1;
    bool DeactivateControls;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) { SceneManager.LoadScene(0); }
        Level1Showoff.Rotate(0, 0, 0.32f, Space.Self);
        Level2Showoff.Rotate(0, 0.32f, 0, Space.Self);
        if(Input.GetAxis("Horizontal") >= 0.2 && Input.GetButtonDown("Horizontal") && !DeactivateControls)
        {
            dontGoTooHigh += 1;
            if (dontGoTooHigh > -1 && dontGoTooHigh < 2)
            {
                ChangeRight = true;
                placement[0] = Level1.position - new Vector3(32, 0, 0);
                placement[1] = Level2.position - new Vector3(32, 0, 0);
            }
            
        }
        if (Input.GetAxis("Horizontal") <= -0.2 && Input.GetButtonDown("Horizontal") && !DeactivateControls)
        {
            dontGoTooHigh -= 1;
            if (dontGoTooHigh < 2 && dontGoTooHigh > -1)
            {
                ChangeLeft = true;
                placement[0] = Level1.position + new Vector3(32, 0, 0);
                placement[1] = Level2.position + new Vector3(32, 0, 0);
            }
        }
        
        if(ChangeLeft || ChangeRight)
        {
            Level1.position = Vector3.Lerp(Level1.position, placement[0], 0.032f);
            Level2.position = Vector3.Lerp(Level2.position, placement[1], 0.032f);
            if (Level1.position == placement[0] && Level2.position == placement[1])
            {
                ChangeLeft = false;
                ChangeRight = false;
            }
        }
        if(Input.GetButtonDown("Jump") && dontGoTooHigh == 0)
        {
            DeactivateControls = true;
        }
        
        if (Input.GetButtonDown("Jump") && dontGoTooHigh == 1)
        {
            DeactivateControls = true;
        }
        
        Debug.Log(dontGoTooHigh.ToString());
        if(DeactivateControls)
        {
            FadeToBlack.color += new Color(0, 0, 0, 0.005f);
        }
        if(FadeToBlack.color == new Color(0,0,0,1))
        {
            if (dontGoTooHigh == 0)
            {
                SceneManager.LoadScene(2);
            }
            if (dontGoTooHigh == 1)
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
