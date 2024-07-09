using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Animator playerAnim;
    public float weaponRange;
    public float attackDelay;
    public Transform weaponTransform;
    public LayerMask enemyLayer;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
    }

    //Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerAnim.Play("SwordSlash");
        }
    }

    void Attack()
    {
        Collider2D enemy = Physics2D.OverlapCircle(weaponTransform.position, weaponRange, enemyLayer);
        if (enemy == null)
        {
            return;
        }

        if (enemy.tag == "Bear")
        {
            enemy.GetComponent<Bear>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Bird")
        {
            enemy.GetComponent<Bird>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Boar")
        {
            enemy.GetComponent<Boar>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Deer1")
        {
            enemy.GetComponent<Deer1>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Deer2")
        {
            enemy.GetComponent<Deer2>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Camel")
        {
            enemy.GetComponent<Camel>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Fox")
        {
            enemy.GetComponent<Fox>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Rabbit")
        {
            enemy.GetComponent<Rabbit>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Snake")
        {
            enemy.GetComponent<Snake>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Snake2")
        {
            enemy.GetComponent<Snake2>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Snake3")
        {
            enemy.GetComponent<Snake3>().TakeDamage(playerStats.attack);
        }
        else if (enemy.tag == "Wolf")
        {
            enemy.GetComponent<Wolf>().TakeDamage(playerStats.attack);
        }

    }
}
