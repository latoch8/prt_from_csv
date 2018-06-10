using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PRT_z_csv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Results Data = new Results();
        
        List<Results> ListFromFile = new List<Results>();
        List<Results> OnlyOneFamily = new List<Results>();
        List<Results> OnlyOneStepText = new List<Results>();

        private void loadFileButton_Click(object sender, EventArgs e) //przycisk wczytaj 
        {
            try
            {
                DataPreparation csv = new DataPreparation();
                ListFromFile = csv.LoadDataFromFile(Data);
                foreachList(ref familyList, Data.UniqueFamilyList(ListFromFile));
            }
            catch (Exception error)
            {
                MessageBox.Show("Ups... Coś poszło nie tak :( Wczytaj poprawny plik!\n" + error.ToString(), "Error");
            }
        }

        private void chooseFamily_ClickList(object sender, EventArgs e) 
        {
            OnlyOneFamily = Data.OneFamilyList(ListFromFile, familyList.Text);
            table.DataSource = OnlyOneFamily;
            foreachList(ref steptextList, Data.UniqueSteptextList(OnlyOneFamily));
        }

        private void chooseSteptext_ClickList(object sender, EventArgs e)
        {
            OnlyOneStepText = Data.OneSteptextList(OnlyOneFamily, steptextList);
            table.DataSource = OnlyOneStepText;
            foreachList(ref failList, Data.UniqueFailList(OnlyOneStepText));
            //top bledow
            Top5.DataSource = Data.TopFails(OnlyOneStepText, failList);
        }

        private void foreachList(ref ListBox list, string[] tablica) //drukuje tablice do Listboxow dla uzytkownika
        {
            list.Items.Clear();
            foreach (var item in tablica)
            {
                list.Items.Add(item);
            }
        }
        private void foreachList(ref CheckedListBox list, string[] tablica) //drukuje tablice do CheckedListBox dla uzytkownika
        {
            list.Items.Clear();
            foreach (var item in tablica)
            {
                list.Items.Add(item);
            }
        }

        private void chooseFail_ClickList(object sender, EventArgs e)
        {
            table.DataSource = Data.OnlyOneFail(OnlyOneStepText, failList.Text);
        }

        private void tabControl1_Click(object sender, EventArgs e) //przejscie do zakladki analiza lub powrot
        {
            if (ListFromFile.Count < 3)
            {
                tabControl1.SelectTab("Results");
                if (MessageBox.Show("Wczytaj poprawny plik!", "Error") == DialogResult.OK)
                {
                    loadFileButton_Click(sender, e);
                }
            }
            else
            {
                var analyze = new Analyzer(this);
                analyze.startAnalyse(OnlyOneFamily, failList);
                //dataGridView1.DataSource = Data.TopFailureWithEquipment(OnlyOneStepText, failList);
            }
            //if ()
            //{
            //    button1.Text = "zakladka1";
            //}
            //else
            //{
            //    button1.Text = "zakladka0";
            //}

            //tu dodac analize
        }

        public void report(string message)
        {
            //List<Results> newlist = new List<Results>();
            //if (list.GetType() == newlist.GetType())
            //{
            //    newlist = (List<Results>)list;
            //}

            //List<Results> newlist = (List<Results>)list;
            if (message == "cleare")
            {
                Report.Items.Clear();
            }
            else
            {
                Report.Items.Add(message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Analyzer analyze = new Analyzer(this);
            analyze.startAnalyse(OnlyOneFamily, failList);
        }
    }
}
