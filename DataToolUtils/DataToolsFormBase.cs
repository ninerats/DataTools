using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataToolUtils
{
    public class DataToolsFormBase : Form
    {
        protected void ShowStatus(string msg, Color color = default(Color))
        {
            if (color == default(Color)) color = Color.DarkBlue;
            StatusLabel.ForeColor = color;
            StatusLabel.Text = msg;
            Application.DoEvents();

        }

        protected void UiWrap(Action action, string context)
        {
            StatusLabel.ForeColor = Color.DarkBlue;
            StatusLabel.Text = context + "...";
            var result = ReturnValue.Wrap(action);
            ShowStatus(result, context);
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

        protected Label StatusLabel { get; set; }
    }
}