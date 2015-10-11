using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {
	private Vector2 velocity;

	[HideInInspector] public float smoothTimeX = 0.05f;
	[HideInInspector] public float smoothTimeY = 0.05f;

	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX, posY, transform.position.z);
	}
}
