using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCombatRoom : RoomDungeon
{
    public Door[] doors;
    public void EnemiesCheck()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy && enemies.ToArray().Length <= 0)
            {
                return;
            }
        }
        OpenDoors();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            CloseDoors();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }


    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++) 
        {
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++) 
        {
            doors[i].Open();
        }
    }
}
