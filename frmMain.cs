using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using BulkRegEx.Classes;
using System.IO;
using System.Diagnostics;

namespace BulkRegEx
{
    public partial class frmMain : Form
    {
        private Config config;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            if(File.Exists("config.json"))
            {
                try
                {
                    this.config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(string.Format("Error: Could not load configuration file (config.json).{0}{1}", (Environment.NewLine + Environment.NewLine), ex.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            } else
            {
                MessageBox.Show("Error: Could not load configuration file (config.json).","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string[] filepaths = Directory.EnumerateFiles("files/", "*.*", SearchOption.AllDirectories).ToArray<string>();

            foreach(string filepath in filepaths)
            {
                string contents = File.ReadAllText(filepath);

                foreach (MatchReplace matchReplace in this.config.MatchReplaces)
                {
                    RegexOptions regexOptions = new RegexOptions();

                    if (matchReplace.IgnoreCase) regexOptions |= RegexOptions.IgnoreCase;
                    if (matchReplace.MultiLine) regexOptions |= RegexOptions.Multiline;

                    contents = Regex.Replace(contents, matchReplace.Match, matchReplace.Replace, regexOptions);
                }

                // need just the filename
                string filename = Regex.Replace(filepath, ".*/(.*)$", "$1");

                string outputPath = filepath;
                if (config.CreateNewFiles) outputPath = this.config.OutputPath + filename;

                File.WriteAllText(outputPath, contents);

                MessageBox.Show("Operations Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
