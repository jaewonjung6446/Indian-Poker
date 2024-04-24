using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICon : MonoBehaviour
{
    public static UICon instance;
    public GameObject CardDescription;
    private void Start()
    {
        instance = this;
    }
}
