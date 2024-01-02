using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlastController : MonoBehaviour
{

    public float speed;
    public float timeToLive;
    public GameObject explosionPrefab;


    Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        moveVector = Vector3.up * speed * Time.fixedDeltaTime;
        StartCoroutine(DestroyBlast());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        transform.Translate(moveVector);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator DestroyBlast() {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
