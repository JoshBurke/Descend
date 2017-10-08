using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Should be called whenever the player's health is changed.
/// </summary>
public delegate void HealthChangedEventHandler(float newHealth);
/// <summary>
/// Should be called whenever the player's fuel is changed.
/// </summary>
public delegate void FuelChangedEventHandler(float newFuel);
/// <summary>
/// Should be called whenever the player will be respawned. Make sure you pass the instantiated game 
/// object, as this event rebuild the link between the game object and the UI element.
/// </summary>
public delegate void WillBeRespawnedEventHandler(GameObject newCharacter);


public class BarController : MonoBehaviour
{

    public GameObject Player;

    protected PlayerController playerController;
    protected Slider slider;


	/// <summary>
    /// Initialize playerController and the slider object and connect teh Respawned event to its
    /// default responder.
    /// </summary>
    protected virtual void Awake()
	{
        playerController = Player.GetComponent<PlayerController>();
        slider = GetComponent<Slider>();

        playerController.WillBeRespawned += WillBeRespawned;
	}

	// Update is called once per frame
	void Update()
	{
			
	}

    /// <summary>
    /// Default event responder of WillBeRespawned event of the PlayerController class
    /// </summary>
    /// <returns></returns>
    /// <param name="newCharacter">New character instiated.</param>
    protected virtual void WillBeRespawned(GameObject newCharacter)
    {
        playerController = newCharacter.GetComponent<PlayerController>();
        Player = newCharacter;
    }
}
