using UnityEngine;
using System.Collections;

public class Gun : Block {
    public float firedelay = 1f;
    public bool canfire = true;
    public GameObject laserPrefab;
    private float firedelta;

	// Use this for initialization
	void Start () {
        firedelta = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FixedUpdate()
    {
        base.FixedUpdate();
        firedelta += Time.deltaTime;
        if (firedelay > 2f){ firedelay = 2f;}
        //OnFire();
    }

    public void OnFire()
    {
        if (firedelta > firedelay)
        {
            firedelta -= 0.5f;
            GameObject laser = (GameObject)Instantiate(laserPrefab, transform.position, transform.rotation);
            var laservel = GetComponentInParent<Rigidbody2D>().velocity;
            laser.GetComponent<LaserBlast>().OnFire(laservel);
        }
        else { 
            //Debug.Log("Can't Fire "+ firedelta.ToString() + " " + firedelay.ToString()); 
        }
    }
}
