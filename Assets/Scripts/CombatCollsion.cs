using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCollsion : MonoBehaviour
{
    public static Collider colliderPunch;

    private void Start()
    {
        colliderPunch = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<EnemyController>() != null) 
        {
            Debug.Log("Tomou dano");
            collider.GetComponent<EnemyController>().Damage(1);
        }
    }
   

}
