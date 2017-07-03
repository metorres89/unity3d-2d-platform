using System;

public static class PlayerState
{
	//stats
	public static float HealthPoints = 3.0f;
	public static int KilledEnemies = 0;
	public static int Score = 0;
	public static float RemainingTime = 0.0f;

	//animation flags
	public static float HorizontalDirection = 0.0f;
	public static bool IsOnGround = false;
	public static bool IsShooting = false;
	public static bool IsDead = false;

	public static void Reset() {
		HealthPoints = 3.0f;
		KilledEnemies = 0;
		Score = 0;
		RemainingTime = 0.0f;

		HorizontalDirection = 0.0f;
		IsOnGround = false;
		IsShooting = false;
		IsDead = false;
	}

}