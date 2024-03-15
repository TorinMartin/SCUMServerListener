using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCUMServerListener.UI;

public partial class Gui
{
    private int _counter;
    
    private async void UpdateTimer_Tick(object? sender, EventArgs e) => await UpdateTimerTickAsync();
    
    private void UpdateTimer_Reset()
    {
        update_progbar.Value = 0;
        _counter = 0;
    }
    
    private async Task UpdateTimerTickAsync()
    {
        if (_server is null) await FetchAndUpdateAsync();
        if (_counter >= 30)
        {
            update_progbar.Value = 0;
            _counter = 0;
            await FetchAndUpdateAsync();
        }
        _counter++;
        update_progbar.Value = _counter;

        if (_overlayEnabled && _overlay is not null)
        {
            if (AppSettings.Instance.OverlayAllWindows) return;
            try
            {
                _overlay.SetWindowVisibility();
                if (_overlay.HasProcessExited())
                    StopOverlay();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                StopOverlay();
                if (ex.Message.Contains("Access is denied"))
                    MessageBox.Show("Please run as administrator in order for overlay to stick to SCUM game window!", "Access Denied!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}