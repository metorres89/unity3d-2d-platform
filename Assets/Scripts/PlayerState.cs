using System;

public static class PlayerState
{
	//stats
	public static float HP = 3.0f;
	public static int killedEnemies = 0;

	//animation flags
	public static float horizontalDirection = 0.0f;
	public static bool isOnGround = false;
	public static bool isShooting = false;
	public static bool isDead = false;
}