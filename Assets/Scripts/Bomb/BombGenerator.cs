using UnityEngine;

internal class BombGenerator : MonoBehaviour
{
    [SerializeField] private float reloadTime = 5f;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform target;

    private float cooldown = 0f;
    private bool isCanPlant = false;

    private void Update()
    {
        Timer();
        GetInput();
    }

    private void Timer()
    {
        if (isCanPlant) return;

        cooldown += Time.deltaTime;
        if (cooldown >= reloadTime)
        {
            isCanPlant = true;
            cooldown = 0f;
        }
    }

    private void GetInput()
    {
        if (Input.touchCount > 1 && isCanPlant)
        {
            Touch touch = Input.GetTouch(1);

            if (touch.phase == TouchPhase.Began) Planting();
        }
    }

    private void Planting()
    {
        GameObject bomb = Instantiate(bombPrefab);
        bomb.transform.position = target.position;
        isCanPlant = false;
    }
}
