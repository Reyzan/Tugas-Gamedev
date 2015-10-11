using UnityEngine;
using System.Collections;

public class CharacterBehaviourScript : MonoBehaviour {

	// [HideInInspector] means this public variable
	// will not appear and uneditable in inspector
	// component 
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool facingRight = true;
	// public variable will shown in inspector
	// so the value can be changed and different 
	// for all object that using same scripts.
	// It's good practice to give it default value
	// like this
	public float jumpForce = 1000f;
	public float moveForce = 185f;
	public float maxSpeed = 4f;
	public bool grounded;
	private Rigidbody2D rigidbody2d;
	private Animator animator;
	public float healthBar;
	// for full documentation about unity behavior lifecycle
	// see http://docs.unity3d.com/Manual/ExecutionOrder.html
	void Start(){
		healthBar = 100f;
	}
	void Awake () {
		// Animator component animate the object based
		// on behavior we set in animator
		animator = GetComponent<Animator> ();

		// Rigidbody2D component give the object physics
		// properties like mass, drag, etc. We can interact
		// with the object using rigidbody2d by using physics
		// properties, like throwing a ball is essentially
		// add force to ball to specific direction
		rigidbody2d = GetComponent<Rigidbody2D> ();	
	}
	//void OnTriggerEnter2D(Collider2D col){
	//	grounded = true;
	//}
	//void OnTriggerExit2D(Collider2D col){
	//	grounded = false;
	//}
	void Update () {
		/*------------
		 * Input
		 * -----------*/
		// Character will jump if the player told so and
		// the character is on ground (so it cannot jump mid-air)
		// TODO : Check if the character is on the ground]
		   grounded = true;
		// animator.SetBool ("Grounded", grounded);
		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate () {
		/*------------
		 * Movement
		 * -----------*/
		// Get direction (positive = right, negative = left, zero = nothing)
		// This is defined in InputManager (Edit->Project Settings->Input)
		float direction = Input.GetAxisRaw("Horizontal");
		animator.SetFloat ("speed", Mathf.Abs(direction));

		// Move to direction
		// Accelerate the object to its max speed
		if (rigidbody2d.velocity.x * direction < maxSpeed)
			rigidbody2d.AddForce (Vector2.right * moveForce * direction);
		// constant speed when reached max speed
		if (Mathf.Abs (rigidbody2d.velocity.x) > maxSpeed) {
			float xSpeed = maxSpeed * Mathf.Sign(rigidbody2d.velocity.x);
			float ySpeed = rigidbody2d.velocity.y;

			rigidbody2d.velocity = new Vector2 (xSpeed, ySpeed);
		}

		// Change character facing direction
		if (direction > 0 && !facingRight)
			Flip ();
		else if (direction < 0 && facingRight)
			Flip ();

		// if character is ordered to jump
		if (jump) {
			// TODO : Animate character to jump animation

			// TODO : get the character jumping
			//Debug.Log ("I jump!");
			grounded = false;
			rigidbody2d.AddForce(new Vector2(0f, jumpForce));
			// successfully jumped, set jump to false so in next frame
			// it isn't jump twice
			jump = false;
		}
	}
	void OnTriggerEnter2D(Collider2D collision){
		if(collision.CompareTag("Ground")){
		    grounded = true;
		}
		if (collision.gameObject.CompareTag ("Collectible")) {
			collision.gameObject.SetActive(false);
		}
		if (collision.gameObject.CompareTag ("Spike")){
			healthBar -= 10f;
			Debug.Log("taking damage");
		}
	}
	void OnTriggerExit2D(Collider2D collision){
		if(collision.CompareTag("Ground")){
			grounded = false;
		}
	}
	// Flip the character facing direction
	void Flip() {
		// flip the flag
		facingRight = !facingRight;

		// flip by scale
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
