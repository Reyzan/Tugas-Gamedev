﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D collision) {
			if(collision.CompareTag("Player")){
				Destroy(gameObject);	
			}
	}
}
