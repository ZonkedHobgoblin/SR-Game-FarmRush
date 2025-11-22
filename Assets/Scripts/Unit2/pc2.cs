using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pc2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float horizontalInput;
    public float speed = 10.0f;
    public float Range = 10;
    public GameObject projectile;
    private bool isShooting = false;
    void Update()
    {
        //Movement stuff VVVVVVVVVVVVVVVVV
        //input and movement
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        //clamp x axis because using if statements for this is stupid
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), transform.position.y, transform.position.z);
        
        //projectile stuff VVVVVVVVVVVV
        if (Input.GetKey(KeyCode.Space) && !isShooting)
        {
            //call the shoot thing
            StartCoroutine(FireProjectile());
        }
    }
    //cooldown between shots
    IEnumerator FireProjectile()
    {
        isShooting = true;
        Instantiate(projectile, transform.position, projectile.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        isShooting = false;

    }
}
