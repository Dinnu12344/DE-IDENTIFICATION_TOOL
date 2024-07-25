using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL.CustomAction
{
    public class CustomDatePicker : DateTimePicker
    {
        public CustomDatePicker()
        {
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = " ";
            this.ValueChanged += CustomDateTimePicker_ValueChanged;
            this.MouseDown += CustomDateTimePicker_MouseDown;
        }

        private void CustomDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            this.CustomFormat = "dd/MM/yy";
        }

        private void CustomDateTimePicker_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.CustomFormat == " ")
            {
                this.CustomFormat = "dd/MM/yy";
                this.Focus();
                SendKeys.Send("{F4}"); // Open the dropdown

            }
        }
    }
}
