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
    public float DoubleJumpModifier;
    public static bool IsLooped;
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ground" && !Input.GetKey(KeyCode.Space))
        {
            isGrounded = true;
        }
        if(other.gameObject.tag == "Loop" && isGrounded) { rb.useGravity = false; IsLooped = true; }
        if (other.gameObject.tag == "Loop" && !isGrounded) { rb.useGravity = true; IsLooped = false; }
        Debug.Log(other.gameObject.tag);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
            transform.rotation = transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        }
        if (other.gameObject.tag == "Loop") { rb.useGravity = true; IsLooped = false; }
    }
    bool HasPressedMainJump;
    bool IsJumpable;
    bool dontChange;
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
                    rb.velocity += jump * jumpForce;
                    dontChange = false;
                    
                    GetComponent<AudioSource>().Play();
                    GetComponent<AudioSource>().pitch = 1f;
                }
                if(HasPressedMainJump)
                {
                    dontChange = true;
                    rb.velocity += (jump * jumpForce) - new Vector3(0,DoubleJumpModifier,0);
                        GetComponent<AudioSource>().Play();
                    GetComponent<AudioSource>().pitch = 1.1f;
                    HasPressedMainJump = false;
                }
            }
        }
       if(rb.velocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            isGrounded = false;
        }
       if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.05f) && Input.GetKey(KeyCode.Space) && rb.velocity.y < 0)
        {
            isGrounded = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !DoubleJump && isGrounded)
        {
            rb.velocity += jump * jumpForce;
            isGrounded = false;
            transform.rotation = transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            GetComponent<AudioSource>().Play();
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

