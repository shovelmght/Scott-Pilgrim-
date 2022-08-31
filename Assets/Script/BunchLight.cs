using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunchLight : MonoBehaviour
{
    public GameObject lightP;
    private int nbrLight = 0;
    public int maxLight = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nbrLight < maxLight)
        {
            Instantiate(lightP, transform.position, Quaternion.identity);
            Instantiate(lightP, transform.position, Quaternion.identity);
            nbrLight++;
        }
      
    }
}
