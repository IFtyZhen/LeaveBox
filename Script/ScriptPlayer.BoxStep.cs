using Script.Forms;
using Script.Nodes;

namespace Script;

public partial class ScriptPlayer
{
    private BoxForm m_boxForm1;

    private BoxForm m_boxForm2;

    private BoxForm BoxForm1 => m_boxForm1 ??= new BoxForm(1);

    private BoxForm BoxForm2 => m_boxForm2 ??= new BoxForm(2);

    private string PlayBoxStep(BoxStep step)
    {
        switch (step.Buttons.Length)
        {
            case >= 2:
            {
                BoxStep.Button b1 = step.Buttons[0];

                BoxStep.Button b2 = step.Buttons[1];

                return BoxForm2.Show(step.Content, b1.Text, b1.Next, b2.Text, b2.Next);
            }
            case >= 1:
            {
                BoxStep.Button b = step.Buttons[0];

                BoxForm1.Show(step.Content, b.Text, b.Next);

                return b.Next;
            }
        }

        return default;
    }
}