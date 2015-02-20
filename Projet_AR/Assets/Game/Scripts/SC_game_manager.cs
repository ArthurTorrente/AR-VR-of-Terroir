using UnityEngine;
using System.Collections;

public class SC_game_manager : MonoBehaviour {

	[SerializeField]
	static public SC_game_manager _instance;

	[SerializeField]
	private SC_ui_manager _ui_manager;
	[SerializeField]
	private SC_build_manager _build_manager;

	[SerializeField]
	private GameObject _GO_root_game;

	private int _i_score = 0;
	private int _i_lifes = 10;
	private int _i_credits = 5;

	private bool _b_mode = false; // FALSE : Shoot / TRUE : Build


	void Start ()
	{
		_instance = this;

		UnactiveGame();

		_ui_manager.SetMode(_b_mode);
		_build_manager.SetActive(_b_mode);

		_ui_manager.UpdateScore(_i_score);
		_ui_manager.UpdateLifes(_i_lifes);
		_ui_manager.UpdateCredits(_i_credits);
	}

	public void ActiveGame()
	{
		_GO_root_game.SetActive(true);
		if (_i_lifes > 0)
		{
			if (!_b_mode)
				Time.timeScale = 1;
			_ui_manager.SetActiveGame(true);
		}
	}

	public void UnactiveGame()
	{
		_GO_root_game.SetActive(false);
		if (_i_lifes > 0)
		{
			Time.timeScale = 0;
			_ui_manager.SetActiveGame(false);
		}
	}

	public void SwitchMode()
	{
		_b_mode = !_b_mode;
		_ui_manager.SetMode(_b_mode);
		_build_manager.SetActive(_b_mode);
		if (_b_mode)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void EnemyKilled()
	{
		++_i_score;
		++_i_credits;
		_ui_manager.UpdateScore(_i_score);
		_ui_manager.UpdateCredits(_i_credits);
	}

	public void DecreasedLife()
	{
		--_i_lifes;
		_ui_manager.UpdateLifes(_i_lifes);
		if (_i_lifes == 0)
			End();
	}

	public bool SpendCredits(int i_nb_credits)
	{
		if (i_nb_credits > _i_credits)
			return false;

		_i_credits -= i_nb_credits;
		_ui_manager.UpdateCredits(_i_credits);

		return true;
	}

	private void End()
	{
		Time.timeScale = 0;
		_ui_manager.SetEnd(_i_score);
	}

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
