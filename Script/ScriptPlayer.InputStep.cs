using Script.Forms;
using Script.Nodes;

namespace Script;

public partial class ScriptPlayer
{
    private InputForm m_inputForm1;

    private InputForm m_inputForm2;

    private InputForm InputForm1 => m_inputForm1 ??= new InputForm(1);

    private InputForm InputForm2 => m_inputForm2 ??= new InputForm(2);

    private string PlayInputStep(InputStep step)
    {
        // 输入框的结果
        var value = default(string);

        var cases = default(InputStep.Case[]);

        var next = default(string);

        switch (step.Buttons.Length)
        {
            case >= 2:
            {
                InputStep.Button b1 = step.Buttons[0];

                InputStep.Button b2 = step.Buttons[1];

                value = step.Value;

                string result = InputForm2.Show(step.Content, ref value, b1.Text, "1", b2.Text, "2");

                switch (result)
                {
                    case "1":
                    {
                        cases = b1.Cases;

                        next = b1.Next;

                        break;
                    }
                    case "2":
                    {
                        cases = b2.Cases;

                        next = b2.Next;

                        break;
                    }
                }

                break;
            }
            case >= 1:
            {
                InputStep.Button b1 = step.Buttons[0];

                value = step.Value;

                cases = b1.Cases;

                next = InputForm1.Show(step.Content, ref value, b1.Text, b1.Next);

                break;
            }
        }

        if (cases == null)
            return next;

        foreach (InputStep.Case c in cases)
        {
            if (c.Value == value)
                return c.Value;
        }

        return next;
    }
}