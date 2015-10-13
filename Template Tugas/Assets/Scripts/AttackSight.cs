using UnityEngine;
using System.Collections;

public class AttackSight : MonoBehaviour {
	public TurretAI TurretAI;
	public bool isLeft = false;
	// Use this for initialization
	void Awake () {
		TurretAI = gameObject.GetComponentInParent<TurretAI> ();
	}
	
	// Update is called once per frame
	void OnTriggerStay2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			if(isLeft){
				Debug.Log("kiri");
				TurretAI.Attack(false);
			} else {
				Debug.Log("kanan");
				TurretAI.Attack(true);
			}
		}
	}
}
