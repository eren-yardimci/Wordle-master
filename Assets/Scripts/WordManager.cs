using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

// WordManager sýnýfý Unity'de bir MonoBehaviour'dur ve kelime veritabanýndan rastgele kelimeler çekmek için kullanýlýr.
public class WordManager : MonoBehaviour
{
    // Veritabaný dosyasýnýn yolu
    private string databasePath = @"C:\Users\eren\Desktop\Wordle-master\Assets\WordleDB.json";
    // Seçilen kelime
    private string selectedWord;

    // Veritabanýndan rastgele bir kelime seçen metot
    private string GetRandomWordFromDatabase()
    {
        string randomWord = null;

        // JSON dosyasýný oku
        string jsonText = File.ReadAllText(databasePath);

        // JSON verisini WordDataList nesnesine dönüþtür
        WordDataList wordDataList = JsonUtility.FromJson<WordDataList>(jsonText);

        // Eðer wordDataList null deðilse ve içinde kelimeler varsa
        if (wordDataList != null && wordDataList.words.Count > 0)
        {
            // Rastgele bir indeks seç
            int randomIndex = Random.Range(0, wordDataList.words.Count);
            // Seçilen indeksteki kelimeyi al
            randomWord = wordDataList.words[randomIndex].kelime;
        }

        return randomWord;
    }

    // Seçilen kelimeyi döndüren metot (eðer daha önce seçilmemiþse, yeni bir kelime seçer)
    public string GetSelectedWord()
    {
        if (string.IsNullOrEmpty(selectedWord))
        {
            selectedWord = GetRandomWordFromDatabase();
        }
        return selectedWord;
    }

    // Kelime verilerini temsil eden sýnýf
    [System.Serializable]
    public class WordData
    {
        public string kelime; // Kelimenin kendisi
    }

    // Kelime listesini temsil eden sýnýf
    [System.Serializable]
    public class WordDataList
    {
        public List<WordData> words; // Kelimelerin listesi
    }

    // Kullanýcýnýn tahmin ettiði kelimeye göre durumlarý döndüren metot
    public List<State> GetStates(string msg)
    {
        var result = new List<State>();

        // Seçilen kelimeyi karakterlere ayýr ve listeye çevir
        var list = GetSelectedWord().ToCharArray().ToList();
        // Kullanýcýnýn tahminini karakterlere ayýr ve listeye çevir
        var listCurrent = msg.ToCharArray().ToList();

        // Tahmin edilen her karakter için durumlarý belirle
        for (var i = 0; i < listCurrent.Count; i++)
        {
            var currentChar = listCurrent[i];
            var contains = list.Contains(currentChar);
            if (contains)
            {
                var index = list.IndexOf(currentChar);
                var isCorrect = index == i;
                result.Add(isCorrect ? State.Correct : State.Contain);
                list[index] = ' '; // Karakterin tekrar bulunmasýný engelle
            }
            else
            {
                result.Add(State.None);
            }
        }

        // Durumu None olanlarý Fail olarak güncelle
        for (var i = 0; i < listCurrent.Count; i++)
        {
            if (result[i] == State.None)
            {
                result[i] = State.Fail;
            }
        }

        return result;
    }
}
