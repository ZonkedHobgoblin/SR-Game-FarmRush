using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    private ObjectReferenceManager objectReferenceManager;
    private PlayerControls controls;
    private GameObject projectilePrefab;
    private GameObject defensePrefab;
    private GameObject ghostedDefensePrefab;
    private GameObject runtimeGhostDefensePrefab;
    private bool isDefenseCooldown;
    private bool isGhostedDefense;
    private bool isPlayerCooldown;
    private bool canPlayerFire;
    private float playerCooldown;
    private float playerHorizontalInput = 0f;
    private float playerSpeed = 10;
    private float playerFireRate = 0.25f;
    private float defenseCooldownTime = 25f;

    private void Awake()
    {
        // Load our assets
        projectilePrefab = Resources.Load("Prefabs/ProjectilePrefab") as GameObject;
        defensePrefab = Resources.Load("Prefabs/DefensePrefab") as GameObject;
        ghostedDefensePrefab = Resources.Load("Prefabs/GhostedDefensePrefab") as GameObject;

        // Get our Object Reference Manager
        objectReferenceManager = FindFirstObjectByType<ObjectReferenceManager>();

        // Load our control map
        controls = new PlayerControls();
        controls.DefaultMap.SpawnDefense.performed += context => GhostDefense();
        controls.DefaultMap.SpawnDefense.canceled += context => SpawnDefense();
        controls.DefaultMap.Fire.performed += context => StartFiring();
        controls.DefaultMap.Fire.canceled += context => StopFiring();
        controls.DefaultMap.Horizontal.performed += context => SetInput(context.ReadValue<float>());
        controls.DefaultMap.Horizontal.canceled += context => SetInput(0);
        controls.DefaultMap.Pause.performed += context => objectReferenceManager.gameBehaviourManager.TogglePauseMenu();
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

    public bool GetCanPlayerFire()
    {
        return canPlayerFire;
    }

    public void SetCanPlayerFire(bool canFire)
    {
        canPlayerFire = canFire;
    }

    public void SetPlayerDefenseCooldownTime(float time)
    {
        defenseCooldownTime = time;
    }

    public float GetPlayerDefenseCooldownTime()
    {
        return defenseCooldownTime;
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
        if (canPlayerFire)
        {
            // When StartFiring is triggered, our FireProjectile method should constantly repeat every second value given in playerFireRate
            InvokeRepeating("FireProjectile", 0, playerFireRate);

            // Stop the cooldown so we don't cooldown while shooting
            CancelInvoke("LowerCooldown");
        }
    }

    private void StopFiring()
    {
        // When StopFiring is triggered, it'll stop our invoke repeating. Should be toggeled on key raise/input canceled
        CancelInvoke("FireProjectile");

        if (!IsInvoking("LowerCooldown"))
        {
            // A small delay (1f) before cooling starts
            InvokeRepeating("LowerCooldown", 0.1f, 0.2f);
        }
    }
    private void FireProjectile()
    {
        if (!isPlayerCooldown)
        {
            objectReferenceManager.audioManager.PlayFireSound();
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

    private void GhostDefense()
    {
        if (!isDefenseCooldown)
        {
            isGhostedDefense = true;
            runtimeGhostDefensePrefab = Instantiate(ghostedDefensePrefab, new Vector3(transform.position.x, transform.position.y, 4f), Quaternion.identity);
        }
    }

    private void SpawnDefense()
    {
        if (isGhostedDefense)
        {
            StartCoroutine(StartDefenseCooldown());
            isGhostedDefense = false;
            Destroy(runtimeGhostDefensePrefab);
            Instantiate(defensePrefab, new Vector3(transform.position.x, transform.position.y, 6f), Quaternion.identity);
        }
    }

    private IEnumerator StartDefenseCooldown()
    {
        isDefenseCooldown = true;
        yield return new WaitForSeconds(defenseCooldownTime);
        isDefenseCooldown = false;
    }

    private IEnumerator StartCooldown()
    {
        isPlayerCooldown = true;
        yield return new WaitForSeconds(3);
        isPlayerCooldown = false;
    }


    private void Update()
    {
        // Add movement
        AddMovement(playerSpeed);
        // Clamp our location to avoid overstepping our bounds
        ClampPlayerPos(-12f, 12f);

        // Set ghost defense pos
        if (isGhostedDefense)
        {
            runtimeGhostDefensePrefab.transform.position = new Vector3(transform.position.x, transform.position.y, 6f);
        }
    }
    
}
