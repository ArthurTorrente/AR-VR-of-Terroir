using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SC_ui_manager : MonoBehaviour {

	[SerializeField]
	private GameObject GO_ui_game;
	[SerializeField]
	private GameObject GO_ui_capture_target;
	[SerializeField]
	private GameObject GO_ui_general;
	[SerializeField]
	private GameObject GO_ui_end;

	[SerializeField]
	private GameObject GO_ui_shoot;
	[SerializeField]
	private GameObject GO_ui_build;

	[SerializeField]
	private Text _text_score;
	[SerializeField]
	private Text _text_lifes;
	[SerializeField]
	private Text _text_credits;

	[SerializeField]
	private Text _text_score_end;
	

	public void SetActiveGame(bool b_active)
	{
		GO_ui_game.SetActive(b_active);
		GO_ui_capture_target.SetActive(!b_active);
	}

	public void SetMode(bool b_mode)
	{
		GO_ui_shoot.SetActive(!b_mode);
		GO_ui_build.SetActive(b_mode);
	}

	public void UpdateScore(int i_score)
	{
		_text_score.text = "Score : " + i_score.ToString();
	}

	public void UpdateLifes(int i_lifes)
	{
		_text_lifes.text = "HP : " + i_lifes.ToString();
	}

	public void UpdateCredits(int i_credits)
	{
		_text_credits.text = "Credits : " + i_credits.ToString();
	}

	public void SetEnd(int i_score)
	{
		GO_ui_game.SetActive(false);
		GO_ui_capture_target.SetActive(false);
		GO_ui_general.SetActive(false);

		GO_ui_end.SetActive(true);

		_text_score_end.text = "Score : " + i_score.ToString();
	}
}
