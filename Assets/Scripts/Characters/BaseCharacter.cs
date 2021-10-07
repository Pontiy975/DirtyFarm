using System.Collections;
using UnityEngine;

internal class BaseCharacter : MonoBehaviour
{
    private const float blinkDelay = 0.5f;

    [SerializeField] protected float speed = 5f, handlerRadius = 0.5f, handlerSize = 1f, handlerOffsetY = -0.25f;

    [SerializeField] protected LayerMask obstacleLayer;
    [SerializeField] protected Transform handler;
    [SerializeField] protected Sprite[] baseSprites;

    protected bool isCanMove = false, isDirty = false;

    protected SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Move(int direction)
    {
        if (isDirty || !GameManager.Instance.IsGame) return;

        switch (direction)
        {
            case 0:
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                break;
            case 1:
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                break;
            case 2:
                transform.Translate(new Vector2(0, speed * Time.deltaTime));
                break;
            case 3:
                transform.Translate(new Vector2(0, -speed * Time.deltaTime));
                break;
        }
    }

    protected void SetHandler(int direction)
    {
        switch (direction)
        {
            case 0:
                handler.localPosition = new Vector2(handlerRadius, handlerOffsetY);
                break;
            case 1:
                handler.localPosition = new Vector2(-handlerRadius, handlerOffsetY);
                break;
            case 2:
                handler.localPosition = new Vector2(0, handlerRadius);
                break;
            case 3:
                handler.localPosition = new Vector2(0, handlerOffsetY - handlerRadius);
                break;
            default:
                handler.localPosition = new Vector2(0, handlerOffsetY);
                break;
        }

        isCanMove = !Physics2D.OverlapCircle(handler.position, handlerRadius, obstacleLayer);
    }



    // Направление идентично индексу нужного спрайта в массиве
    protected virtual void ChangeSprite(int direction, Sprite[] spritesArray)
    {
        if (isDirty) return;
        if (direction >= 0) spriteRenderer.sprite = spritesArray[direction];
    }

    internal virtual IEnumerator Death()
    {
        isDirty = true;

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(blinkDelay);
            spriteRenderer.color = new Color(255, 255, 255, 0.5f);
            yield return new WaitForSeconds(blinkDelay);
            spriteRenderer.color = new Color(255, 255, 255, 1);
        }

        yield return new WaitForSeconds(blinkDelay);
        
        Destroy(gameObject);
    }
}
