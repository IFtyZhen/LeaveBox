using System;
using System.Drawing;
using System.Windows.Forms;

namespace Script.Forms;

/// <summary>
/// 包含1个Label、1个TextBox、1~2个Button的小窗口
/// </summary>
public partial class InputForm
{
    private readonly int ButtonNumber;
    
    private Form _form;

    private Label _content;
    
    private TextBox _textBox;

    private Button _button1;

    private Button _button2;

    private string _result1;
    
    private string _result2;

    private string _result;

    public InputForm(int buttonNumber)
    {
        if (buttonNumber != 1 && buttonNumber != 2)
            throw new ArgumentException("buttonNumber必须等于1或者2");
        
        ButtonNumber = buttonNumber;
        
        _form = new Form();

        _form.Size = new Size(300, 240);

        _form.StartPosition = FormStartPosition.CenterScreen;

        _form.FormBorderStyle = FormBorderStyle.FixedSingle;

        _form.ControlBox = false;

        _form.TopMost = true;


        _content = new Label();

        _content.Font = new Font("宋体", 14f, FontStyle.Regular);

        _content.Size = new Size(270, 60);

        _content.Location = new Point((_form.Width - _content.Width >> 1) + 5, 20);

        _content.TextAlign = ContentAlignment.TopLeft;

        _form.Controls.Add(_content);
        
        
        _textBox = new TextBox();

        _textBox.Multiline = true;

        _textBox.Size = new Size(240, 77);
        
        _textBox.Font = new Font("宋体", 14f, FontStyle.Regular);

        _textBox.Location = new Point(30, _content.Location.Y + _content.Height + 10);
        
        
        _form.Controls.Add(_textBox);

        switch (ButtonNumber)
        {
            case 1:
                InitOneButton();
                break;
            case 2:
                InitTwoButton();
                break;
        }
    }
    
    public string Show(string content, ref string value, string text1, string result1, string text2 = default, string result2 = default)
    {
        _content.Text = content;

        _textBox.Text = value ?? "";

        if (ButtonNumber >= 1)
        {
            _button1.Text = text1;

            _result1 = result1;
        }

        if (ButtonNumber >= 2)
        {
            _button2.Text = text2;

            _result2 = result2;
        }


        _form.ShowDialog();

        value = _textBox.Text;

        return _result;
    }

    private Button AddButton()
    {
        var button = new Button();
        
        button.Font = new Font("宋体", 14f, FontStyle.Regular);
        
        button.Size = new Size(120, 40);
        
        _form.Controls.Add(button);

        return button;
    }
}