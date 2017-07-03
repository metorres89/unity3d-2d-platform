using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameState
{
	public enum ResultType
	{
		GAME_OVER,
		WIN,
		INITIAL
	};

	private static int GemScore = 0;
	private static int KillScore = 0;
	private static int TimeMinuteScore = 0;
	private static int TimeSecondScore = 0;
	private static int TotalScore = 0;

	private static ResultType currentState = ResultType.INITIAL;

	public static void SetState(ResultType newState, string nextScene) {

		Debug.Log ("trying to set State of GameState");

		if (newState != currentState) {

			currentState = newState;

			switch (currentState) {
			case ResultType.INITIAL:
				//reset private scores
				GemScore = 0;
				KillScore = 0;
				TimeMinuteScore = 0;
				TotalScore = 0;
				break;
			case ResultType.GAME_OVER:
			case ResultType.WIN:
				
				//calculate final scores

				GemScore = PlayerState.Score;
				KillScore = PlayerState.KilledEnemies * 2000;
				TimeMinuteScore = ((int)PlayerState.RemainingTime / 60) * 1000;
				TimeSecondScore = ((int)PlayerState.RemainingTime % 60) * 15;

				TotalScore = GemScore + KillScore + TimeMinuteScore + TimeSecondScore;
				break;
			}

			SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
		}
	}

	public static ResultType GetCurrentState() {
		return currentState;
	}

	public static int GetGemScore(){
		return GemScore;
	}

	public static int GetKillScore() {
		return KillScore;
	}

	public static int GetTimeMinuteScore() {
		return TimeMinuteScore;
	}

	public static int GetTimeSecondScore() {
		return TimeSecondScore;
	}

	public static int GetTotalScore() {
		return TotalScore;
	}
}