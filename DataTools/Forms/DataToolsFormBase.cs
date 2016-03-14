using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Forms
{
    public class DataToolsFormBase : Form
    {
        protected Label StatusLabel { get; set; }
        protected void ShowStatus(string msg, Color color = default(Color))
        {
            if (color == default(Color)) color = Color.DarkBlue;
            StatusLabel.ForeColor = color;
            StatusLabel.Text = msg;
            Application.DoEvents();

        }

        protected void UiWrap(Action action, string context)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                StatusLabel.ForeColor = Color.DarkBlue;
                var result = ReturnValue.Wrap(action, context);
                ShowStatus(result, context);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception'{0} in UiTry.\r\n\r\nStack trace{1}:\r\n", ex.Message,
                    ex.StackTrace));
            }
            finally
            {

                Cursor = Cursors.Default;
            }
        }

        protected bool ShowStatus(ReturnValue result, string context)
        {
            if (!result.Success)
            {
                MessageBox.Show(result.ToString(), context,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowStatus(string.Format("{0} failed.", context), Color.Red);
                Debug.WriteLine(result);
            }
            ShowStatus(string.Format("{0} complete.", context));
            return result.Success;
        }

      
    }
}