using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ExplosionController : MonoBehaviour
{
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(DestroyExplosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyExplosion() {
        AnimatorStateInfo explosionState = myAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(explosionState.length);
        Destroy(gameObject);
    }
}
