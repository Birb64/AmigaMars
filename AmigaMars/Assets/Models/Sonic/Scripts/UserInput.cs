using UnityEngine;

public class UserInput : MonoBehaviour {
	public float IncSpeed = 0.2f;
	public float DecSpeed = 0.5f;
	public float MaximumSpeed = 25f;

	private float MoveSpeed;
	private float MaxMoveSpeed;

	private bool IsMoving;

	PlayerMove character;
	Transform cam;

	private Vector3 camForward;
	private Vector3 move;

	public GameObject SonicMesh;
	public GameObject BallMesh;
	void Start() {
		if(Camera.main != null)
			cam = Camera.main.transform;
		
		character = GetComponent<PlayerMove>();
	}

		float axis;
		float axis2;
	void FixedUpdate() {
		axis = Input.GetAxis("Horizontal");
		axis2 = Input.GetAxis("Vertical");
		if (axis >= 0.2 || axis2 >= 0.2 || axis <= -0.2 || axis2 <= -0.2)
		{
			MaxMoveSpeed = MaximumSpeed;
			IsMoving = true;
		}
		else
		{
			MaxMoveSpeed = 0f;
			IsMoving = false;
		}
		if (cam != null)
		{
			camForward = Vector3.Scale(cam.forward, new Vector3(1f, 0f, 1f)).normalized;
			move = axis2 * camForward + axis * cam.right;
		}
		else if(!PlayerController.IsLooped)
		{
			move = axis2 * Vector3.forward + axis * Vector3.right;
		}

		if (!IsMoving)
		{
			MoveSpeed -= DecSpeed;
			if (MoveSpeed < 0f)
				MoveSpeed = 0f;
		}
		if(IsMoving)
		{
			MoveSpeed += IncSpeed;
			if (MoveSpeed > MaxMoveSpeed)
				MoveSpeed = MaxMoveSpeed;
		}
        if (PlayerController.IsLooped)
        {
			
			transform.Rotate(0, axis, 0, Space.Self);
			//transform.Translate(Vector3.right * axis * Time.deltaTime);
		}
		if (move.magnitude > 1f)
		
			move.Normalize();

			character.Move(move);
		
		transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
	}
	void Update()
    {
		
		if (!IsMoving)
		{
			GetComponent<Animator>().SetFloat("Speed", Mathf.Lerp(GetComponent<Animator>().GetFloat("Speed"), 0f, IncSpeed));
			
		}
		else
		{
			GetComponent<Animator>().SetFloat("Speed", Mathf.Lerp(GetComponent<Animator>().GetFloat("Speed"), 1f, IncSpeed));
			
		}
		







		// This will align the player along sloped surfaces
		if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.05f)) {
			Vector3 up = hit.normal;
			Vector3 vel = transform.forward;
			Vector3 forward = vel - up * Vector3.Dot(vel, up);
			if (Mathf.Sign(forward.x) * Mathf.Sign(GetComponent<Rigidbody>().velocity.x) == 1)
			{ transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward, forward.normalized, 0.48f), Vector3.Lerp(transform.up, up, 0.48f)); }
            else { transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward, forward.normalized, 0.32f), Vector3.Lerp(transform.up, up, 0.32f)); }
			if (axis < 0.2 && axis2 < 0.2 && axis > -0.2 && axis2 > -0.2 && !Input.GetKey(KeyCode.Space))
			{
				transform.position += new Vector3(-Vector3.up.x / 100 - -up.x / 100, -Vector3.up.y / 100 - -up.y / 100, -Vector3.up.z / 100 - -up.z / 100);
			}
            else 
					{ transform.position -= new Vector3(-Vector3.up.x / 100 - -up.x / 100, -Vector3.up.y / 100 - -up.y / 100, -Vector3.up.z / 100 - -up.z / 100); }
			
		}

    }
}