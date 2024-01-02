using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed;
    public float inputDeadZone;
    public GameObject laserBlastPrefab;
    public Transform firePoint;
    public float timeBetweenShots;

    private Rigidbody2D rb;
    private Vector2 leftStickInput;
    private Vector2 rightStickInput;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gamePlaying)
        {
            GetPlayerInput();

        } else {
            leftStickInput = Vector2.zero;
            rightStickInput = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector2 curMovement = leftStickInput * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + curMovement);

        if (rightStickInput.magnitude > 0f)
        {
            Vector3 curRotation = Vector3.left * rightStickInput.x + Vector3.up * rightStickInput.y;
            Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);

            rb.SetRotation(playerRotation);
        }
    }

    private void Shoot()
    {
        canShoot = false;
        Instantiate(laserBlastPrefab, firePoint.position, transform.rotation);
        StartCoroutine(ShotCooldown());
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void GetPlayerInput()
    {
        leftStickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (leftStickInput.magnitude < inputDeadZone)
        {
            leftStickInput = Vector2.zero;
        }

        rightStickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));
        if (rightStickInput.magnitude < inputDeadZone)
        {
            rightStickInput = Vector2.zero;
        }

        if (Input.GetButton("Shoot") && canShoot)
        {
            Shoot();
        }
    }


}
