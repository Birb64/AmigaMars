﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    Rigidbody rb;
    public GameObject SonicMesh;
    public GameObject BallMesh;
    void Start(){
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay(){
        isGrounded = true;
    }
    void OnCollisionExit(){
        isGrounded = false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (!isGrounded){
            SonicMesh.SetActive(false);
            BallMesh.SetActive(true);
        }
    }
}
