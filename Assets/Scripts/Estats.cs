using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Estats : MonoBehaviour
{
    public float HP;
    public float WSpeed;
    public float ASpeed;
    public float damage;
    private SpriteRenderer SpriteRenderer;
    public Animator _animator;
    private Rigidbody2D _rigidbody2D;
    public GameObject self;
    private Transform _transform;
    private float XMovement = 1;
    private float timer = 1f;
    private float coundown;
    private string Enemytag = "Cat";
    private bool AT=false;
    // Start is called before the first frame update
    private void Start()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInParent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        timer = timer / ASpeed;
        coundown = timer;
    }
    private void Update()
    {
        if (AT)
        {
            if (!_animator.GetBool("Attack"))
            {
                coundown -= Time.deltaTime;
                if (coundown < 0) { _animator.SetBool("Attack", true); coundown = timer; }
            }
        }
        if (_animator.GetBool("Walk"))
        {
            _rigidbody2D.velocity = new Vector2(WSpeed * XMovement, _rigidbody2D.velocity.y);
        }
        if (HP <= 0)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        _animator.SetBool("Dead", true);
        yield return new WaitForSeconds(100f);
        Destroy(gameObject);
    }
    public void Summon()

    {
        Instantiate(self, GameObject.Find("Base").transform);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == Enemytag)
        {
            HP -= collision.GetComponentInChildren<CatStats>().damage;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            AT = true;
            _animator.SetBool("Walk", false);
        };
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        AT = false;
        _animator.SetBool("Walk", true);
    }
}
