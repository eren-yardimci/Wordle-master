using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ContentController s�n�f�, oyun i�eri�ini kontrol eder.
public class ContentController : MonoBehaviour
{
    // Kullan�c� giri� alan�
    [SerializeField] private TMP_InputField inputField;
    // Ge�ici buton
    [SerializeField] private Button temp;
    // Sat�rlar� temsil eden RowController listesi
    [SerializeField] private List<RowController> rows;
    // Kelime y�neticisi
    [SerializeField] private WordManager wordManager;
    // Oyun y�neticisi
    [SerializeField] private GameManager gameManager;
    // Kazanma/Kaybetme metni
    [SerializeField] private Text winLoseText;
    // Do�ru kelimeyi g�steren metin
    [SerializeField] private Text wordDisplayText;

    // Ge�erli sat�r indeksi
    private int _index;

    // Ba�lang��ta �a�r�lan metod
    private void Start()
    {
        // Kullan�c� giri� alan�na de�i�iklik ve onay dinleyicileri ekle
        inputField.onValueChanged.AddListener(OnUpdateContent);
        inputField.onSubmit.AddListener(OnSubmit);
    }

    // Kullan�c� giri� alan� her de�i�ti�inde �a�r�lan metod
    private void OnUpdateContent(string msg)
    {
        // Ge�erli sat�r� g�ncelle
        var row = rows[_index];
        row.UpdateText(msg);
    }

    // H�crelerin durumlar�n� g�ncelleyen metod
    private bool UpdateState()
    {
        var states = wordManager.GetStates(inputField.text);
        rows[_index].UpdateState(states);

        // T�m h�crelerin do�ru olup olmad���n� kontrol et
        foreach (var state in states)
        {
            if (state != State.Correct)
                return false;
        }

        return true;
    }

    // Kullan�c� giri� alan� onayland���nda �a�r�lan metod
    private void OnSubmit(string msg)
    {
        temp.Select();
        inputField.Select();

        // Giri�in yeterli uzunlukta olup olmad���n� kontrol et
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

        inputField.text = ""; // Kullan�c� giri� alan�n� temizle
    }

    // Giri�in yeterli uzunlukta olup olmad���n� kontrol eden metod
    private bool IsEnough()
    {
        return inputField.text.Length == rows[_index].CellAmount;
    }

    // Kazanma/Kaybetme metnini g�ncelleyen metod
    public void UpdateWinLoseText(bool isWin)
    {
        if (isWin)
        {
            winLoseText.text = "Kazand�n�z!";
            wordDisplayText.text = "";
        }
        else
        {
            winLoseText.text = "Kaybettiniz!";
            wordDisplayText.text = "Do�ru kelime: " + wordManager.GetSelectedWord();
        }
    }
}
