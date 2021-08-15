using System.Collections;
using UnityEngine;

public class BulletTravel : MonoBehaviour, IPooledObject
{
    public float speed = 20f;
    public float destroyAfter = 5f;

    public void OnObjectSpawn()
    {
        StopAllCoroutines();
        StartCoroutine(PoolAfter(destroyAfter));
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    /*private void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * speed;
    }*/

    IEnumerator PoolAfter(float _poolAfter)
    {
        yield return new WaitForSeconds(_poolAfter);
        gameObject.SetActive(false);
        yield return null;
    }
}
