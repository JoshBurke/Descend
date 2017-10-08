using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarController : BarController 
{

	// Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        playerController.FuelChanged += FuelChanged;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void FuelChanged (float newHealth) 
    {
        slider.value = newHealth;
    }
}
