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
        Debug.Log(RandomNumber());

        int rockR = RandomNumber();
        int rockI = 0;
        if(rockI == 0)
        {
            for (rockI = 0; rockI < rockR; rockI++)
            {
                position.y += 1;
                position.z = RandomNumber();
                Instantiate(rock, position, Quaternion.identity);
            }
        }
       
    }
    int RandomNumber()
    {
        int num = Random.Range(0, 10);
        return num;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
