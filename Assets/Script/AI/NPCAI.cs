using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    public static NPCAI Instace;
    [SerializeField] private float radius = 10;
    public Collider burstedCoin = null;
    private GameObject withDrawNPC = null;
    private NPCGen npcGen;
    private void Awake()
    {
        Instace = this;
    }
    private void Start()
    {
        npcGen = NPCGen.Instance;
    }
    public void WithDraw()
    {

    }
    public void GetMinDistanceNPC()
    {
        float min = 1000;
        if(npcGen.NPCs == null)
        {
            GameObject NPCPreFab = GameObject.Instantiate(npcGen.NPC, npcGen.NPCParent.transform);
            float i = Random.Range(0, 360);
            NPCPreFab.transform.position = new Vector3(10 * Mathf.Sin(i) + 10, 5.5f, 10 * Mathf.Cos(i) - 10);
            npcGen.NPCs.Add(NPCPreFab.GetComponent<Collider>());
        }
        for (int a = 0; a< npcGen.NPCs.Count;a++)
        {
            if((npcGen.NPCs[a].transform.position - burstedCoin.transform.position).magnitude < min)
            {
                min = (npcGen.NPCs[a].transform.position - burstedCoin.transform.position).magnitude;
                withDrawNPC = npcGen.NPCs[a].gameObject;
            }
        }
        Debug.Log(min);
    }
}