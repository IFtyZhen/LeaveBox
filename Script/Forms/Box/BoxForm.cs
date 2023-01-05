using System;
using System.Drawing;
using System.Windows.Forms;

namespace Script.Forms;

/// <summary>
/// 包含1个Label、1~2个Button的小窗口
/// </summary>
internal partial class BoxForm
{
    private readonly int ButtonNumber;
    
    private Form _form;

    private Label _content;

    private Button _button1;

    private Button _button2;

    private string _value1;
    
    private string _value2;

    private string _result;

    public BoxForm(int buttonNumber)
    {
        if (buttonNumber != 1 && buttonNumber != 2)
            throw new ArgumentException("buttonNumber必须等于1或者2");
        
        ButtonNumber = buttonNumber;
        
        _form = new Form();

        _form.Size = new Size(300, 200);

        _form.StartPosition = FormStartPosition.CenterScreen;

        _form.FormBorderStyle = FormBorderStyle.FixedSingle;

        _form.ControlBox = false;

        _form.TopMost = true;


        _content = new Label();

        _content.Font = new Font("宋体", 14f, FontStyle.Regular);

        _content.Size = new Size(270, 80);

        _content.Location = new Point((_form.Width - _content.Width >> 1) + 5, 20);

        _content.TextAlign = ContentAlignment.TopLeft;

        _form.Controls.Add(_content);

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
    
    public string Show(string content, string text1, string value1, string text2 = default, string value2 = default)
    {
        _content.Text = content;

        if (ButtonNumber >= 1)
        {
            _button1.Text = text1;

            _value1 = value1;
        }

        if (ButtonNumber >= 2)
        {
            _button2.Text = text2;

            _value2 = value2;
        }


        _form.ShowDialog();

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