using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;
using Unity.Collections;

public class StringTable : DataTable
{
    public class StringData
    {
        public string String_Key { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<StringData>(textAsset.text);
        dictionary.Clear();
        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.String_Key))
            {
                dictionary.Add(item.String_Key, item.String);
            }
            else
            {
                Debug.LogError($"Å° Áßº¹: {item.String_Key}");
            }
        }
    }

    public string Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            KALLogger.Log("None Key", this);
            return "";
        }
        return dictionary[key];
    }
}
