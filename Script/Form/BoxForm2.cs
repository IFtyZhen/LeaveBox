using System.Drawing;
using System.Windows.Forms;

namespace Script;

/// <summary>
/// 包含1个Label和2个Button的小窗口
/// </summary>
internal class BoxForm2
{
    private readonly Form _form;

    private readonly Label _content;

    private readonly Button _button1;

    private readonly Button _button2;

    private string _value1;
    
    private string _value2;

    private string _result;

    public BoxForm2()
    {
        _form = new Form();

        _form.Size = new Size(300, 200);

        _form.StartPosition = FormStartPosition.CenterScreen;

        _form.FormBorderStyle = FormBorderStyle.FixedSingle;

        _form.ControlBox = false;

        _form.TopMost = true;


        _content = new Label();

        _content.Text = "阿巴阿巴阿巴阿巴";

        _content.Font = new Font("宋体", 14f, FontStyle.Regular);

        _content.Size = new Size(270, 80);

        _content.Location = new Point((_form.Width - _content.Width >> 1) + 5, 20);

        _content.TextAlign = ContentAlignment.TopLeft;

        _form.Controls.Add(_content);


        _button1 = new Button();

        _button1.Text = "OK";

        _button1.Font = new Font("宋体", 14f, FontStyle.Regular);

        _button1.Size = new Size(120, 40);

        _button1.Location = new Point((_form.Width - _button1.Width >> 1) - 70, _form.Size.Height - 60);

        _button1.Click += (_, _) =>
        {
            _result = _value1;

            _form.Close();
        };

        _form.Controls.Add(_button1);


        _button2 = new Button();

        _button2.Text = "Cancel";

        _button2.Font = new Font("宋体", 14f, FontStyle.Regular);

        _button2.Size = new Size(120, 40);

        _button2.Location = new Point((_form.Width - _button2.Width >> 1) + 70, _form.Size.Height - 60);

        _button2.Click += (_, _) =>
        {
            _result = _value2;

            _form.Close();
        };


        _form.Controls.Add(_button2);
    }

    public string Show(string content, string text1, string value1, string text2, string value2)
    {
        _content.Text = content;


        _button1.Text = text1;

        _value1 = value1;


        _button2.Text = text2;

        _value2 = value2;


        _form.ShowDialog();

        return _result;
    }
}