using System.Drawing;
using System.Windows.Forms;

namespace Script;

internal class BoxForm2
{
    private readonly Form m_form;

    private readonly Label m_content;

    private readonly Button m_button1;

    private readonly Button m_button2;

    private string m_value1, m_value2;

    private string m_result;

    public BoxForm2()
    {
        m_form = new Form();

        m_form.Size = new Size(300, 200);

        m_form.StartPosition = FormStartPosition.CenterScreen;

        m_form.FormBorderStyle = FormBorderStyle.FixedSingle;

        m_form.ControlBox = false;

        m_form.TopMost = true;


        m_content = new Label();

        m_content.Text = "阿巴阿巴阿巴阿巴";

        m_content.Font = new Font("宋体", 14f, FontStyle.Regular);

        m_content.Size = new Size(270, 80);

        m_content.Location = new Point((m_form.Width - m_content.Width >> 1) + 5, 20);

        m_content.TextAlign = ContentAlignment.TopLeft;

        m_form.Controls.Add(m_content);


        m_button1 = new Button();

        m_button1.Text = "OK";

        m_button1.Font = new Font("宋体", 14f, FontStyle.Regular);

        m_button1.Size = new Size(120, 40);

        m_button1.Location = new Point((m_form.Width - m_button1.Width >> 1) - 70, m_form.Size.Height - 60);

        m_button1.Click += (_, _) =>
        {
            m_result = m_value1;

            m_form.Close();
        };

        m_form.Controls.Add(m_button1);


        m_button2 = new Button();

        m_button2.Text = "Cancel";

        m_button2.Font = new Font("宋体", 14f, FontStyle.Regular);

        m_button2.Size = new Size(120, 40);

        m_button2.Location = new Point((m_form.Width - m_button2.Width >> 1) + 70, m_form.Size.Height - 60);

        m_button2.Click += (_, _) =>
        {
            m_result = m_value2;

            m_form.Close();
        };


        m_form.Controls.Add(m_button2);
    }

    public string Show(string content, string text1, string value1, string text2, string value2)
    {
        m_content.Text = content;


        m_button1.Text = text1;

        m_value1 = value1;


        m_button2.Text = text2;

        m_value2 = value2;


        m_form.ShowDialog();

        return m_result;
    }
}