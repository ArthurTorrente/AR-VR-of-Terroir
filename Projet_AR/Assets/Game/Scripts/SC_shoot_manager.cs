using UnityEngine;
using System.Collections;

public class SC_shoot_manager : MonoBehaviour {

	[SerializeField]
	private Transform _T_camera;

	[SerializeField]
	private GameObject _Prefab_projectile;

	private bool _b_is_reloaded = true;
	private float _f_timer_reload = 0f;


	public void ShootProjectile()
	{
		if (_b_is_reloaded)
		{
			GameObject GO_tmp = Instantiate(_Prefab_projectile, _T_camera.position, Quaternion.LookRotation(_T_camera.forward)) as GameObject;
			GO_tmp.transform.parent = transform;
			_b_is_reloaded = false;
		}
	}

	void Update()
	{
		if (!_b_is_reloaded)
		{
			_f_timer_reload += Time.deltaTime;
			if (_f_timer_reload >= 0.4f)
			{
				_b_is_reloaded = true;
				_f_timer_reload = 0f;
			}
		}
	}
}
