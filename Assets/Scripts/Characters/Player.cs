using System.Collections;
using UnityEngine;

internal class Player : BaseCharacter
{
    private void Update()
    {
        SetHandler(SwipeManager.GetDirection());
        Move(SwipeManager.GetDirection());
    }

    protected override void Move(int direction)
    {
        if (!isCanMove) return;
        base.Move(direction);

        ChangeSprite(direction, baseSprites);
    }

    internal override IEnumerator Death()
    {
        StartCoroutine(UIManager.Instance.Curtain());
        GameManager.Instance.IsGame = false;
        yield return StartCoroutine(base.Death());
    }
}
