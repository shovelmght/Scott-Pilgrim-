using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyeObj : MonoBehaviour
{
    public float time = 7;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
