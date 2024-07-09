using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    private PlayerHealth playerhealth;

    private BoxCollider2D myCollider;

    private void Awake()
    {
        playerhealth = FindObjectOfType<PlayerHealth>(); // hierarchy â�� �ִ� ��� PlayerHealth �� ã��
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerhealth.pTakeDamage(damage);
        }
    }
}
