using UnityEngine;
using System.Collections;

public class SC_explosion : MonoBehaviour {
	
	[SerializeField]
	private Transform _T_graphic;

	[SerializeField]
	private float _f_radius = 1.5f;


	IEnumerator Start()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, _f_radius);
		for(int i = 0; i < colliders.Length; ++i)
		{
			colliders[i].SendMessage("DealDamage", 2, SendMessageOptions.DontRequireReceiver);
		}
		yield return StartCoroutine(AnimationVisual());
		Destroy(gameObject);
	}

	private IEnumerator AnimationVisual()
	{
		Material Mat_graphic = _T_graphic.renderer.material;
		for(float f_time = 0f; f_time < 0.5f; f_time += Time.deltaTime)
		{
			_T_graphic.localScale = Vector3.one * _f_radius * f_time * 4;
			Mat_graphic.color = new Color(1f, 0.5f, 0f, 1f - f_time*2f);
			yield return null;
		}
	}
}
