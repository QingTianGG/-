using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 正则提取
{
    public partial class Form1 : Form
    {
        string s = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                {
                    s = sr.ReadToEnd();
                }
            }

            var m1 = Regex.Matches(s, "<h4 class=\"title\">\\s*?\\S*?([0-9]{1,3})\\S*?\\s*?<span class=\"pull-right\">", RegexOptions.Multiline);
            var m2 = Regex.Matches(s, "<div class=\"choice\">\\s*?</a>(<p>)?([\\s\\S]*?)(</p>)?\\s*?</div>", RegexOptions.Multiline);
            //var m3 = Regex.Matches(s, "<div class=\"choice\">\\s*?<p>([\\s\\S]*?)</p>\\s*?<p>([\\s\\S]*?)</p>\\s*?<p>([\\s\\S]*?)</p>\\s*?<p>([\\s\\S]*?)</p>\\s*?</div>", RegexOptions.Multiline);
            var m3 = Regex.Matches(s, "<div class=\"choice\">\\s*?(<p>[\\s| ]*?([\\S| ]*?)[\\s| ]*?(</p>)?)+\\s*?</div>", RegexOptions.Multiline);
            var m4 = Regex.Matches(s, "正确答案：</td>\\s*?<td>([\\s\\S]*?)</td>", RegexOptions.Multiline);

            if (m1.Count == m2.Count && m2.Count == m3.Count && m3.Count == m4.Count)
            {
                for (int i = 0; i < m1.Count; i++)
                {
                    textBox2.AppendText(m1[i].Groups[1].Value + "ぁ");
                    textBox2.AppendText(m2[i].Groups[2].Value + "ぁ");
                    //textBox2.AppendText(m3[i].Groups[1].Value + "&&" + m3[i].Groups[2].Value + "&&");
                    for (int j = 0; j < m3[i].Groups[2].Captures.Count; j++)
                        textBox2.AppendText(m3[i].Groups[2].Captures[j].ToString().Trim() + "ぁ");
                    textBox2.AppendText(m4[i].Groups[1].Value + "\r\n");
                }
            }
        }
    }
}
