using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem
{
    public enum Language
    {
        Turkish,
        English,
    }

    public static Language language = Language.Turkish;

    private static Dictionary<string, string> localisedTR;
    private static Dictionary<string, string> localisedEN;

    public static bool isInit;

    public static CSVLoader csvLoader;

    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionaries();

        isInit = true;
    }

    public static void UpdateDictionaries()
    {
        localisedTR = csvLoader.GetDictionaryValues("tr");
        localisedEN = csvLoader.GetDictionaryValues("en");
    }

    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.Turkish:
                localisedTR.TryGetValue(key, out value);
                break;
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
        }

        return value;
    }

    public static Dictionary<string, string> GetDictionaryForEditor() {
        if (!isInit) { Init(); }
        return localisedTR;
    }

    public static void Add(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Replace(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Edit(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Remove(string key)
    {

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }
}
