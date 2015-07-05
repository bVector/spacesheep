using UnityEngine;
using System.Collections;

public class CPU : Block {
	public int credits;
    private Rigidbody2D body;
    public GameObject ArmorPrefab;
    public GameObject EdgePrefab;
	private ShipMap map;

	// Use this for initialization
	void Awake(){
	}

    void Start()
    {
		body = GetComponent<Rigidbody2D> ();
        map = GetComponent<ShipMap>();
        map.Rebuild(this);


        CreateArmor(1, 0);
        CreateArmor(0, 1);
        CreateArmor(-1, 0);
	}
	
	// Update is called once per frame
    void Update()
    {
        CalculateInertialTensor();
    }

    void CalculateInertialTensor()
    {
        Vector3 CenterOfMass = new Vector3(0, 0, 0);
        Vector3 origin = transform.position;
        float Mass = 0f;
        foreach (var block in gameObject.GetComponentsInChildren<Block>())
        {
            Mass += block.Mass;
            CenterOfMass += block.transform.position * block.Mass;
        }
        CenterOfMass /= Mass;
        
        float MOI = 0f;
        foreach (var block in gameObject.GetComponentsInChildren<Block>())
        {
            Vector3 d = CenterOfMass - block.transform.position;
            MOI += d.sqrMagnitude * block.Mass;
        }

        body.centerOfMass = transform.InverseTransformPoint(CenterOfMass);
        if (MOI == 0)
        {
            //MOI = Mass;
        }
        body.inertia = MOI;
        body.mass = Mass;
        Debug.DrawLine(transform.position, transform.TransformPoint(body.centerOfMass), Color.red);
        Debug.DrawLine(transform.TransformPoint(body.centerOfMass) + (Vector3.up * 0.2f), transform.TransformPoint(body.centerOfMass) + (Vector3.down * 0.2f), Color.red);
        GetComponentInChildren<TextMesh>().text = "MOI:" + body.inertia.ToString() + " mass:" + body.mass.ToString();
    }

	void FixedUpdate() {
		float x = Input.GetAxis ("Horizontal") * 3f;
		float y = Input.GetAxis ("Vertical") * 3f;
		body.AddRelativeForce (new Vector2(0,y),ForceMode2D.Force);
		body.AddTorque (-x*10);
	}



    bool CreateArmor(int x, int y)
    {
        if (map.GetBlock(x, y) != null)
            return false;

        Block[] blocks = new Block[4];

        blocks[0] = map.GetBlock(x - 1, y);
        blocks[1] = map.GetBlock(x + 1, y);
        blocks[2] = map.GetBlock(x, y - 1);
        blocks[3] = map.GetBlock(x, y + 1);

        if (blocks[0] == null && blocks[1] == null && blocks[2] == null && blocks[3] == null) return false;

        Vector3 pos = transform.TransformPoint(new Vector3(x, y, 0.0f));

        GameObject tobj = (GameObject)Instantiate(ArmorPrefab, pos, GetComponent<Rigidbody2D>().transform.rotation);

        tobj.transform.parent = transform;

        GameObject NewArmor = (GameObject)tobj;

        Armor armor = NewArmor.GetComponent<Armor>();

        bool createsuccess = map.AddBlock(x, y, armor);
        if (!createsuccess)
            throw null;
        /*
        foreach (Block block in blocks)
        {
            if (block == null)
                continue;

            float mx = block.transform.position.x + pos.x; mx /= 2;
            float my = block.transform.position.y + pos.y; my /= 2;
            float mz = block.transform.position.z + pos.z; mz /= 2;

            


            GameObject NewEdge = (GameObject)Instantiate(EdgePrefab, new Vector3(mx, my, mz), quat);
            
            var armorscript = NewArmor.GetComponent<Armor>();
            var edgeblocks = NewEdge.GetComponent<Edge>();
            NewEdge.GetComponent<Edge>().AddBlock(block);
            NewEdge.GetComponent<Edge>().AddBlock(NewArmor.GetComponent<Armor>());
            HingeJoint2D[] hinges = NewEdge.GetComponents<HingeJoint2D>();
            Debug.Log(hinges.ToString());
            //hinges[1].connectedBody = NewArmor.GetComponent<Rigidbody2D>();
            //hinges[0].connectedBody = block.GetComponent<Rigidbody2D>();
            //Debug.Log (hinges[1].connectedBody.ToString());

            map.Rebuild(this);
            Debug.Log(map.BlockCount);
            //edgehinge.

        }
        */
        return true;
    }

}
