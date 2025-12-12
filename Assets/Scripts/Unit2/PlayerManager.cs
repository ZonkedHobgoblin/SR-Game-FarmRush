using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class pc2 : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.0f;
    public GameObject projectile;
    public GameObject defense;
    public GameObject ghostDefensePrefab;
    private GameObject ghostDefense;
    private bool isShooting = false;
    private float animvalue;
    public float cooldown = 0;
    public bool isCooldown = false;
    private bool isCoolingdown = false;
    public bool defenseCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        //Movement stuff VVVVVVVVVVVVVVVVV
        //input and movement
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        //clamp x axis because using if statements for this is stupid
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -12f, 12f), transform.position.y, transform.position.z);
        
        //projectile stuff VVVVVVVVVVVV
        if (Input.GetKey(KeyCode.Space) && !isShooting && !isCooldown)
        {
            //call the shoot thing
            StartCoroutine(FireProjectile());
        }
        else if (!isShooting && !isCooldown && !isCoolingdown)
        {
            StartCoroutine(LowerCooldown());
        }
        if ((cooldown == 1) && !isCooldown)
        {
            StartCoroutine(Cooldown());
        }

        //spawn defense
        if (Input.GetKey(KeyCode.E) && !defenseCooldown)
        {
            if (!ghostDefense)
            {
                ghostDefense = Instantiate(ghostDefensePrefab, new Vector3(transform.position.x, transform.position.y, (transform.position.z + 8)), ghostDefensePrefab.transform.rotation);
            }
            else
            {
                ghostDefense.transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + 8));
            }

        }
        else if (ghostDefense != null)
        {
            Object.Destroy(ghostDefense);
            Instantiate(defense, new Vector3(transform.position.x, transform.position.y, (transform.position.z + 8)), defense.transform.rotation);
            StartCoroutine(DefenseCooldown());
        }


        //anims
        if (horizontalInput < 0)
        {
            animvalue = (horizontalInput * -1f);
        }
        else
        {
            animvalue = horizontalInput;
        }
        //clamp cooldown
        cooldown = (Mathf.Clamp(cooldown, 0, 1));
        GetComponent<Animator>().SetFloat("Speed_f", animvalue);
    }
    //cooldown between shots
    IEnumerator FireProjectile()
    {
        isShooting = true;
        cooldown = (cooldown + 0.1f);
        Instantiate(projectile, transform.position, projectile.transform.rotation);

        yield return new WaitForSeconds(0.2f);
        isShooting = false;
    }
    IEnumerator LowerCooldown()
    {
        isCoolingdown = true;
        cooldown = (cooldown - 0.06f);
        yield return new WaitForSeconds(0.05f);
        isCoolingdown = false;
    }
    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(2.0f);
        cooldown = 0.9f;
        isCooldown = false;
    }
    IEnumerator DefenseCooldown()
    {
        defenseCooldown = true;
        yield return new WaitForSeconds(30.0f);
        defenseCooldown = false;
    }
    void SpawnDefense()
    {
        defenseCooldown = true;
        Instantiate(defense, new Vector3(transform.position.x, transform.position.y, (transform.position.z + 8)), defense.transform.rotation);
    }
}
