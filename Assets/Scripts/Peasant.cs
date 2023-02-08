using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{
    private Transform player;
    private Animator anim;

    [SerializeField] private GameObject stone;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float attackCd = 0f;
    [SerializeField] private float range = 0f;
    [SerializeField] private float shootForce = 0f;

    [SerializeField] private int health = 0;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CalculateInRange();
    }

    Coroutine attackRoutine;
    private void CalculateInRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);

        if(distance <= range && angle < 65 && attackRoutine == null)
        {
            attackRoutine = StartCoroutine(Attack());
        } else if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

    }

    public IEnumerator Attack()
    {
        while(true)
        {
            transform.LookAt(player);
            yield return new WaitForSeconds(.2f);
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(attackCd);
            anim.ResetTrigger("Attack");
            anim.SetTrigger("Idle");
        }
    }

    public void ShootStone()
    {
        GameObject stoneC = Instantiate(stone, shootPoint.position, shootPoint.rotation);
        stoneC.transform.LookAt(player);
        stoneC.GetComponent<Rigidbody>().AddForce(stoneC.transform.forward * shootForce);
        Destroy(stoneC, 5f);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            health--;
            if(health <= 0)
            {
                StopAllCoroutines();
                anim.SetTrigger("Die");
            }

        }
    }

}
