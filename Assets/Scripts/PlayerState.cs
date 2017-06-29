using System;

public static class PlayerState
{
	//stats
	public static float HP = 3.0f;
	public static int killedEnemies = 0;
	public static int score = 0;
	public static float remainingTime = 0.0f;

	//animation flags
	public static float horizontalDirection = 0.0f;
	public static bool isOnGround = false;
	public static bool isShooting = false;
	public static bool isDead = false;

	public static void reset() {
		HP = 3.0f;
		killedEnemies = 0;
		score = 0;
		remainingTime = 0.0f;

		horizontalDirection = 0.0f;
		isOnGround = false;
		isShooting = false;
		isDead = false;
	}

}