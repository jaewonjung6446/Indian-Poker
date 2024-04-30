using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGen : MonoBehaviour
{
    [SerializeField] private int GenNPCNum = 15;
    public GameObject NPC;
    public GameObject NPCParent;
    public static NPCGen Instance;
    public List<Collider> NPCs = new List<Collider>();
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GenNPCs();
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
