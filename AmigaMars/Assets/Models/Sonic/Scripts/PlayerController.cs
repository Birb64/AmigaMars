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
    public bool DoubleJump;
    public Vector3 DoubleJumpModifier;
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ground" && !Input.GetKey(KeyCode.Space))
        {
            isGrounded = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    bool HasPressedMainJump;
    bool IsJumpable;
    public bool dontChange;
    void Update(){
        if(DoubleJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(!IsJumpable && !dontChange)
                {
                    HasPressedMainJump = true;
                }
                if (IsJumpable && isGrounded)
                {
                    IsJumpable = false;
                    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                    isGrounded = false;
                    dontChange = false;
                    transform.rotation = transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
                    GetComponent<AudioSource>().Play();
                }
                if(HasPressedMainJump)
                {
                    dontChange = true;
                        rb.AddForce((jump * jumpForce) - DoubleJumpModifier, ForceMode.Impulse);
                        GetComponent<AudioSource>().Play();
                    HasPressedMainJump = false;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !DoubleJump)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            transform.rotation = transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            GetComponent<AudioSource>().Play();
        }
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit5, 0.04f) && Input.GetKey(KeyCode.Space))
        {
            isGrounded = true;
        }
        if (!isGrounded){
            GetComponent<UserInput>().SonicMesh.SetActive(false);
            GetComponent<UserInput>().BallMesh.SetActive(true);
            GetComponent<UserInput>().BallMesh.transform.Rotate(0, 0, 32, Space.Self);
            
        }
        if(isGrounded)
        {
            GetComponent<UserInput>().SonicMesh.SetActive(true);
            GetComponent<UserInput>().BallMesh.SetActive(false);
            IsJumpable = true;

            dontChange = false;
        }
    }
}
