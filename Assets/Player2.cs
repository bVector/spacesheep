using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("g"))
        {
            GetComponentInChildren<Gun>().OnFire();
        }
	}
    void FixedUpdate()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        float x = Input.GetAxis("Horizontal2") * 4f;
        float y = Input.GetAxis("Vertical2") * 40f;
        body.AddRelativeForce(new Vector2(0, y), ForceMode2D.Force);
        body.AddTorque(-x * 10);
    }
}
