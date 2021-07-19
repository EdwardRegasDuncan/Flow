using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRock : MonoBehaviour
{

    public GameObject rockPrefab;
    float power = 350f;
    Vector3 offset = new Vector3(0, 0, 3);
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!active)
            {
                StartCoroutine(SummonAction());
            }
            
        }
    }

    private void ApplyForce(GameObject rock)
    {
        rock.GetComponent<Rigidbody>().AddForce(Vector3.up * power);
    }

    IEnumerator SummonAction()
    {
        active = true;
        _animator.SetTrigger("Summon");
        yield return new WaitForSeconds(1.5f);
        GameObject rock = CreateRock();
        ApplyForce(rock);
        _animator.ResetTrigger("Summon");
        active = false;
    }

    GameObject CreateRock()
    {
        Vector3 spawnLocation = transform.TransformPoint(offset);
        GameObject rock = Instantiate(rockPrefab, spawnLocation, Quaternion.identity);
        return rock;
    }
}
