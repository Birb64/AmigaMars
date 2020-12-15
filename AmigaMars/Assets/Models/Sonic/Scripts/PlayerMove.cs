using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public float moveSpeedMultiplier = 1f;
	public float stationaryTurnSpeed = 180f;
	public float movingTurnSpeed = 360f;

	private float turnAmount;
	private float forwardAmount;

	private Vector3 moveInput;

	public void Move(Vector3 move)
	{
		if (!PlayerController.IsLooped)
		{
			if (move.magnitude > 1f)
			{
				move.Normalize();
			}
			moveInput = move;
			ConvertMoveInput();
			ApplyExtraTurnRotation();
		}
	}

	private void ConvertMoveInput()
	{
		if (!PlayerController.IsLooped)
		{
			Vector3 vector = transform.InverseTransformDirection(moveInput);
			turnAmount = Mathf.Atan2(vector.x, vector.z);
			forwardAmount = vector.z;
		}
	}

	private void ApplyExtraTurnRotation()
	{
		if (!PlayerController.IsLooped)
		{
			float num = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
			transform.Rotate(0f, turnAmount * num * Time.deltaTime, 0f);
		}
	}

	private void OnAnimatorMove() {
		if (!PlayerController.IsLooped)
		{
			if (Time.deltaTime > 0f)
			{
				Vector3 velocity = GetComponent<Animator>().deltaPosition * moveSpeedMultiplier / Time.deltaTime;
				velocity.y = GetComponent<Rigidbody>().velocity.y;
				GetComponent<Rigidbody>().velocity = velocity;
			}
		}
	}
}