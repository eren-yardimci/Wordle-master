using System.Collections.Generic;
using UnityEngine;

// RowController sýnýfý, bir satýrdaki hücreleri kontrol eder.
public class RowController : MonoBehaviour
{
    // Hücreleri temsil eden CellController listesi
    [SerializeField] private List<CellController> cells;
    // Hücre sayýsýný döndüren özellik
    public int CellAmount => cells.Count;

    // Hücrelerdeki metinleri güncelleyen metot
    public void UpdateText(string msg)
    {
        // Mesajý karakter dizisine dönüþtür
        var arrayChar = msg.ToCharArray();
        for (int i = 0; i < cells.Count; i++)
        {
            var cell = cells[i];

            // Mevcut karakter var mý kontrol et
            bool isExist = i < arrayChar.Length;
            var content = isExist ? arrayChar[i] : ' ';
            // Hücrenin metnini güncelle
            cell.UpdateText(content);
        }
    }

    // Hücrelerin durumlarýný güncelleyen metot
    public void UpdateState(List<State> states)
    {
        for (int i = 0; i < states.Count; i++)
        {
            var cell = cells[i];
            var state = states[i];
            // Hücrenin durumunu güncelle
            cell.UpdateState(state);
        }
    }
}
