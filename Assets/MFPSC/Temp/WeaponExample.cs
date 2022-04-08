using UnityEngine;
using System.Collections;

/// <summary>
/// This is not a functional weapon script. It just shows how to implement shooting and reloading with buttons system.
/// </summary>
public class WeaponExample : MonoBehaviour 
{
    public GameObject bulletObj;
    public int power = 50;

    [SerializeField]
    private GameObject _bulletSpawner = null;
    //[SerializeField]
    //private float _bulletSpeed = 10.0f;




    public FP_Input playerInput;

    public float shootRate = 0.15F;
    public float reloadTime = 1.0F;
    public int ammoCount = 15;

    private int ammo;
    private float delay;
    private bool reloading;

	void Start () 
    {
        ammo = ammoCount;
	}
	
	void Update () 
    {
        if(playerInput.Shoot())                         //IF SHOOT BUTTON IS PRESSED (Replace your mouse input)
            if(Time.time > delay)
                Shoot();

        if (playerInput.Reload())                        //IF RELOAD BUTTON WAS PRESSED (Replace your keyboard input)
            if (!reloading && ammoCount < ammo)
                StartCoroutine("Reload");
	}

    void Shoot()
    {
        if (ammoCount > 0)
        {
            //Debug.Log("Shoot");

            var bulletClone = Instantiate(bulletObj, _bulletSpawner.transform.position, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * power);
            //bulletClone.GetComponent<Renderer>().material.color = colorList[color];
            //bulletClone.GetComponent<Rigidbody>().velocity = _bulletSpawner.transform.forward * _bulletSpeed;

            ammoCount--;
        }
        else
            Debug.Log("Empty");

        delay = Time.time + shootRate;
    }

    IEnumerator Reload()
    {
        reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammoCount = ammo;
        Debug.Log("Reloading Complete");
        reloading = false;
    }

    void OnGUI()
    {
        GUILayout.Label("AMMO: " + ammoCount);
    }
}
