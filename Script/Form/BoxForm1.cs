using System.Drawing;
using System.Windows.Forms;

namespace Script;

public class BoxForm1
{
    private readonly Form m_form;

    private readonly Label m_content;

    private readonly Button m_button;

    public BoxForm1()
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


        m_button = new Button();

        m_button.Text = "OK";

        m_button.Font = new Font("宋体", 12f, FontStyle.Regular);

        m_button.Size = new Size(80, 40);

        m_button.Location = new Point(m_form.Width - m_button.Width >> 1, m_form.Size.Height - 60);


        m_button.Click += (_, _) => { m_form.Close(); };

        m_form.Controls.Add(m_button);
    }

    public void Show(string content, string button)
    {
        m_content.Text = content;

        m_button.Text = button;

        m_form.ShowDialog();
    }
}
