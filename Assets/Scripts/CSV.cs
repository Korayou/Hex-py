using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Globalization; // Pour la g�n�ricit�

public class CSV<T>
{
    public string FilePath { get; private set; }
    public string Separator { get; set; } = ",";
    public string Encoding { get; set; } = "UTF-8";

    public CSV(string filePath)
    {
        FilePath = filePath;
        CreateFile();
    }

    // �criture d'une ligne dans le fichier CSV
    public void WriteLine(List<T> data)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true, System.Text.Encoding.GetEncoding(Encoding)))
            {
                List<string> stringData = new List<string>();
                foreach (var item in data)
                {
                    stringData.Add(item.ToString()); // Conversion en cha�ne
                }
                writer.WriteLine(string.Join(Separator, stringData));
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Erreur lors de l'�criture dans le fichier CSV : " + e.Message);
        }
    }

    // Lecture de toutes les lignes du fichier CSV
    public List<List<T>> ReadAllLines()
    {
        List<List<T>> data = new List<List<T>>();

        try
        {
            using (StreamReader reader = new StreamReader(FilePath, System.Text.Encoding.GetEncoding(Encoding)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(Separator.ToCharArray());
                    List<T> row = new List<T>();
                    foreach (string value in values)
                    {
                        try
                        {
                            // Conversion du string vers le type T
                            T convertedValue = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
                            row.Add(convertedValue);
                        }
                        catch (Exception)
                        {
                            Debug.LogError("Erreur lors de la conversion de la valeur : " + value + " vers le type " + typeof(T).Name);
                            return null; // Ou g�rer l'erreur diff�remment
                        }
                    }
                    data.Add(row);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Erreur lors de la lecture du fichier CSV : " + e.Message);
        }

        return data;
    }

    // Lecture d'une ligne sp�cifique du fichier CSV (index�e � partir de 0)
    public List<T> ReadLine(int lineNumber)
    {
        List<T> lineData = null;
        List<List<T>> allLines = ReadAllLines();

        if (allLines != null && lineNumber >= 0 && lineNumber < allLines.Count)
        {
            lineData = allLines[lineNumber];
        }
        else
        {
            Debug.LogWarning("Num�ro de ligne invalide : " + lineNumber);
        }

        return lineData;
    }

    // V�rifie si le fichier existe
    public bool FileExists()
    {
        return File.Exists(FilePath);
    }

    // Cr�e le fichier s'il n'existe pas
    public void CreateFile()
    {
        if (!FileExists())
        {
            try
            {
                string directoryPath = Path.GetDirectoryName(FilePath);

                // Cr�ez le dossier s'il n'existe pas
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                File.Create(FilePath).Close();
            }
            catch (Exception e)
            {
                Debug.LogError("Erreur lors de la cr�ation du fichier CSV : " + e.Message);
            }
        }
    }

    //Supprime le fichier
    public void DeleteFile()
    {
        if (FileExists())
        {
            try
            {
                File.Delete(FilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Erreur lors de la suppression du fichier CSV : " + e.Message);
            }
        }
    }
}
