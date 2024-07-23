using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// GameManager s�n�f�, oyunun genel y�netimini sa�lar.
public class GameManager : MonoBehaviour
{
    // Oyun bitti�inde g�sterilecek UI nesnesi
    public GameObject gameOverUI;
    // Kullan�c� giri� alan�
    public TMP_InputField inputField;
    // ��erik denetleyicisi
    public ContentController contentController;

    // Oyunun bitip bitmedi�ini kontrol eden de�i�ken
    private bool isGameOver = false;

    // Her karede �a�r�lan Update metodu
    void Update()
    {
        // E�er oyun bitmediyse ve Escape tu�una bas�lm��sa
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            // Oyunu bitir (kaybetme durumu)
            GameOver(false);
        }
    }

    // Oyunu bitiren metot
    public void GameOver(bool isWin)
    {
        isGameOver = true; // Oyunun bitti�ini i�aretle
        gameOverUI.SetActive(true); // Oyun biti� UI'sini g�ster
        inputField.enabled = false; // Kullan�c� giri� alan�n� devre d��� b�rak
        contentController.UpdateWinLoseText(isWin); // Kazanma/kaybetme metnini g�ncelle
    }

    // Oyunu yeniden ba�latan metot
    public void Retry()
    {
        SceneManager.LoadScene("MainScene"); // Ana sahneyi yeniden y�kle
    }

    // Ana men�ye d�nen metot
    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesini y�kle
    }
}
