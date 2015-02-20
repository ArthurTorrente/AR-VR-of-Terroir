using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SC_build_manager : MonoBehaviour {

	private List<Vector3> _V3_existing_towers = new List<Vector3>();

	[SerializeField]
	private SC_game_manager _game_manager;

	[SerializeField]
	private Transform _T_camera;

	[SerializeField]
	private Transform _T_build_preview;
	
	[SerializeField]
	private GameObject _Prefab_tower;
	
	[SerializeField]
	private LayerMask _layer_mask;

	private bool _b_is_active = false;


	void Start()
	{
		_V3_existing_towers.Add(new Vector3(0, 0, 0));
		_V3_existing_towers.Add(new Vector3(0, 0, 1));
		_V3_existing_towers.Add(new Vector3(0, 0, -1));
		_V3_existing_towers.Add(new Vector3(1, 0, 0));
		_V3_existing_towers.Add(new Vector3(1, 0, 1));
		_V3_existing_towers.Add(new Vector3(1, 0, -1));
		_V3_existing_towers.Add(new Vector3(-1, 0, 0));
		_V3_existing_towers.Add(new Vector3(-1, 0, 1));
		_V3_existing_towers.Add(new Vector3(-1, 0, -1));

	}

	void Update()
	{
		if (_b_is_active)
		{
			RaycastHit _hit;
			if (Physics.Raycast(_T_camera.position, _T_camera.forward, out _hit, 1000, _layer_mask))
			{
				float f_x = _hit.point.x;
				float f_z = _hit.point.z;

				if (f_x > 0f)
					f_x += 0.5f;
				else
					f_x -= 0.5f;

				if (f_z > 0f)
					f_z += 0.5f;
				else
					f_z -= 0.5f;

				_T_build_preview.position = new Vector3((int)f_x, 0, (int)f_z);
			}
			else
			{
				_T_build_preview.position = Vector3.down;
			}
		}
	}
	
	public void SetActive(bool b_active)
	{
		_b_is_active = b_active;
		_T_build_preview.gameObject.SetActive(b_active);
		_T_build_preview.position = Vector3.down;
	}

	public void BuildTower()
	{
		if (_T_build_preview.gameObject.activeInHierarchy && _T_build_preview.position.y == 0)
		{
			if (!_V3_existing_towers.Contains(_T_build_preview.position))
			{
				if (_game_manager.SpendCredits(4))
				{
					GameObject GO_tmp = Instantiate(_Prefab_tower, _T_build_preview.position, Quaternion.identity) as GameObject;
					GO_tmp.transform.parent = transform;
					_V3_existing_towers.Add(_T_build_preview.position);
				}
			}
		}
	}
}
