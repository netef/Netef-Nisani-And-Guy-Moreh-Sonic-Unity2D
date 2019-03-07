using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public Text score;

	// Use this for initialization
	void Start () {
        score.text = PlayerChanges.Score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        score.text = PlayerChanges.Score.ToString();
    }
}
