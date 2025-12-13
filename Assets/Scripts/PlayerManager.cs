using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject projectilePrefab;
    private GameObject defensePrefab;
    private GameObject ghostedDefensePrefab;
    private bool isDefenseCooldown;
    private bool isPlayerCooldown;
    private float playerCooldown;
    private float playerHorizontalInput;
    private float playerSpeed;

    private void Awake()
    {
        // Load our assets
        projectilePrefab = Resources.Load("Prefabs/ProjectilePrefab") as GameObject;
        defensePrefab = Resources.Load("Prefabs/DefensePrefab") as GameObject;
        ghostedDefensePrefab = Resources.Load("Prefabs/GhostedDefensePrefab") as GameObject;
    }



    // Variable calls
    public bool GetDefenseCooldown()
    {
        return isDefenseCooldown;
    }

    public void SetDefenseCooldown(bool cooldown)
    {
        isDefenseCooldown = cooldown;
    }
    public float GetCooldown()
    {
        return playerCooldown;
    }

    public bool GetIsPlayerCooldown()
    {
        return isPlayerCooldown;
    }

    public void SetCooldown(float cooldown)
    {
        playerCooldown = cooldown;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
    public void SetPlayerSpeed(float speed)
    {
        playerSpeed = speed;
    }
//////////////////////////////////////////////////////////

    private void AddMovement(float speed)
    {
        transform.Translate(Vector3.right * playerHorizontalInput * Time.deltaTime * speed);
    }

    private void ClampPlayerPos(float beginningPos, float endingPos)
    {
        // Clamps our X axis with the two specified values
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, beginningPos, endingPos), transform.position.y, transform.position.z);
    }

    private void FireProjectile()
    {
        Instantiate(projectilePrefab, transform.position, new Quaternion(0,0,0,0));
    }
    private void Update()
    {
        // Update our input
        playerHorizontalInput = Input.GetAxis("Horizontal");
        // Add movement
        AddMovement(playerSpeed);
        // Clamp our location to avoid overstepping our bounds
        ClampPlayerPos(-12f, 12f);
    }










    private float animvalue;

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
    //IEnumerator FireProjectile()
    {
        isShooting = true;
        cooldown = (cooldown + 0.1f);
        Instantiate(projectile, transform.position, ProjectileScript.transform.rotation);

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
