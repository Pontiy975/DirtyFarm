using UnityEngine;

internal class SwipeManager : MonoBehaviour
{
    private static Vector3 startTouch, direction;

    private void Update()
    {
        GetInput();
    }

    private static void GetInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouch = Camera.main.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Moved:
                    direction = Camera.main.ScreenToWorldPoint(touch.position) - startTouch;
                    break;
                case TouchPhase.Ended:
                    direction = Vector2.zero;
                    break;
            }
        }
    }

    /// <summary>
    /// Возвращает целочисленное значение направления свайпа
    /// </summary>
    /// <returns>0 - right; 1 - left; 2 - top; 3 - bottom. -1 - stands</returns>
    internal static int GetDirection()
    {
        // Определяет ось смещения. Возвращает число в зависимости от значения смещения по конкретной оси
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            return direction.x > 0 ? 0 : 1;

        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            return direction.y > 0 ? 2 : 3;

        else return -1;
    }
}
