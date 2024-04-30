using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    public static NPCAI Instace;
    [SerializeField] private float radius = 10;
    [SerializeField] private int GenNPCNum = 15;
    [SerializeField] private GameObject NPC;
    [SerializeField] private GameObject NPCParent;
    public Collider burstedCoin = null;
    public List<Collider> NPCs = new List<Collider>();
    private GameObject withDrawNPC = null;
    private void Awake()
    {
        Instace = this;
    }
    private void Start()
    {
        GenNPCs();
    }

    public void WithDraw()
    {

    }
    public void GetMinDistanceNPC()
    {
        float min = 1000;
        if(NPCs == null)
        {
            GameObject NPCPreFab = GameObject.Instantiate(NPC, NPCParent.transform);
            float i = Random.Range(0, 360);
            NPCPreFab.transform.position = new Vector3(10 * Mathf.Sin(i) + 10, 5.5f, 10 * Mathf.Cos(i) - 10);
            NPCs.Add(NPCPreFab.GetComponent<Collider>());
        }
        for (int a = 0; a<NPCs.Count;a++)
        {
            if((NPCs[a].transform.position - burstedCoin.transform.position).magnitude < min)
            {
                min = (NPCs[a].transform.position - burstedCoin.transform.position).magnitude;
                withDrawNPC = NPCs[a].gameObject;
            }
        }
        Debug.Log(min);
    }
    private void GenNPCs()
    {
        for (int a = 0; a < GenNPCNum; a++)
        {
            GameObject NPCPreFab = GameObject.Instantiate(NPC, NPCParent.transform);
            float i = Random.Range(0, 360);
            NPCPreFab.transform.position = new Vector3(10 * Mathf.Sin(i) + 10, 5.5f, 10 * Mathf.Cos(i) - 10);
            NPCs.Add(NPCPreFab.GetComponent<Collider>());
        }

    }
}