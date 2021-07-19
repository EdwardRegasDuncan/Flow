using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWall : MonoBehaviour
{

    public GameObject prefab;
    float summonSpeed = 5f;
    Vector3 offset = new Vector3(0, -1.5f, 3);
    Animator _animator;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!active)
            {
                StartCoroutine(SummonAction());
            }
            
        }
    }

    IEnumerator LiftWall(GameObject earthObject){
        Vector3 targetPosition = earthObject.transform.position + new Vector3(0, -earthObject.transform.position.y * 2, 0);
        Debug.Log(targetPosition);
        while (Vector3.Distance(earthObject.transform.position, targetPosition) > 0){
            earthObject.transform.position = Vector3.MoveTowards(earthObject.transform.position, targetPosition, summonSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SummonAction()
    {
        active = true;
        _animator.SetTrigger("Summon");
        yield return new WaitForSeconds(1.5f);
        GameObject earthObject = CreateObject();
        StartCoroutine(LiftWall(earthObject));

        _animator.ResetTrigger("Summon");
        active = false;
    }

    GameObject CreateObject()
    {
        Vector3 spawnLocation = transform.TransformPoint(offset);
        GameObject earthObject = Instantiate(prefab, spawnLocation, transform.rotation);
        return earthObject;
    }
}
