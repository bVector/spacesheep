using UnityEngine;
using System.Collections;

public class LaserBlast : MonoBehaviour {
    public float impulse = 50;
    public float lifetime = 5f;
    public int damage = 5;
    private Rigidbody2D body;
	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void OnFire(Vector2 vel)
    {
        vel += new Vector2(0, impulse);
        body.AddRelativeForce(vel);
    }

	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) {
            OnKill();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.ToString());
        foreach (Block block in other.gameObject.GetComponents<Block>())
        {
            block.health -= damage;
            //block.GetComponentInParent<Rigidbody2D>().AddForce(transform.TransformVector(body.velocity*100f), ForceMode2D.Force);
            OnKill();

        }
    }

    void OnKill()
    {
        GetComponent<Animator>().SetBool("isDead", true);
        GetComponent<ParticleSystem>().Emit(2000);
        body.velocity *= .1f;
    }
    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
