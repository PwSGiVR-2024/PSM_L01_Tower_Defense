using System;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
   public Camera cam;
   public NavMeshAgent agent;
   private GameObject[] spawnSpots;
   private GameObject target;

   private void Start()
   {
       target = GameObject.FindGameObjectWithTag("TARGET");
       
       agent.destination = target.transform.position;
 
   }

   void OnTriggerEnter(Collider other)
   {
       if (other.tag == "TARGET")
       {
           Destroy(this.gameObject);
       }
   }
}