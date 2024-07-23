using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// GameManager sýnýfý, oyunun genel yönetimini saðlar.
public class GameManager : MonoBehaviour
{
    // Oyun bittiðinde gösterilecek UI nesnesi
    public GameObject gameOverUI;
    // Kullanýcý giriþ alaný
    public TMP_InputField inputField;
    // Ýçerik denetleyicisi
    public ContentController contentController;

    // Oyunun bitip bitmediðini kontrol eden deðiþken
    private bool isGameOver = false;

    // Her karede çaðrýlan Update metodu
    void Update()
    {
        // Eðer oyun bitmediyse ve Escape tuþuna basýlmýþsa
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            // Oyunu bitir (kaybetme durumu)
            GameOver(false);
        }
    }

    // Oyunu bitiren metot
    public void GameOver(bool isWin)
    {
        isGameOver = true; // Oyunun bittiðini iþaretle
        gameOverUI.SetActive(true); // Oyun bitiþ UI'sini göster
        inputField.enabled = false; // Kullanýcý giriþ alanýný devre dýþý býrak
        contentController.UpdateWinLoseText(isWin); // Kazanma/kaybetme metnini güncelle
    }

    // Oyunu yeniden baþlatan metot
    public void Retry()
    {
        SceneManager.LoadScene("MainScene"); // Ana sahneyi yeniden yükle
    }

    // Ana menüye dönen metot
    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükle
    }
}
