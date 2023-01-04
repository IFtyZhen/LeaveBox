using System.Drawing;
using System.Windows.Forms;

namespace Script;

/// <summary>
/// 包含1个Label和1个Button的小窗口
/// </summary>
public class BoxForm1
{
    private readonly Form _form;

    private readonly Label _content;

    private readonly Button _button;

    public BoxForm1()
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


        _button = new Button();

        _button.Text = "OK";

        _button.Font = new Font("宋体", 12f, FontStyle.Regular);

        _button.Size = new Size(80, 40);

        _button.Location = new Point(_form.Width - _button.Width >> 1, _form.Size.Height - 60);


        _button.Click += (_, _) => { _form.Close(); };

        _form.Controls.Add(_button);
    }

    public void Show(string content, string button)
    {
        _content.Text = content;

        _button.Text = button;

        _form.ShowDialog();
    }
}
