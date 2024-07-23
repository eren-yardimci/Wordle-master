using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

// WordManager s�n�f� Unity'de bir MonoBehaviour'dur ve kelime veritaban�ndan rastgele kelimeler �ekmek i�in kullan�l�r.
public class WordManager : MonoBehaviour
{
    // Veritaban� dosyas�n�n yolu
    private string databasePath = @"C:\Users\eren\Desktop\Wordle-master\Assets\WordleDB.json";
    // Se�ilen kelime
    private string selectedWord;

    // Veritaban�ndan rastgele bir kelime se�en metot
    private string GetRandomWordFromDatabase()
    {
        string randomWord = null;

        // JSON dosyas�n� oku
        string jsonText = File.ReadAllText(databasePath);

        // JSON verisini WordDataList nesnesine d�n��t�r
        WordDataList wordDataList = JsonUtility.FromJson<WordDataList>(jsonText);

        // E�er wordDataList null de�ilse ve i�inde kelimeler varsa
        if (wordDataList != null && wordDataList.words.Count > 0)
        {
            // Rastgele bir indeks se�
            int randomIndex = Random.Range(0, wordDataList.words.Count);
            // Se�ilen indeksteki kelimeyi al
            randomWord = wordDataList.words[randomIndex].kelime;
        }

        return randomWord;
    }

    // Se�ilen kelimeyi d�nd�ren metot (e�er daha �nce se�ilmemi�se, yeni bir kelime se�er)
    public string GetSelectedWord()
    {
        if (string.IsNullOrEmpty(selectedWord))
        {
            selectedWord = GetRandomWordFromDatabase();
        }
        return selectedWord;
    }

    // Kelime verilerini temsil eden s�n�f
    [System.Serializable]
    public class WordData
    {
        public string kelime; // Kelimenin kendisi
    }

    // Kelime listesini temsil eden s�n�f
    [System.Serializable]
    public class WordDataList
    {
        public List<WordData> words; // Kelimelerin listesi
    }

    // Kullan�c�n�n tahmin etti�i kelimeye g�re durumlar� d�nd�ren metot
    public List<State> GetStates(string msg)
    {
        var result = new List<State>();

        // Se�ilen kelimeyi karakterlere ay�r ve listeye �evir
        var list = GetSelectedWord().ToCharArray().ToList();
        // Kullan�c�n�n tahminini karakterlere ay�r ve listeye �evir
        var listCurrent = msg.ToCharArray().ToList();

        // Tahmin edilen her karakter i�in durumlar� belirle
        for (var i = 0; i < listCurrent.Count; i++)
        {
            var currentChar = listCurrent[i];
            var contains = list.Contains(currentChar);
            if (contains)
            {
                var index = list.IndexOf(currentChar);
                var isCorrect = index == i;
                result.Add(isCorrect ? State.Correct : State.Contain);
                list[index] = ' '; // Karakterin tekrar bulunmas�n� engelle
            }
            else
            {
                result.Add(State.None);
            }
        }

        // Durumu None olanlar� Fail olarak g�ncelle
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
