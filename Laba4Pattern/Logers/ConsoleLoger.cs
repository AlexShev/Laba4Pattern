using System;
using System.Windows.Forms;

namespace library_laba4.Logers
{
    public class ConsoleLoger : ILoger
    {
        private System.Windows.Forms.TextBox textBox1;

        public ConsoleLoger(TextBox textBox1)
        {
            this.textBox1 = textBox1;
        }

        public void Log(string message)
        {
            try
            {
                if (textBox1?.InvokeRequired ?? false)
                textBox1?.Invoke(new Action(() =>
                {
                    textBox1.AppendText(message.Replace("\n", Environment.NewLine));
                    textBox1.AppendText(Environment.NewLine);
                }));
            else
                textBox1?.AppendText(message.Replace("\n", Environment.NewLine));
            }
            catch (Exception e)
            {
                textBox1 = null;
            }
        }

        public void Log()
        {
            try {
                if (textBox1?.InvokeRequired ?? false)
                    textBox1?.Invoke(new Action(() =>
                    {
                        textBox1.AppendText(Environment.NewLine);
                    }));
                else
                    textBox1?.AppendText(Environment.NewLine);
            } 
            catch (Exception e)
            {
                textBox1 = null;
            }
        }

        public void Close()
        {
            textBox1 = null;
        }
    }
}