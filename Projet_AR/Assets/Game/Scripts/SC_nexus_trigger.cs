using UnityEngine;
using System.Collections;

public class SC_nexus_trigger : MonoBehaviour {


	void OnTriggerEnter(Collider collider)
	{
		SC_game_manager._instance.DecreasedLife();
		Destroy(collider.gameObject);
	}
}
