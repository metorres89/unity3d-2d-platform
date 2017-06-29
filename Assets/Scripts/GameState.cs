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

	private static int gemScore = 0;
	private static int killScore = 0;
	private static int timeMinuteScore = 0;
	private static int timeSecondScore = 0;
	private static int totalScore = 0;

	private static ResultType currentState = ResultType.INITIAL;

	public static void setState(ResultType newState, string nextScene) {

		Debug.Log ("trying to set State of GameState");

		if (newState != currentState) {

			currentState = newState;

			switch (currentState) {
			case ResultType.INITIAL:
				//reset private scores
				gemScore = 0;
				killScore = 0;
				timeMinuteScore = 0;
				totalScore = 0;
				break;
			case ResultType.GAME_OVER:
			case ResultType.WIN:
				
				//calculate final scores

				gemScore = PlayerState.score;
				killScore = PlayerState.killedEnemies * 2000;
				timeMinuteScore = ((int)PlayerState.remainingTime / 60) * 1000;
				timeSecondScore = ((int)PlayerState.remainingTime % 60) * 15;

				totalScore = gemScore + killScore + timeMinuteScore + timeSecondScore;
				break;
			}

			SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
		}
	}

	public static ResultType getCurrentState() {
		return currentState;
	}

	public static int getGemScore(){
		return gemScore;
	}

	public static int getKillScore() {
		return killScore;
	}

	public static int getTimeMinuteScore() {
		return timeMinuteScore;
	}

	public static int getTimeSecondScore() {
		return timeSecondScore;
	}

	public static int getTotalScore() {
		return totalScore;
	}
}