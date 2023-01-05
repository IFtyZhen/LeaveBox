using System.Drawing;

namespace Script.Forms;

internal partial class BoxForm
{
    private void InitOneButton()
    {
        _button1 = AddButton();

        _button1.Location = new Point(_form.Width - _button1.Width >> 1, _form.Size.Height - 60);

        _button1.Click += (_, _) =>
        {
            _result = _value1;

            _form.Close();
        };
    }
}