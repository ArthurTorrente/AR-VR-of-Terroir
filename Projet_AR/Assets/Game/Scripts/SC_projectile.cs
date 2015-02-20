using UnityEngine;
using System.Collections;

public class SC_projectile : MonoBehaviour {

	[SerializeField]
	private float _f_speed = 25;
	[SerializeField]
	private GameObject _GO_explosion;
	private Transform _T_projectile;


	void Start ()
	{
		_T_projectile = transform;
		Destroy(gameObject, 30);
	}
	
	void Update ()
	{
		_T_projectile.Translate(0, 0, _f_speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider)
	{
		collider.SendMessage("DealDamage", 3, SendMessageOptions.DontRequireReceiver);
		GameObject GO_tmp = Instantiate(_GO_explosion, _T_projectile.position, Quaternion.identity) as GameObject;
		if (_T_projectile.parent != null)
			GO_tmp.transform.parent = _T_projectile.parent;
		Destroy(gameObject);
	}
}
