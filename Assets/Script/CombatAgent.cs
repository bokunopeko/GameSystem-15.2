using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAgent : MonoBehaviour
{
    //private = no other class can see this 
    // protect = only child classes can see this

    [SerializeField] protected float healthCurrent;
    [SerializeField] protected float healthMax;


  public void TakeDamage(float damage)
    {
        healthCurrent -= damage;

        if (healthCurrent <= 0 )
        {
            EndOfLife();
                
        }

    }

  public void Heal(float heal )
    {
        healthCurrent = Mathf.Clamp( healthCurrent + heal , 0, healthMax);
    }

    //absttract method - define what the method shpould do for each child class
    protected abstract void EndOfLife();
  
}
