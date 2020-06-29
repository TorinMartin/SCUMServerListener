using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static int[] LoadPositions()
        {
            int[] pos = new int[2];
            pos[0] = 20;
            pos[1] = 20;

            var settings_json = LoadJSON();
            if (settings_json != "")
            {
                dynamic result = JsonConvert.DeserializeObject(settings_json);

                pos[0] = (int)result["settings"]["posx"];
                pos[1] = (int)result["settings"]["posy"];
            }

            return pos;
        }

        public static bool LoadWindowPref()
        {
            bool overlayAllWindows = false;
            var settings_json = LoadJSON();
            if (settings_json != "")
            {
                dynamic result = JsonConvert.DeserializeObject(settings_json);

                overlayAllWindows = (bool)result["settings"]["overlayAllWindows"];
            }
            return overlayAllWindows;
        }

        public static void SaveWindowPref(bool arg)
        {
            string json = LoadJSON();
            if (json != "")
            {
                JObject settingsJob = JObject.Parse(json);
                JObject settings = (JObject)settingsJob["settings"];

                settings["overlayAllWindows"] = arg.ToString();

                SaveSettings(settingsJob);
            }
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

        public static void SavePositions(int x, int y)
        {
            string json = LoadJSON();
            if (json != "")
            {
                JObject settingsJob = JObject.Parse(json);
                JObject settings = (JObject)settingsJob["settings"];

                settings["posx"] = x.ToString();
                settings["posy"] = y.ToString();

                SaveSettings(settingsJob);
            }
        }

        public static bool LoadTextPref()
        {
            bool disableBackground = false;
            var settings_json = LoadJSON();
            if (settings_json != "")
            {
                dynamic result = JsonConvert.DeserializeObject(settings_json);

                disableBackground = (bool)result["settings"]["disableBackground"];
            }
            return disableBackground;
        }

        public static void SaveTextPref(bool arg)
        {
            string json = LoadJSON();
            if (json != "")
            {
                JObject settingsJob = JObject.Parse(json);
                JObject settings = (JObject)settingsJob["settings"];

                settings["disableBackground"] = arg.ToString();

                SaveSettings(settingsJob);
            }
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
