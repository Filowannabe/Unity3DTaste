using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("General")]
    public GameObject bulletHolePrefap;
    public LayerMask hitableLayers;

    [Header("Shoot Parameters")]
    public float FireRange = 200;
    public float fireRate = 0.0f;
    public int maxAmmo = 8;
    private float lastTimeShoot = Mathf.NegativeInfinity;
    public int currentAmmo { get; private set; }

    private Transform cameraPlayerTransform;
    // Start is called before the first frame update
    [Header("reload")]
    public float reloadTime = 0.5f;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        //eventmanager.current.updateBulletsEvent.invoke(currentAmmo, maxAmmo);
        eventmanager.current.UpdateBulletsEvent.Invoke(currentAmmo, maxAmmo);
    }
    void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //posicion inicial ray,
            //posicion final,
            // donde se guarda,
            // rango de disparo
            //QWue capas quiero que afecte
            TryShoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

    }
    private bool TryShoot()
    {
        if (lastTimeShoot + fireRate < Time.time)
        {
            if (currentAmmo >= 1)
            {
                HandleShoot();
                currentAmmo -= 1;
                eventmanager.current.UpdateBulletsEvent.Invoke(currentAmmo, maxAmmo);
                return true;
            }
        }
        return false;
    }
    private void HandleShoot()
    {


        RaycastHit hit;
        if (Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, FireRange, hitableLayers))
        {
            GameObject bulletHoleClone = Instantiate(bulletHolePrefap, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
            Destroy(bulletHoleClone, 4f);
        }
        lastTimeShoot = Time.time;
    }
    IEnumerator Reload()
    {
        Debug.Log("char");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        eventmanager.current.UpdateBulletsEvent.Invoke(currentAmmo, maxAmmo);
        Debug.Log("chargeed");
    }
}
