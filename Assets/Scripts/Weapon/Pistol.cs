using UnityEngine;
using System.Collections;
using System;

public class Pistol : Weapon
{
    [SerializeField]
    private int startSpeed = 50;
    [SerializeField]
    private Transform _bulletSpawnerPoint = null;
    
    public float shootRate = 0.15F;
    public float reloadTime = 1.0F;
    public int ammoCount = 15;

    private int ammo;
    private float delay;
    private bool reloading;

    private void Awake()
    {
        if (bulletPrefab == null)
            throw new Exception("bulletPrefab is not defined");
    }

    private void Start()
    {
        ammo = ammoCount;
        shootSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerInput.Shoot())                         //IF SHOOT BUTTON IS PRESSED (Replace your mouse input)
            if (Time.time > delay)
                Shoot();

        if (playerInput.Reload())                        //IF RELOAD BUTTON WAS PRESSED (Replace your keyboard input)
            if (!reloading && ammoCount < ammo)
                StartCoroutine("Reload");
    }

    private void BulletInstantiate()
    {
        var bulletClone = Instantiate(bulletPrefab, _bulletSpawnerPoint.position, Quaternion.identity);
        bulletClone.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * startSpeed);
        colorChanger.ChangeBulletMaterialColor(bulletClone);
    }

    private void PlayShootSound()
    {
        if (shootSource != null)
            shootSource.Play();
    }

    void Shoot()
    {
        if (ammoCount > 0)
        {
            PlayShootSound();
            BulletInstantiate();

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
