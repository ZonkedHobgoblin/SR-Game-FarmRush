using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerControls controls;
    private GameObject projectilePrefab;
    private GameObject defensePrefab;
    private GameObject ghostedDefensePrefab;
    private bool isDefenseCooldown;
    private bool isPlayerCooldown;
    private float playerCooldown;
    private float playerHorizontalInput = 0f;
    private float playerSpeed;
    private float playerFireRate = 0.5f;

    private void Awake()
    {
        // Load our assets
        projectilePrefab = Resources.Load("Prefabs/ProjectilePrefab") as GameObject;
        defensePrefab = Resources.Load("Prefabs/DefensePrefab") as GameObject;
        ghostedDefensePrefab = Resources.Load("Prefabs/GhostedDefensePrefab") as GameObject;

        // Load our control map
        controls = new PlayerControls();
        controls.DefaultMap.Fire.performed += context => StartFiring();
        controls.DefaultMap.Fire.canceled += context => StopFiring();
        controls.DefaultMap.Horizontal.performed += context => SetInput(context.ReadValue<float>());
        controls.DefaultMap.Horizontal.canceled += context => SetInput(context.ReadValue<float>());
    }

    // Enable/Disable our controls when used/not used
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    //////////////////////////////////////////////////////////
    // Variable call methods
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
        cooldown = Mathf.Clamp01(cooldown);
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

    public void SetPlayerFireRate(float rate)
    {
        playerFireRate = rate;
    }

    public float GetPlayerFireRate()
    {
        return playerFireRate;
    }



    private void SetInput(float input)
    {
        playerHorizontalInput = input;
    }
    //////////////////////////////////////////////////////////

    private void Start()
    {
        InvokeRepeating("LowerCooldown", 0, 0.2f);
    }

    private void AddMovement(float speed)
    {
        transform.Translate(Vector3.right * playerHorizontalInput * Time.deltaTime * speed);
    }

    private void ClampPlayerPos(float beginningPos, float endingPos)
    {
        // Clamps our X axis with the two specified values
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, beginningPos, endingPos), transform.position.y, transform.position.z);
    }

    private void StartFiring()
    {
        // When StartFiring is triggered, our FireProjectile method should constantly repeat every second value given in playerFireRate
        InvokeRepeating("FireProjectile", 0, playerFireRate);
    }

    private void StopFiring()
    {
        // When StopFiring is triggered, it'll stop our invoke repeating. Should be toggeled on key raise/input canceled
        CancelInvoke("FireProjectile");
    }
    private void FireProjectile()
    {
        if (!isPlayerCooldown)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            SetCooldown(GetCooldown() + 0.1f);
            if (playerCooldown >= 1)
            {
                StartCoroutine(StartCooldown());
            }
        }
    }

    private void LowerCooldown()
    {
        if (!isPlayerCooldown)
        {
        SetCooldown(GetCooldown() - 0.1f);
        }
    }

    private IEnumerator StartCooldown()
    {
            isPlayerCooldown = true;
            yield return new WaitForSeconds(5);
            isPlayerCooldown = false;
    }

    private void Update()
    {
        // Add movement
        AddMovement(playerSpeed);
        // Clamp our location to avoid overstepping our bounds
        ClampPlayerPos(-12f, 12f);
    }









    /// <summary>
    /// Old script below this point
    /// </summary>
    private float animvalue;

    // Update is called once per frame

    void Update()
    {
        
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
