using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    Rigidbody rb;
    void Start(){
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionExit(){
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.06f)){
            isGrounded = false;
        }

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (!isGrounded){
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.Lerp(transform.up, Vector3.up, 0.8f));
            GetComponent<UserInput>().SonicMesh.SetActive(false);
            GetComponent<UserInput>().BallMesh.SetActive(true);
            GetComponent<UserInput>().BallMesh.transform.Rotate(0, 0, 32, Space.Self);
        }
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.064f)){
            isGrounded = true;
        }
    }
}
