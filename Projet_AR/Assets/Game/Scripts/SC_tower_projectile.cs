using UnityEngine;
using System.Collections;

public class SC_tower_projectile : MonoBehaviour {

	[SerializeField]
	private float _f_speed = 10f;

	private Transform _T_this;

	[HideInInspector]
	public Transform _T_target;


	void Start()
	{
		_T_this = transform;
	}

	void Update()
	{
		if (_T_target == null)
		{
			Destroy(gameObject);
			return;
		}

		_T_this.LookAt(_T_target.position);
		_T_this.position = Vector3.MoveTowards(_T_this.position, _T_target.position, _f_speed * Time.deltaTime);

		if (Vector3.Distance(_T_this.position, _T_target.position) < 0.5f)
		{
			_T_target.SendMessage("DealDamage", 1, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
