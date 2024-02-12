using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public Image DeathMenuImage;
    public TextMeshProUGUI DeathMenuText;
    public float fadeInDuration = 1f; // Durée de l'animation de fondu

    public Button restartButton;
    public Button quitButton;


    void Start()
    {
        DeathMenuImage.gameObject.SetActive(false);
        DeathMenuText.gameObject.SetActive(false);
        StartCoroutine(FadeInElements());

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);

    }

    void RestartGame()
    {
        Destroy(PlayerManager.Instance);
        Destroy(EnemyManager.Instance);
        Destroy(AugmentManager.Instance);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    void QuitGame()
    {
        Destroy(PlayerManager.Instance);
        Destroy(EnemyManager.Instance);
        Destroy(AugmentManager.Instance);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }



    IEnumerator FadeInElements()
    {
        // Activer l'image et le texte
        DeathMenuImage.gameObject.SetActive(true);
        DeathMenuText.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < fadeInDuration)
        {
            // Calculer l'opacité actuelle en fonction du temps écoulé
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);

            // Modifier l'opacité de l'image et du texte
            Color imageColor = DeathMenuImage.color;
            imageColor.a = alpha;
            DeathMenuImage.color = imageColor;

            Color textColor = DeathMenuText.color;
            textColor.a = alpha;
            DeathMenuText.color = textColor;

            // Attendre une image de frame
            yield return null;

            // Mettre à jour le temps écoulé
            timer += Time.deltaTime;
        }

        // Assurez-vous que l'opacité finale est définie
        Color finalImageColor = DeathMenuImage.color;
        finalImageColor.a = 1f;
        DeathMenuImage.color = finalImageColor;

        Color finalTextColor = DeathMenuText.color;
        finalTextColor.a = 1f;
        DeathMenuText.color = finalTextColor;
    }
}
