using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingCounter : MonoBehaviour {

    public Text rings;

	// Use this for initialization
	void Start () {
        rings.text = "" + (int)(PlayerChanges.Rings);
	}
	
	// Update is called once per frame
	void Update () {
        rings.text = "" + (int)(PlayerChanges.Rings);
    }
}
