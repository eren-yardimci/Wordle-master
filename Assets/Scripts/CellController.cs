using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// CellController sýnýfý, her bir hücrenin kontrolünü saðlar.
public class CellController : MonoBehaviour
{
    // Durumlara göre hücre renkleri
    [SerializeField] private Color colorCorrect; // Doðru olan hücre rengi
    [SerializeField] private Color colorExist;   // Ýçerikte bulunan ama yanlýþ yerde olan hücre rengi
    [SerializeField] private Color colorFail;    // Yanlýþ olan hücre rengi
    [SerializeField] private Color colorNone;    // Henüz kontrol edilmemiþ hücre rengi

    // Hücrenin arka planý ve metni
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;

    // Hücredeki metni güncelleyen metod
    public void UpdateText(char msg)
    {
        text.SetText(msg.ToString());
    }

    // Hücrenin durumunu güncelleyen metod
    public void UpdateState(State state)
    {
        background.color = GetColor(state); // Arka plan rengini güncelle
    }

    // Verilen duruma göre uygun rengi döndüren metod
    private Color GetColor(State state)
    {
        return state switch
        {
            State.None => colorNone,
            State.Contain => colorExist,
            State.Correct => colorCorrect,
            State.Fail => colorFail,
        };
    }
}

// Hücre durumlarýný temsil eden enum
public enum State
{
    None,     // Henüz kontrol edilmemiþ
    Contain,  // Kelimede mevcut ama yanlýþ yerde
    Correct,  // Doðru
    Fail      // Yanlýþ
}
