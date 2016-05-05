using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
	public Transform target;
	public float distance = 10.0f;
	public float sensitivity = 3.0f;

	private Vector3 offset; 

	void Start ()  {
		offset = (transform.position - target.position).normalized * distance;
		transform.position = target.position + offset;
	}

	void Update () {
		Quaternion q = Quaternion.AngleAxis(Input.GetAxis ("Mouse X") * sensitivity, Vector3.up);
		offset = q * offset;
		transform.rotation = q * transform.rotation;
		transform.position = target.position + offset;
	}
}

