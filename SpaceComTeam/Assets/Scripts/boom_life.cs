using UnityEngine;
using System.Collections;

public class boom_life : MonoBehaviour {

    public float lifetime_b = 0.25f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime_b);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
