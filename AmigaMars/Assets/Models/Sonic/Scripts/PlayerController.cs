using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public Vector3 GroundSensorForward;
    public Vector3 GroundSensorBackward;
    public Vector3 GroundSensorLeft;
    public Vector3 GroundSensorRight;
    public bool isGrounded;
    Rigidbody rb;
    void Start(){
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionExit(){
        

    }
    void OnCollisionStay()
    {
        
        isGrounded = true;
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
        if(Physics.Raycast(transform.position + GroundSensorForward, Vector3.down + GroundSensorForward, out RaycastHit hit, 0.05f + GetComponent<Rigidbody>().velocity.z*100) || Physics.Raycast(transform.position + GroundSensorBackward, Vector3.down + GroundSensorBackward, out RaycastHit hit2, 0.05f + GetComponent<Rigidbody>().velocity.z*100) || Physics.Raycast(transform.position + GroundSensorLeft, Vector3.down + GroundSensorLeft, out RaycastHit hit3, 0.05f + GetComponent<Rigidbody>().velocity.z * 100) || Physics.Raycast(transform.position + GroundSensorRight, Vector3.down + GroundSensorRight, out RaycastHit hit4, 0.05f + GetComponent<Rigidbody>().velocity.z * 100))
        {
            isGrounded = true;
        }
        else { isGrounded = false; }
       
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position + GroundSensorBackward, GroundSensorBackward);
        Gizmos.DrawLine(transform.position + GroundSensorForward, GroundSensorForward);
    }
}
