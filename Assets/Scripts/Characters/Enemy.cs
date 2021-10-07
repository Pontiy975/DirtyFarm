using System.Collections;
using UnityEngine;

internal class Enemy : BaseCharacter
{
    private const float targetRadius = 3f;

    [SerializeField] private Sprite[] angrySprites, dirtySprites;

    private int enemyDirection = 0;

    private Transform target;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = FindObjectOfType<Player>().transform;

        SetDirection();

        //StartCoroutine(Death());
    }

    // Update is called once per frame
    private void Update()
    {
        SetHandler(enemyDirection);
        Move(enemyDirection);
    }

    protected override void Move(int direction)
    {
        if (isDirty) return;
        if (!isCanMove) SetDirection();

        base.Move(direction);

        if (target && Vector2.Distance(transform.position, target.position) <= targetRadius)
        {
            ChangeSprite(direction, angrySprites);
        }
        else
        {
            ChangeSprite(direction, baseSprites);
        }
    }

    private void SetDirection()
    {
        enemyDirection = Random.Range(0, 4);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SetDirection();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(collision.gameObject.GetComponent<Player>().Death());
        }
    }

    internal override IEnumerator Death()
    {
        ChangeSprite(enemyDirection, dirtySprites);
        GameManager.Instance.enemies.Remove(this);
        yield return StartCoroutine(base.Death());
    }
}
