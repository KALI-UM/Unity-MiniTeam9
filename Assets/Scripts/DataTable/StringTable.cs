using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;
using Unity.Collections;

public class StringTable : DataTable
{
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAsset.text);
        dictionary.Clear();
        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.Id))
            {
                dictionary.Add(item.Id, item.String);
            }
            else
            {
                Debug.LogError($"키 중복: {item.Id}");
            }
        }
    }

    public string Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            return "키 없다.";
        }
        return dictionary[key];
    }
}
