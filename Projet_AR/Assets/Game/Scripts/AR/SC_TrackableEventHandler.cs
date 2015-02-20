/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class SC_TrackableEventHandler : MonoBehaviour,
ITrackableEventHandler
{
	#region PRIVATE_MEMBER_VARIABLES
	
	private TrackableBehaviour mTrackableBehaviour;
	[SerializeField]
	private GameObject _GO_root_game;
	[SerializeField]
	private SC_game_manager _game_manager;

	#endregion // PRIVATE_MEMBER_VARIABLES
	
	
	
	#region UNTIY_MONOBEHAVIOUR_METHODS
	
	void Start()
	{
		_GO_root_game.SetActive(false);
		Time.timeScale = 0;

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}
	
	#endregion // UNTIY_MONOBEHAVIOUR_METHODS
	
	
	
	#region PUBLIC_METHODS
	
	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
	
	#endregion // PUBLIC_METHODS
	
	
	
	#region PRIVATE_METHODS
	
	
	private void OnTrackingFound()
	{
//		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
//		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
//		
//		// Enable rendering:
//		foreach (Renderer component in rendererComponents)
//		{
//			component.enabled = true;
//		}
//		
//		// Enable colliders:
//		foreach (Collider component in colliderComponents)
//		{
//			component.enabled = true;
//		}

		_game_manager.ActiveGame();

		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
	}
	
	
	private void OnTrackingLost()
	{
//		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
//		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
//		
//		// Disable rendering:
//		foreach (Renderer component in rendererComponents)
//		{
//			component.enabled = false;
//		}
//		
//		// Disable colliders:
//		foreach (Collider component in colliderComponents)
//		{
//			component.enabled = false;
//		}

		_game_manager.UnactiveGame();

		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
	}
	
	#endregion // PRIVATE_METHODS
}
