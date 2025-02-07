using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEditor;

public abstract class DataTable
{
    public static readonly string FormatPath = "Tables/{0}";
    public static readonly string FormatTempPath = "Tables/Temp/{0}";

    public int currentVersion = 0;

    public abstract void Load(string filename);

    public static List<T> LoadCSV<T>(string csv)
    {
        using (var reader = new StringReader(csv))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csvReader.GetRecords<T>().ToList<T>();
        }
    }

    public static void SaveCSV<T>(List<T> list, string path)
    {
        using (var writer = new StreamWriter(path + ".csv"))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csvWriter.WriteRecords(list);
        }
    }

}
