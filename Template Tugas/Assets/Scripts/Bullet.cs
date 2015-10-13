using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public PlayerHealthScript playerHealth;
	public CharacterBehaviourScript characterBehaviour;
	void Awake(){
		playerHealth = GetComponent<PlayerHealthScript> ();
		characterBehaviour = GetComponent<CharacterBehaviourScript> ();
	}
	void OnTriggerEnter2D (Collider2D collision) {
		if (collision.isTrigger != true) {
			if(collision.CompareTag("Player")){
				Debug.Log("hehe");
				playerHealth.takingDamage(20f);
				playerHealth.healthAmount(0.2f);
			}
			Destroy(gameObject);
		}
	}
}
