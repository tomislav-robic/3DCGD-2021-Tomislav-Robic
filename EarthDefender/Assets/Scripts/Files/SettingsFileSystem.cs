using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class SettingsFileSystem
{
    public static void SaveSettings(SettingsManager settings)
    {
        SettingsFile settingsFile = new SettingsFile(settings);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsFile));
        string path = Path.Combine(Application.persistentDataPath, "settings.xml");
        try
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fs, settingsFile);
            fs.Close();
        } catch (Exception ex)
        {
            Debug.LogWarning($"{path} is inaccessible. {ex.GetType().Name}");
        }
    }

    public static SettingsFile LoadSettings()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsFile));
        string path = Path.Combine(Application.persistentDataPath, "settings.xml");
        if (File.Exists(path))
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                SettingsFile file = (SettingsFile)xmlSerializer.Deserialize(fs);
                fs.Close();
                return file;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"{path} is inaccessible. {ex.GetType().Name}");
            }
        }
        return new SettingsFile();
    }
}
