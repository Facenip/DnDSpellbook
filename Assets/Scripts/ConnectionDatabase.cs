using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using TMPro;
using System.IO;

public class ConnectionDatabase : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform parent;
    [SerializeField] TMP_InputField Name, LvL, Class, Rase;
    public SqliteConnection dbconnection;
    private string DBPath;
    private string fileName = "MainDB.s3db";

    private void Start() //Скрипт происходящий при запуске
    {
        tableConnection();
        tableReset();
    }


    private string GetDatabasePath() //Получение пути до базы данных
    {
        string filePath = Application.dataPath + "/" + fileName;
        if (!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
    }

    private void UnpackDatabase(string toPath) //Распаковка базы данных (Опционально для Android)
    {
        string fromPath = Path.Combine(Application.dataPath + "/Raw", fileName);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);
    }


    public void tableConnection() // Подключение бд
    {
        DBPath = GetDatabasePath();
        dbconnection = new SqliteConnection("URI=file:" + DBPath);
        dbconnection.Open();
    }

    public void tableReset() // Проход по бд и вывод элементов на экран
    {

        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        SqliteCommand cmd = dbconnection.CreateCommand();
        cmd.CommandText = "SELECT * FROM Character";

        SqliteDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            character.transform.Find("CharacterName").GetComponent<TextMeshProUGUI>().text = (string)reader[1];
            character.transform.Find("LvL").GetComponent<TextMeshProUGUI>().text = "Ур: " + (string)reader[4];
            character.transform.Find("CharacterClass").GetComponent<TextMeshProUGUI>().text = "Класс: " + (string)reader[2];
            character.transform.Find("CharacterRase").GetComponent<TextMeshProUGUI>().text = "Расса: " + (string)reader[3];
            Instantiate(character, parent);
           
        }
    }


    public void newCharacter() // Создание нового элемента в бд
    {
        IDbCommand cmdWrite = dbconnection.CreateCommand();
        cmdWrite.CommandText = "INSERT INTO Character (Name,Class,Rase,lvl) VALUES (\""+ Name.text + "\",\"" + Class.text + "\",\"" + Rase.text + "\",\"" + LvL.text + "\")";
        cmdWrite.ExecuteNonQuery();
        tableReset();
    }
}
