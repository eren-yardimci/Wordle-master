using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// CellController s�n�f�, her bir h�crenin kontrol�n� sa�lar.
public class CellController : MonoBehaviour
{
    // Durumlara g�re h�cre renkleri
    [SerializeField] private Color colorCorrect; // Do�ru olan h�cre rengi
    [SerializeField] private Color colorExist;   // ��erikte bulunan ama yanl�� yerde olan h�cre rengi
    [SerializeField] private Color colorFail;    // Yanl�� olan h�cre rengi
    [SerializeField] private Color colorNone;    // Hen�z kontrol edilmemi� h�cre rengi

    // H�crenin arka plan� ve metni
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;

    // H�credeki metni g�ncelleyen metod
    public void UpdateText(char msg)
    {
        text.SetText(msg.ToString());
    }

    // H�crenin durumunu g�ncelleyen metod
    public void UpdateState(State state)
    {
        background.color = GetColor(state); // Arka plan rengini g�ncelle
    }

    // Verilen duruma g�re uygun rengi d�nd�ren metod
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

// H�cre durumlar�n� temsil eden enum
public enum State
{
    None,     // Hen�z kontrol edilmemi�
    Contain,  // Kelimede mevcut ama yanl�� yerde
    Correct,  // Do�ru
    Fail      // Yanl��
}
