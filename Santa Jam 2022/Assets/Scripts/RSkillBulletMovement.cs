using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSkillBulletMovement : MonoBehaviour
{
    public Transform sprites;
    
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 angles = sprites.rotation.eulerAngles;
        sprites.rotation = Quaternion.Euler(0, 0, angles.z + rotateSpeed * Time.deltaTime);
    }
}
