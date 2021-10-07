using System.Collections.Generic;
using UnityEngine;

internal class GameManager : MonoBehaviour
{
    internal static GameManager Instance { get; private set; }

    [SerializeField] internal List<Enemy> enemies = new List<Enemy>();

    internal bool IsGame { get; set; } = false;
    internal bool IsWin { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (enemies.Count <= 0)
        {
            IsGame = false;
            IsWin = true;
            StartCoroutine(UIManager.Instance.Curtain());
        }
        else if (!IsGame)
        {
            IsWin = false;
        }
    }
}
