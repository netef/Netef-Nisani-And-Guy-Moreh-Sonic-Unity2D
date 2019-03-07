using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSequanceScript : MonoBehaviour {

	public GameObject theBoss;
	public GameObject leftWall;
	public GameObject rightWall;

	private bool flag = false;

	public float timeUntilStart;

	// Use this for initialization
	void Start () {
		timeUntilStart = 100f;
	}

	// Update is called once per frame
	void Update () {

		timeUntilStart -= Time.deltaTime;
		if (timeUntilStart <= 0 == !flag)
		{
			leftWall.SetActive(true);
			rightWall.SetActive(true);
			flag = true;
		}

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			timeUntilStart = 8f;
			theBoss.SetActive(true);
		}
	}

	
}
