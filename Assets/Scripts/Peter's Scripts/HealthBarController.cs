using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HealthBarController : BarController 
{


	// Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        playerController.HealthChanged += HealthChanged;
    }
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void HealthChanged (float newHealth) 
    {
        slider.value = newHealth;
    }
}
