using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testiguess : MonoBehaviour
{
    public GameObject rock;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(0, 2, 0);
        Instantiate(rock, position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
