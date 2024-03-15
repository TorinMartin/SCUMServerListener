using System;
using System.Linq;
using System.Windows.Forms;

namespace SCUMServerListener.UI;

public partial class Gui
{
    private void SetDft_Btn_Click(object sender, EventArgs e)
    {
        AppSettings.Instance.DefaultServerId = _server?.Data.Id ?? AppSettings.Instance.DefaultServerId;
        if (!Configuration.Save(AppSettings.Instance))
        {
            MessageBox.Show("Unable to save default server!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        MessageBox.Show("Default Server Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
        
    private async void Btn_Overlay_Click(object sender, EventArgs e)
    {
        await ToggleOverlayAsync();
    }

    private void OverlaySettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _settingsForm = new SettingsForm(_overlay);
        _settingsForm.Show();
    }

    private void Btn_Drag_Overlay_Click(object sender, EventArgs e)
    {
        if (_overlay is null || !_overlayEnabled) return;

        Toggle_Overlay_Btn(false);
        _overlay.DragOverlay();
    }
        
    private async void SearchButton_Click(object sender, EventArgs e)
    {
        var query = searchbox.Text;
            
        var results = await SearchAsync(query);
        if (results.Any() is false)
        {
            MessageBox.Show("Search returned no results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _server = await IterateResultsAsync(results);
        await FetchAndUpdateAsync();
        UpdateTimer_Reset();
    }
}