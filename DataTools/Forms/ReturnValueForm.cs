using System.Windows.Forms;
using Craftsmaneer.Lang;

namespace Craftsmaneer.DataTools.Forms
{
    public partial class ReturnValueForm : Form
    {
        public ReturnValueForm()
        {
            InitializeComponent();
        }

        private ReturnValue ClearFields()
        {
            return ReturnValue.Wrap(() =>
            {
                txtErrorCode.Text = "N/A";
                txtErrorDetails.Text = "N/A";
                txtContext.Text = "";
                txtSuccess.Text = "";
                txtValueType.Text = "N/A";
                txtValueAsString.Text = "N/A";

            });
        }

        public static ReturnValue Show(ReturnValue r)
        {
            return ReturnValue.Wrap(() =>
            {
                var form = new ReturnValueForm();
              
                form.SetFields(r);
                form.tabReturnValue.SelectTab(r.Success ? form.pagValue : form.pagError);
                form.ShowDialog();
            });

        }

        private void SetFields(ReturnValue r)
        {
            ClearFields();
            txtSuccess.Text = r.Success ? "True" : "False";
            txtErrorDetails.Text = r.ToString();
            txtContext.Text = r.Context;
            

        }

        private void SetFields<T>(ReturnValue<T> r)
        {
            SetFields(r as ReturnValue);
            txtValueAsString.Text = r.Value.ToString();
            txtValueType.Text = r.Value.GetType().Name;
        }

        private void SetFields<TValue, TCode>(ReturnValue<TValue, TCode> r)
        {SetFields(r as ReturnValue<TValue>);
            txtErrorCode.Text = r.ErrorCode.ToString();
        }
    }

   
}
