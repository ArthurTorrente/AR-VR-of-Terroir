using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SC_tower : MonoBehaviour {

	[SerializeField]
	private GameObject _GO_prefeb_tower_projectile;

	private List<Transform> _T_enemies = new List<Transform>();

	private bool _b_is_reloaded = true;
	private float _f_timer_reload = 0f;


	void Start()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 3.5f);
		for(int i = 0; i < colliders.Length; ++i)
		{
			SC_enemy _enemy = collider.GetComponent<SC_enemy>();
			if (_enemy != null)
				_T_enemies.Add(collider.transform);
		}
	}

	void Update()
	{
		if (Time.timeScale > 0)
		{
			List<Transform> T_enemies_to_remove = new List<Transform>();
			for (int i = 0; i < _T_enemies.Count; ++i)
			{
				if(_T_enemies[i] == null)
					T_enemies_to_remove.Add(_T_enemies[i]);
			}
			for (int i = 0; i < T_enemies_to_remove.Count; ++i)
			{
				_T_enemies.Remove(T_enemies_to_remove[i]);
			}

			if (!_b_is_reloaded)
			{
				_f_timer_reload += Time.deltaTime;
				if (_f_timer_reload >= 2f)
				{
					_b_is_reloaded = true;
					_f_timer_reload = 0f;
				}
			}

			if (_b_is_reloaded && _T_enemies.Count > 0)
			{
				GameObject GO_tmp = Instantiate(_GO_prefeb_tower_projectile, transform.position + Vector3.up * 1.25f, Quaternion.identity) as GameObject;
				GO_tmp.GetComponent<SC_tower_projectile>()._T_target = _T_enemies[0];
				GO_tmp.transform.parent = transform;
				_b_is_reloaded = false;
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		SC_enemy _enemy = collider.GetComponent<SC_enemy>();
		if (_enemy != null)
			_T_enemies.Add(collider.transform);
	}

	void OnTriggerExit(Collider collider)
	{
		_T_enemies.Remove(collider.transform);
	}
}
