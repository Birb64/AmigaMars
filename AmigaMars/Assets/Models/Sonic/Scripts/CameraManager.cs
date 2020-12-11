using UnityEngine;

public class CameraManager : MonoBehaviour {
    public Transform PlayerFollow;
    public Vector3 CameraOffset;
    public float SmoothFollow = 2.0f;

	void Start () {
        Application.targetFrameRate = 30;
    }

    void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, PlayerFollow.position + CameraOffset, SmoothFollow * Time.fixedDeltaTime);
    }
}
