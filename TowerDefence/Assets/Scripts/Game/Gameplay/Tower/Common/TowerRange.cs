using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : Child<Tower>
{
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            Parent.Enemys.Add(collision.GetComponent<Enemy>());
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null && Parent.Enemys.Contains(enemy))
        {
            Parent.Enemys.Remove(enemy);
        }
    }
}
