using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShipMap : MonoBehaviour
{

    struct Int2
    {
        public int x;
        public int y;
        public Int2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public override string ToString()
        {
            return string.Format("[{0}, {1}]", x, y);
        }
    }

    public int BlockCount
    {
        get { return blocks.Count; }
    }

    Dictionary<Int2, Block> blocks = new Dictionary<Int2, Block>();

    public bool AddBlock(int x, int y, Block block)
    {
        if (blocks.ContainsKey(new Int2(x, y)) || blocks.ContainsValue(block))
        {
            return false;
        }
        blocks.Add(new Int2(x, y), block);
        Debug.Log(blocks[new Int2(x, y)].ToString());
        return true;
    }

    public Block GetBlock(int x, int y)
    {
        Int2 coord = new Int2(x, y);
        if (blocks.ContainsKey(coord)) return blocks[coord];
        return null;
    }

    public bool RemoveBlock(int x, int y)
    {
        Int2 coord = new Int2(x, y);
        if (blocks.ContainsKey(coord))
        {
            blocks.Remove(coord);
            return true;
        }
        return false;
    }

    public void Rebuild(Cpu _CPU)
    {
        blocks.Clear();
        AddBlock(0, 0, _CPU);
        Stack<Block> todo = new Stack<Block>();
        todo.Push(_CPU);
        Block b;
        while (todo.Count > 0)
        {
            b = todo.Pop();
            if (b == null) break;
            foreach (Edge e in b.Edges)
            {
                for (int bidx = 0; bidx < 2; bidx++)
                {
                    if (!blocks.ContainsValue(e.Blocks[1]))
                    {
                        Vector3 pos = _CPU.transform.InverseTransformPoint(e.Blocks[bidx].transform.position);
                        AddBlock((int)(pos.x + 0.5f), (int)(pos.y + 0.5f), e.Blocks[bidx]);
                        todo.Push(e.Blocks[bidx]);
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
