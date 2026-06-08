using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI")]
    public TMP_Text TutorialText;

    public GameObject WImage;
    public GameObject AImage;
    public GameObject SImage;
    public GameObject DImage;

    [Header("Fade")]
    public Image fadePanel;
    public float fadeDuration = 1f;

    [Header("Minions")]
    public GameObject minionPrefab;
    public Transform[] spawnPoints;

    [Header("Player")]
    public Transform player;

    private Vector3 playerStartPos;

    private int kills;
    private bool movementCompleted;
    private bool shootingStarted;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerStartPos = player.position;

        // Start with fade panel invisible
        if (fadePanel != null)
        {
            Color c = fadePanel.color;
            c.a = 0f;
            fadePanel.color = c;
        }

        TutorialText.text = "Move using WASD";

        WImage.SetActive(true);
        AImage.SetActive(true);
        SImage.SetActive(true);
        DImage.SetActive(true);
    }

    void Update()
    {
        CheckMovement();
    }

    void CheckMovement()
    {
        if (movementCompleted)
            return;

        float distance =
            Vector3.Distance(player.position, playerStartPos);

        if (distance > 2f)
        {
            movementCompleted = true;
            StartCoroutine(FadeToShootingTutorial());
        }
    }

    IEnumerator FadeToShootingTutorial()
    {
        float elapsed = 0f;
        Color color = fadePanel.color;

        // Fade to black
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }

        WImage.SetActive(false);
        AImage.SetActive(false);
        SImage.SetActive(false);
        DImage.SetActive(false);

        TutorialText.text =
            "Left Click to Shoot\n\nKill 3 Minions\n0/3";

        SpawnMinions();

        yield return new WaitForSeconds(0.3f);

        // Fade back in
        elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }
    }

    void SpawnMinions()
    {
        if (shootingStarted)
            return;

        shootingStarted = true;

        foreach (Transform spawn in spawnPoints)
        {
            Instantiate( minionPrefab, spawn.position, Quaternion.identity);
        }
    }

    public void MinionKilled()
    {
        kills++;

        TutorialText.text =
            "Left Click to Shoot\n\nKill 3 Minions\n" +
            kills + "/3";

        if (kills >= 3)
        {
            TutorialCompleted();
        }
    }

    void TutorialCompleted()
    {
        TutorialText.text = "Tutorial Complete!";

        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        float elapsed = 0f;
        Color color = fadePanel.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }

        SceneManager.LoadScene("ArenaTest");
    }
}