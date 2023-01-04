using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    public GameObject body;
    public float bodyRotateSpeed;
    public GameObject barrel;

    public GameObject muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        barrel.transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 angles = body.transform.rotation.eulerAngles;
        body.transform.rotation = Quaternion.Euler(0, 0, angles.z - bodyRotateSpeed * Time.deltaTime);
    }

    public void OnBulletShot()
    {
        muzzleFlash.SetActive(true);
        Invoke("SetMuzzleFlashOff", 0.05f);
    }

    private void SetMuzzleFlashOff()
    {
        muzzleFlash.SetActive(false);
    }
}
