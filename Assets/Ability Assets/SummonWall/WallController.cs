using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour, IEarthInteractions
{
    float projectileTravelTime = 1.5f;
    float killTime = 5f;
    float projectileSpeed = 30f;
    public int wallHealth = 3;

    private void Start()
    {
        StartCoroutine(spawnDelay());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }

    public void Push(Transform source)
    {
        StartCoroutine(ApplyForwardForceToObject(source));
    }

    public void ApplyDamage(int damage)
    {
        wallHealth -= damage;
        if(wallHealth <= 0){
            Destroy(this.gameObject);
        }
    }

    public IEnumerator ApplyForwardForceToObject(Transform player)
    {
        StartCoroutine(killTimer());
        Vector3 targetDirection = player.TransformDirection(Vector3.forward * projectileSpeed);
        while (true)
        {
            transform.Translate( targetDirection * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    IEnumerator spawnDelay()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator killTimer()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(this.gameObject);
    }
}
