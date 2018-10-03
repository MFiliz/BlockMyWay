using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        //foreach (GameObject enemy in enemies)
        //{
        //    if (enemy.GetComponent<Rigidbody>().velocity.magnitude > 0)
        //    {
        //        var que =Quaternion.FromToRotation(enemy.transform.position, enemy.GetComponent<Rigidbody>().velocity);
        //        foreach (GameObject subenemy in enemies)
        //        {
        //            Debug.DrawRay(subenemy.transform.position, que.eulerAngles, Color.green);
        //        }
                
        //    }
        //}


        //RaycastHit deneme;
        //Debug.DrawRay(transform.localPosition, new Vector3(0, 0, 10), Color.green);
        //if (Physics.Raycast(transform.localPosition, new Vector3(0, 0, 10), out deneme))
        //{
        //    if (deneme.rigidbody.velocity.z > 0)
        //    {
        //        Debug.Log(deneme.rigidbody.velocity);
        //    }
        //}


        //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            YeniMovement.EnemyCanMove = false;
        }

    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }





}
