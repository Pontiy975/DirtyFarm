using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

internal class UIManager : MonoBehaviour
{
    private bool isFirstStart = true;

    internal static UIManager Instance { get; private set; }

    private const float curtainDelay = 0.01f, itemDelay = 0.1f;

    [SerializeField] private Image curtain;
    [SerializeField] private Text GOText, rulesText;
    [SerializeField] private GameObject buttonPanel;

    private void Awake()
    {
        Instance = this;
    }

    internal IEnumerator Curtain(bool isDown = true)
    {
        if (isDown)
        {
            while (curtain.color.a < 1)
            {
                yield return new WaitForSeconds(curtainDelay);
                curtain.color = new Color(0, 0, 0, curtain.color.a + Time.deltaTime);
            }

            yield return new WaitForSeconds(itemDelay);
            GOText.text = GameManager.Instance.IsWin ? "You Win!" : "Game Over ;(";
            GOText.gameObject.SetActive(true);


            yield return new WaitForSeconds(itemDelay);
            buttonPanel.SetActive(true);

        }
        else
        {
            rulesText.gameObject.SetActive(false);
            GOText.gameObject.SetActive(false);
            buttonPanel.SetActive(false);

            while (curtain.color.a > 0)
            {
                yield return new WaitForSeconds(curtainDelay);
                curtain.color = new Color(0, 0, 0, curtain.color.a - Time.deltaTime);
            }

            GameManager.Instance.IsGame = true;
            isFirstStart = false;
        }
    }

    public void PlayButton()
    {
        if (isFirstStart)
            StartCoroutine(Curtain(false));
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_ANDROID
            Application.Quit();
        #endif
    }
}
