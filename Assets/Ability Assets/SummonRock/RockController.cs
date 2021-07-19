using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour, IEarthInteractions
{
    float projectileTravelTime = 1.5f;
    float projectileSpeed = 30f;
    int damage = 1;

    public void Push(Transform source)
    {
        StartCoroutine(ApplyForwardForceToObject(source));
    }

    public void ApplyDamage(int value)
    {
        //do nothing as this projectile will not recieve damage
    }

    private void Start()
    {
        StartCoroutine(spawnDelay());
        StartCoroutine(killTimer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
        if(collision.transform.tag == "EarthManipulate")
        {
            collision.gameObject.GetComponent<IEarthInteractions>().ApplyDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public IEnumerator ApplyForwardForceToObject(Transform player)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = player.TransformDirection(new Vector3(0, 0, projectileSpeed));
        yield return new WaitForSeconds(projectileTravelTime);
        rb.useGravity = true;
        //rb.velocity = player.TransformDirection(new Vector3(0, 0, 10));
    }

    IEnumerator spawnDelay()
    {
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SphereCollider>().enabled = true;
    }

    IEnumerator killTimer()
    {
        yield return new WaitForSeconds(7f);
        Destroy(this.gameObject);
    }
}
