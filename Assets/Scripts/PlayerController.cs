using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Update is called once per frame
	public Text countText;
	private Rigidbody rb;
	public float speed;
	private int count;
	public Text winText;
	float timeLeft = 60.0f;
	public Text timeText;
	public Transform cam;
	public bool win = false;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
		timeText.text = "Time: " + timeLeft.ToString ();
	}
	void FixedUpdate () {
		Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if(inputDirection.sqrMagnitude > 1)
		{
			inputDirection = inputDirection.normalized;
		}
		float moveHorizontal = Input.GetAxis ("Horizontal")*speed;
		float moveVertical = Input.GetAxis ("Vertical")*speed;

		Vector3 x = Vector3.Cross(Vector3.up, cam.forward);
		Vector3 y = Vector3.Cross(x, Vector3.up);
		Vector3 movement = (x * inputDirection.x) + (y * inputDirection.y);

		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText ();
		}
	}
	void SetCountText(){
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win!";
			win = true;
		}
	}
	void Update () {
		if (Input.GetKeyDown ("space") && GetComponent<Rigidbody>().transform.position.y == .5f) {
			Vector3 jump = new Vector3 (0.0f, 200.0f, 0.0f);

			GetComponent<Rigidbody>().AddForce (jump);
		}
		if (transform.position.y <= -10.0f)
		{
			Application.LoadLevel (Application.loadedLevel);
		}
		if (win == false) {
			timeLeft -= Time.deltaTime;
			int temp = (int)timeLeft;
			timeText.text = "Time: " + temp.ToString ();
			if (timeLeft < 0) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}