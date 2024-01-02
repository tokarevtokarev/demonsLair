using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EnemieShooterController : MonoBehaviour
{
    public int hitPoint;

    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float shootingInterval = 1f;
    private float timeUntilNextShot;
    public GameObject player;


    private Animator myAnimator;
    private AudioSource hitSound;
    private AudioClip deathSound;

    // In der Start-Methode können wir die Referenzen initialisieren
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        hitSound = GetComponent<AudioSource>();
        deathSound = Resources.Load<AudioClip>("Audio/die");
        timeUntilNextShot = 0f;
    }

    // In der Update-Methode können wir dann den Transform des Gegners ändern, um ihn zum Transform des Spielers zu bewegen
    void Update()
    {
        if (timeUntilNextShot > 0)
        {
            timeUntilNextShot -= Time.deltaTime;
        }
        else
        {
            timeUntilNextShot = shootingInterval;
            Vector2 direction = (player.transform.position - spawnPoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<LaserBlastController>().speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);        
        }
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
