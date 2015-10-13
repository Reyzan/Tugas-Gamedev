using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {

	//Integer
	public int maxHealth;
	public int currentHealth;
	//float
	public float distance;
	public float wakeRange;
	public float shootInterval;
	public float bulletSpeed = 100f;
	public float bulletTimer;
	// booleans
	public bool awake = false;
	public bool lookingRight = true;
	//references
	public GameObject bullet;
	public Transform target;
	public Animator animator;
	public Transform shootPointLeft, shootPointRight;

	void Start(){
		maxHealth = 100;
		currentHealth = maxHealth;
	}
	void Awake(){
		animator = gameObject.GetComponent<Animator> ();
	}
	void Update(){
		animator.SetBool ("Awake", awake);
		animator.SetBool ("LookingRight", lookingRight);
		RangeCheck ();
		if (target.transform.position.x > transform.position.x) {
			lookingRight = true;
		} else {
			lookingRight = false;
		}
	}

	void RangeCheck(){
		distance = Vector3.Distance (transform.position, target.transform.position);

		if (distance < wakeRange) {
			awake = true;
		} else {
			awake = false;
		}
	}
	public void Attack(bool attackingRight){
		bulletTimer += Time.deltaTime;
		if (bulletTimer >= shootInterval) {
			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize();

			if (!attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
				bulletTimer = 0;
			} else {
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
				bulletTimer = 0;
			}
		}
	}
}
