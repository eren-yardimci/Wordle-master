using System.Collections.Generic;
using UnityEngine;

// RowController s�n�f�, bir sat�rdaki h�creleri kontrol eder.
public class RowController : MonoBehaviour
{
    // H�creleri temsil eden CellController listesi
    [SerializeField] private List<CellController> cells;
    // H�cre say�s�n� d�nd�ren �zellik
    public int CellAmount => cells.Count;

    // H�crelerdeki metinleri g�ncelleyen metot
    public void UpdateText(string msg)
    {
        // Mesaj� karakter dizisine d�n��t�r
        var arrayChar = msg.ToCharArray();
        for (int i = 0; i < cells.Count; i++)
        {
            var cell = cells[i];

            // Mevcut karakter var m� kontrol et
            bool isExist = i < arrayChar.Length;
            var content = isExist ? arrayChar[i] : ' ';
            // H�crenin metnini g�ncelle
            cell.UpdateText(content);
        }
    }

    // H�crelerin durumlar�n� g�ncelleyen metot
    public void UpdateState(List<State> states)
    {
        for (int i = 0; i < states.Count; i++)
        {
            var cell = cells[i];
            var state = states[i];
            // H�crenin durumunu g�ncelle
            cell.UpdateState(state);
        }
    }
}
