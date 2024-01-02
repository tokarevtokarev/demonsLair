using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EnemieController : MonoBehaviour
{
    public int hitPoint;

    private Animator myAnimator;
    private AudioSource hitSound;
    private AudioClip deathSound;

    // In der Start-Methode können wir die Referenzen initialisieren
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        hitSound = GetComponent<AudioSource>();
        deathSound = Resources.Load<AudioClip>("Audio/die");
    }

    // In der Update-Methode können wir dann den Transform des Gegners ändern, um ihn zum Transform des Spielers zu bewegen
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hitPoint--;
            if (hitPoint <= 0)
            {
                hitSound.clip = deathSound;
                StartCoroutine(KillDemon());
            }
            hitSound.Play();
        }
    }

    IEnumerator KillDemon()
    {
        GetComponent<Collider2D>().enabled = false;
        GameController.instance.SlayDemon();
        myAnimator.SetTrigger("Kill Demon");
        AnimatorStateInfo deathAnimState = myAnimator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(deathAnimState.length);
        Destroy(gameObject);
    }

}
