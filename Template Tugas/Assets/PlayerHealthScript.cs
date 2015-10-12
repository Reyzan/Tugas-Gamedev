using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;
	public Image healthbar;

	private CharacterBehaviourScript characterBehaviour;
	private Animator animator;
	private Rigidbody2D rigidbody2d;
	private bool isDead;
	public bool immune;
	// Use this for initialization
	void Start() {
		maxHealth = 100f;
		currentHealth = maxHealth;
	}
	void Awake () {
		rigidbody2d = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		characterBehaviour = GetComponent<CharacterBehaviourScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		healthColor ();
	}
	public void takingDamage(int n){
		if(!immune){
			currentHealth -= n; 
			healthbar.fillAmount -= 0.1f;
			Debug.Log ("taking damage");
			animator.SetBool ("pain", true);
			rigidbody2d.AddForce (new Vector2 (-1f, characterBehaviour.jumpForce / 2));

			if (currentHealth <= 0f && !isDead) {
				Death ();
			}
		}
	}
	public void healthColor(){
		if (immune) {
			healthbar.color = new Color (0f, 1f, 1f, 1f);
		} else {
			if (currentHealth >= 50f) {
				Debug.Log ("green");
				healthbar.color = new Color (0f, 1f, 0f, 1f);
			}
			if (currentHealth >= 25f && currentHealth < 50f) {
				Debug.Log ("orange");
				healthbar.color = new Color (1f, 0.35f, 0f, 1f);
			}
			if (currentHealth < 25f) {
				Debug.Log ("red");
				healthbar.color = new Color (1f, 0f, 0f, 1f);
			}
		}
	}
	void Death(){
		isDead = true;
		animator.SetTrigger ("dead");
		characterBehaviour.enabled = false;
	}
}

