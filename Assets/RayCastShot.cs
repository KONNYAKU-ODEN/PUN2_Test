using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    //public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    //[Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    [Header("GameObject")]
    public GameObject barrelLocationObj;

    PhotonView myPV;

    void Start()
    {
        myPV = GetComponent<PhotonView>();

        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPV.IsMine)
        {
            //If you want a different input, change it here
            if (Input.GetButtonDown("Fire1"))
            {
                //Calls animation on the gun that has the relevant animation events that will fire
                gunAnimator.SetTrigger("Fire");

                //Shoot();
                //CasingRelease();

                // Create Ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                // If Ray hit something
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        //PhotonNetwork.Destroy(hit.collider.gameObject);

                        //GameObject enemy = hit.collider.gameObject;
                        //PhotonView photonView = enemy.GetComponent<PhotonView>();

                        //if (!photonView.IsMine)
                        //{
                        //    photonView.RequestOwnership();
                        //}
                        //PhotonNetwork.Destroy(hit.collider.gameObject);

                        //hit.collider.GetComponent<EnemyHP>().DecreaseEnemyHP(10);
                        myPV.RPC(nameof(SendDamage), RpcTarget.AllViaServer, 10, hit.collider.gameObject.name);
                    }                   

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2, false);
                    Debug.Log(hit);
                }
            }
        }
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        Debug.Log("Shot");
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = PhotonNetwork.Instantiate(muzzleFlashPrefab.name, barrelLocation.position, barrelLocation.rotation);
            tempFlash.transform.parent = barrelLocationObj.transform;

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);

        }
        ////cancels if there's no bullet prefeb
        //if (!bulletPrefab)
        //{ return; }

        //// Create a bullet and add force on it in direction of the barrel
        ////Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        //PhotonNetwork.Instantiate(bulletPrefab.name, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        //tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        tempCasing = PhotonNetwork.Instantiate(casingPrefab.name, casingExitLocation.position, casingExitLocation.rotation);
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    [PunRPC]
    public void SendDamage(int Damage, string EnemyName)
    {
        GameObject.Find(EnemyName).GetComponent<EnemyHP>().DecreaseEnemyHP(Damage);
    }
}
