using UnityEngine;

internal class Bomb : MonoBehaviour
{
    [SerializeField] private float cooldown = 3f, radius = 0.25f;

    private bool isExplode = false;

    //private List<GameObject> charactersInTrigger = new List<GameObject>();
        
    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (isExplode) return;

        cooldown -= Time.deltaTime;
        if (cooldown <= 0) Explosion();
    }

    private void Explosion()
    {
        isExplode = true;

        BaseCharacter[] characters = FindObjectsOfType<BaseCharacter>();

        foreach (BaseCharacter character in characters)
        {
            if (Vector2.Distance(transform.position, character.transform.position) <= radius)
            {
                StartCoroutine(character.Death());
            }
        }

        //charactersInTrigger.Clear();
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //        charactersInTrigger.Add(collision.gameObject);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //        charactersInTrigger.Remove(collision.gameObject);
    //}
}
