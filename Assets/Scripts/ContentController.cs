using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ContentController sýnýfý, oyun içeriðini kontrol eder.
public class ContentController : MonoBehaviour
{
    // Kullanýcý giriþ alaný
    [SerializeField] private TMP_InputField inputField;
    // Geçici buton
    [SerializeField] private Button temp;
    // Satýrlarý temsil eden RowController listesi
    [SerializeField] private List<RowController> rows;
    // Kelime yöneticisi
    [SerializeField] private WordManager wordManager;
    // Oyun yöneticisi
    [SerializeField] private GameManager gameManager;
    // Kazanma/Kaybetme metni
    [SerializeField] private Text winLoseText;
    // Doðru kelimeyi gösteren metin
    [SerializeField] private Text wordDisplayText;

    // Geçerli satýr indeksi
    private int _index;

    // Baþlangýçta çaðrýlan metod
    private void Start()
    {
        // Kullanýcý giriþ alanýna deðiþiklik ve onay dinleyicileri ekle
        inputField.onValueChanged.AddListener(OnUpdateContent);
        inputField.onSubmit.AddListener(OnSubmit);
    }

    // Kullanýcý giriþ alaný her deðiþtiðinde çaðrýlan metod
    private void OnUpdateContent(string msg)
    {
        // Geçerli satýrý güncelle
        var row = rows[_index];
        row.UpdateText(msg);
    }

    // Hücrelerin durumlarýný güncelleyen metod
    private bool UpdateState()
    {
        var states = wordManager.GetStates(inputField.text);
        rows[_index].UpdateState(states);

        // Tüm hücrelerin doðru olup olmadýðýný kontrol et
        foreach (var state in states)
        {
            if (state != State.Correct)
                return false;
        }

        return true;
    }

    // Kullanýcý giriþ alaný onaylandýðýnda çaðrýlan metod
    private void OnSubmit(string msg)
    {
        temp.Select();
        inputField.Select();

        // Giriþin yeterli uzunlukta olup olmadýðýný kontrol et
        if (!IsEnough())
        {
            Debug.Log("YETERSIZ...");
            return;
        }

        var isWin = UpdateState();
        if (isWin)
        {
            gameManager.GameOver(true); // Oyunu kazanma durumu
            return;
        }

        _index++;
        var isLost = _index == rows.Count;
        if (isLost)
        {
            gameManager.GameOver(false); // Oyunu kaybetme durumu
            return;
        }

        inputField.text = ""; // Kullanýcý giriþ alanýný temizle
    }

    // Giriþin yeterli uzunlukta olup olmadýðýný kontrol eden metod
    private bool IsEnough()
    {
        return inputField.text.Length == rows[_index].CellAmount;
    }

    // Kazanma/Kaybetme metnini güncelleyen metod
    public void UpdateWinLoseText(bool isWin)
    {
        if (isWin)
        {
            winLoseText.text = "Kazandýnýz!";
            wordDisplayText.text = "";
        }
        else
        {
            winLoseText.text = "Kaybettiniz!";
            wordDisplayText.text = "Doðru kelime: " + wordManager.GetSelectedWord();
        }
    }
}
