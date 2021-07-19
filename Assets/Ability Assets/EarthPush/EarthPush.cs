using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPush : MonoBehaviour
{

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(PushAction());
        }
    }

    IEnumerator PushAction()
    {
        _animator.SetTrigger("Push");
        yield return new WaitForSeconds(0.3f);
        Collider[] earthObjects = GetPushableObjects();
        earthObjects = FilterEarthObjectsByPushable(earthObjects);
        ApplyForceToFilteredEarthObjects(earthObjects);
        _animator.ResetTrigger("Push");
    }

    void ApplyForceToFilteredEarthObjects(Collider[] earthObjects)
    {
        foreach(Collider earthObject in earthObjects)
        {
            earthObject.GetComponent<IEarthInteractions>().Push(transform);
        }
    }

    Collider[] FilterEarthObjectsByPushable(Collider[] earthObjects)
    {
        List<Collider> filteredObjects = new List<Collider>();
        foreach(Collider earthObject in earthObjects)
        {
            if (earthObject.transform.tag == "EarthManipulate")
            {
                filteredObjects.Add(earthObject);
            }
        }
        return filteredObjects.ToArray();
    }

    Collider[] GetPushableObjects()
    {
        Vector3 colliderBoxSize = new Vector3(3f, 3f, 3f);
        Collider[] collitions = Physics.OverlapBox(transform.position + transform.TransformDirection(new Vector3(0, 1.5f, 3)), colliderBoxSize, transform.rotation, LayerMask.GetMask("Earth"));
        Debug.Log($"Detected {collitions.Length} Earth Objects");
        Debug.Log(collitions);
        return collitions;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(transform.position + transform.TransformDirection(new Vector3(0, 3f, 3)), new Vector3(6f, 6f, 5f));
    }
}
