using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SCUMServerListener
{
    public static class SettingsManager
    {
        public static string LoadJSON()
        {
            string settings_json = "";
            if (!File.Exists("settings.ini"))
            {
                MessageBox.Show("Settings file does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                settings_json = File.ReadAllText("settings.ini");
            }
            return settings_json;
        }

        public static string LoadDefault()
        {
            string id = "0";

            var settings_json = LoadJSON();
            if (settings_json != "")
            {
                dynamic result = JsonConvert.DeserializeObject(settings_json);

                id = result["settings"]["default_id"].ToString();
            }

            return id;
        }
        public static IDictionary<string, string> LoadAllSettings()
        {
            IDictionary<string, string> settings = new Dictionary<string, string>();

            var settings_json = LoadJSON();
            if (settings_json != "")
            {
                dynamic result = JsonConvert.DeserializeObject(settings_json);
                settings.Add("disableBackground", result["settings"]["disableBackground"].ToString());
                settings.Add("overlayAllWindows", result["settings"]["overlayAllWindows"].ToString());
                settings.Add("posx", result["settings"]["posx"].ToString());
                settings.Add("posy", result["settings"]["posy"].ToString());
                settings.Add("showName", result["settings"]["showName"].ToString());
                settings.Add("showPlayers", result["settings"]["showPlayers"].ToString());
                settings.Add("showTime", result["settings"]["showTime"].ToString());
                settings.Add("showPing", result["settings"]["showPing"].ToString());
                settings.Add("onlineColor", result["settings"]["onlineColor"].ToString());
                settings.Add("offlineColor", result["settings"]["offlineColor"].ToString());
                settings.Add("bgColor", result["settings"]["bgColor"].ToString());
            }

            return settings;
        }

        public static void SetDefault(string id)
        {
            string json = LoadJSON();
            if(json != "")
            {
                JObject settingsJob = JObject.Parse(json);
                JObject settings = (JObject)settingsJob["settings"];

                settings["default_id"] = id;

                SaveSettings(settingsJob);

                MessageBox.Show("Default Server Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }


        public static void SaveAllSettings(bool windowPref, bool textPref, int x, int y, bool showName, bool showPlayers, bool showTime, bool showPing, string onlineCol, string offlineCol, string bgCol)
        {
            var json = LoadJSON();
            if (!string.IsNullOrEmpty(json))
            {
                JObject settingsJob = JObject.Parse(json);
                JObject settings = (JObject)settingsJob["settings"];

                settings["overlayAllWindows"] = windowPref.ToString();
                settings["disableBackground"] = textPref.ToString();
                settings["posx"] = x.ToString();
                settings["posy"] = y.ToString();
                settings["showName"] = showName.ToString();
                settings["showPlayers"] = showPlayers.ToString();
                settings["showTime"] = showTime.ToString();
                settings["showPing"] = showPing.ToString();
                settings["onlineColor"] = onlineCol;
                settings["offlineColor"] = offlineCol;
                settings["bgColor"] = bgCol;

                SaveSettings(settingsJob);
            }
        }

        public static void SaveCoordinates(int x, int y)
        {
            var json = LoadJSON();
            if (string.IsNullOrEmpty(json)) return;

            JObject settingsJob = JObject.Parse(json);
            JObject settings = (JObject)settingsJob["settings"];

            settings["posx"] = x.ToString();
            settings["posy"] = y.ToString();

            SaveSettings(settingsJob);
        }

        public static void SaveSettings(JObject settings)
        {
            string path = @"settings.ini";
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                TextWriter tw = new StreamWriter(path);
                tw.Write(settings.ToString());
                tw.Close();
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("Path does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Unable to write to file!. Permission is denied!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
