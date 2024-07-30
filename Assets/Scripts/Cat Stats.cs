using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStats : MonoBehaviour
{
    public float HP;
    public float WSpeed;
    public float ASpeed;
    public float damage;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    public GameObject self;
    private Transform _transform;
    private float timer =1f;
    private float coundown;
    private string Enemytag = "Enemy";
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        timer = timer / ASpeed;
        coundown = timer;
    }
    private void Update()
    {
        if (_animator.GetBool("Walk"))
        {
            _rigidbody2D.velocity = new Vector2(WSpeed * -1, _rigidbody2D.velocity.y);
        }
        if (HP <= 0)
        {
            StartCoroutine(Death());
        }
        if (!_animator.GetBool("Attack"))
        {
            coundown -= Time.deltaTime;
            if (coundown < 0) { _animator.SetBool("attack", true); coundown = timer; }
        }
    }

    private IEnumerator Death()
    {
        _animator.SetBool("Dead", true);
        yield return new WaitForSeconds(100f);
        Destroy(gameObject);
    }
    public void Summon ()

    {
        Instantiate(self, GameObject.Find("Base").transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Enemytag)
        {
            HP -= collision.GetComponentInChildren<Estats>().damage;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        _animator.SetBool("Walk", true);
    }
}
