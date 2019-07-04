﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace later_list
{
    public partial class settings : Form
    {
        #region variables
        string fileName;
        string filePath;
        public Color lightBC = Color.FromArgb(242,242,242);
        public Color darkBC = Color.FromArgb(51,51,51);
        public Color lightTC = Color.FromArgb(242,242,242);
        public Color darkTC = Color.FromArgb(31, 31, 31);
        #endregion

        #region settings opened
        public settings()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        private Form mainForm = null;
        public settings(Form settings)
        {
            mainForm = settings as settings;
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            loadTheme();
        }

        public void loadTheme()
        {
            ThemeManager.registerForm(this);

            ThemeManager.registerGB(settings_gb);
            ThemeManager.registerGB(themes_gb);

            ThemeManager.registerTB(movie_path_tb);
            ThemeManager.registerTB(serie_path_tb);
            ThemeManager.registerTB(book_path_tb);

            ThemeManager.registerButton(save_settings_button);
        }
        #endregion

        #region settings closed
        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (save_settings_button.Enabled == true)
            {
                DialogResult confirm = MessageBox.Show("Unsaved settings will be lost. Continue?", "Exit",
                                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (confirm == DialogResult.OK)
                {
                    save_settings_button.Enabled = false;
                    getSettings();
                    if (Properties.Settings.Default.light_checked == true)
                    {
                        light_rb.Checked = true;
                        dark_rb.Checked = false;
                        ThemeManager.setAllBackcolors(lightBC, darkTC, darkTC);
                    }
                    if (Properties.Settings.Default.dark_checked == true)
                    {
                        dark_rb.Checked = true;
                        light_rb.Checked = false;
                        ThemeManager.setAllBackcolors(darkBC, lightTC, darkTC);
                    }
                }
                else if (confirm == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region settings option variables
        public string MovieTextBoxText
        {
            get { return movie_path_tb.Text; }
            set { movie_path_tb.Text = value; }
        }

        public string SerieTextBoxText
        {
            get { return serie_path_tb.Text; }
            set { serie_path_tb.Text = value; }
        }

        public string BookTextBoxText
        {
            get { return book_path_tb.Text; }
            set { book_path_tb.Text = value; }
        }

        public bool LightThemeCheck
        {
            get { return light_rb.Checked; }
            set { light_rb.Checked = value; }
        }

        public bool DarkThemeCheck
        {
            get { return dark_rb.Checked; }
            set { dark_rb.Checked = value; }
        }
        #endregion 

        #region settings datas
        public void getSettings()
        {
            movie_path_tb.Text = Properties.Settings.Default.movie_path;
            serie_path_tb.Text = Properties.Settings.Default.serie_path;
            book_path_tb.Text = Properties.Settings.Default.book_path;
        }

        public void saveSettings()
        {
            Properties.Settings.Default.movie_path = movie_path_tb.Text;
            Properties.Settings.Default.serie_path = serie_path_tb.Text;
            Properties.Settings.Default.book_path = book_path_tb.Text;
            if (light_rb.Checked)
            {
                Properties.Settings.Default.light_checked = true;
                Properties.Settings.Default.dark_checked = false;
            }

            if (dark_rb.Checked)
            {
                Properties.Settings.Default.dark_checked = true;
                Properties.Settings.Default.light_checked = false;
            }
            save_settings_button.Enabled = false;
            Properties.Settings.Default.Save();

            //check settings form is already open
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == "settings")
                {
                    this.Close(); //if it's, close it
                }
            }
        }
        #endregion

        #region settings input fields
        private void pathChanged(object sender, EventArgs e)
        {
            if (movie_path_tb.Text == "")
            {
                clear_movie_path_button.Enabled = false;
            }
            else if (movie_path_tb.Text != "")
            {
                clear_movie_path_button.Enabled = true;
            }

            if (serie_path_tb.Text == "")
            {
                clear_serie_path_button.Enabled = false;
            }
            else if (serie_path_tb.Text != "")
            {
                clear_serie_path_button.Enabled = true;
            }

            if (book_path_tb.Text == "")
            {
                clear_book_path_button.Enabled = false;
            }
            else if (book_path_tb.Text != "")
            {
                clear_book_path_button.Enabled = true;
            }
        }
        #endregion

        #region settings buttons
        private void setFileName(string whichButton, string fileName)
        {
            switch (whichButton)
            {
                case "open_movie_path_button":
                    movie_path_tb.Text = fileName;
                    break;
                case "open_serie_path_button":
                    serie_path_tb.Text = fileName;
                    break;
                case "open_book_path_button":
                    book_path_tb.Text = fileName;
                    break;
            }
        }

        private void detectButton(string whichButton)
        {
            switch (whichButton)
            {
                case "open_movie_path_button":
                    filePath = @Properties.Settings.Default.movie_path;
                    fileName = "movielist";
                    break;
                case "open_serie_path_button":
                    filePath = @Properties.Settings.Default.serie_path;
                    fileName = "serielist";
                    break;
                case "open_book_path_button":
                    filePath = @Properties.Settings.Default.book_path;
                    fileName = "booklist";
                    break;
            }
        }
        private void openPath(object sender, EventArgs e)
        {
            detectButton(((Button)sender).Name);
            openfiledialog.InitialDirectory = filePath;
            openfiledialog.RestoreDirectory = true;
            openfiledialog.FileName = fileName;
            openfiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openfiledialog.FilterIndex = 0;
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                setFileName(((Button)sender).Name, openfiledialog.FileName);
                save_settings_button.Enabled = true;
            }
            else
            {
                getSettings();
                save_settings_button.Enabled = false;
            }
        }

        private void clearPath(object sender, EventArgs e)
        {
            save_settings_button.Enabled = true;
            switch (((Button)sender).Name)
            {
                case "clear_movie_path_button":
                    movie_path_tb.Text = "";
                    break;
                case "clear_serie_path_button":
                    serie_path_tb.Text = "";
                    break;
                case "clear_book_path_button":
                    book_path_tb.Text = "";
                    break;
            }
        }

        public void theme_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (light_rb.Checked)
            {
                ThemeManager.setAllBackcolors(lightBC, darkTC, darkTC);
                if (Properties.Settings.Default.dark_checked == true)
                {
                    save_settings_button.Enabled = true;
                }
            }
            else if (dark_rb.Checked)
            {
                ThemeManager.setAllBackcolors(darkBC, lightTC, darkTC);
                if (Properties.Settings.Default.light_checked == true)
                {
                    save_settings_button.Enabled = true;
                }
            }
        }

        private void save_settings_button_Click(object sender, EventArgs e)
        {
            saveSettings();
            MessageBox.Show("Settings Saved!");
        }

        private void save_settings_button_EnabledChanged(object sender, EventArgs e)
        {
            if (save_settings_button.Enabled == true)
            {
                settings_error_provider.SetError(save_settings_button, "There're unsaved settings!");
            }
            if (save_settings_button.Enabled == false)
            {
                settings_error_provider.Clear();
            }
        }
        #endregion

    }

    #region Theme Manager
    public static class ThemeManager
    {
        //FORMS
        private static List<Form> formList = new List<Form>();

        public static void registerForm(Form form)
        {
            if (!formList.Contains(form)) formList.Add(form);
        }

        public static void unRegisterForm(Form form)
        {
            if (formList.Contains(form)) formList.Remove(form);
        }

        //GROUP BOX
        private static List<GroupBox> gbList = new List<GroupBox>();

        public static void registerGB(GroupBox gb)
        {
            if (!gbList.Contains(gb)) gbList.Add(gb);
        }

        public static void unRegisterGB(GroupBox gb)
        {
            if (gbList.Contains(gb)) gbList.Remove(gb);
        }

        //TEXTBOX
        private static List<TextBox> tbList = new List<TextBox>();

        public static void registerTB(TextBox tb)
        {
            if (!tbList.Contains(tb)) tbList.Add(tb);
        }

        public static void unRegisterTB(TextBox tb)
        {
            if (tbList.Contains(tb)) tbList.Remove(tb);
        }

        //COMBOBOX
        private static List<ComboBox> cbList = new List<ComboBox>();

        public static void registerCB(ComboBox cb)
        {
            if (!cbList.Contains(cb)) cbList.Add(cb);
        }

        public static void unRegisterCB(ComboBox cb)
        {
            if (cbList.Contains(cb)) cbList.Remove(cb);
        }

        //LISTS
        private static List<ListBox> lbList = new List<ListBox>();

        public static void registerLB(ListBox lb)
        {
            if (!lbList.Contains(lb)) lbList.Add(lb);
        }

        public static void unRegisterLB(ListBox lb)
        {
            if (lbList.Contains(lb)) lbList.Remove(lb);
        }

        //BUTTONS
        private static List<Button> buttonList = new List<Button>();

        public static void registerButton(Button btn)
        {
            if (!buttonList.Contains(btn)) buttonList.Add(btn);
        }

        public static void unRegisterButton(Button btn)
        {
            if (buttonList.Contains(btn)) buttonList.Remove(btn);
        }

        //SET ALL COLORS
        public static void setAllBackcolors(Color BackColor, Color TextColor, Color ButtonTextColor)
        {
            foreach (Form f in formList) if (f != null) f.BackColor = BackColor;
            foreach (GroupBox gb in gbList) if (gb != null) gb.ForeColor = TextColor;
            foreach (TextBox tb in tbList) if (tb != null) tb.ForeColor = TextColor;
            foreach (TextBox tb in tbList) if (tb != null) tb.BackColor = BackColor;
            foreach (ComboBox cb in cbList) if (cb != null) cb.ForeColor = TextColor;
            foreach (ComboBox cb in cbList) if (cb != null) cb.BackColor = BackColor;
            foreach (ListBox lb in lbList) if (lb != null) lb.ForeColor = TextColor;
            foreach (ListBox lb in lbList) if (lb != null) lb.BackColor = BackColor;
            foreach (Button b in buttonList) if (b != null) b.ForeColor = ButtonTextColor;
        }
    }
    #endregion
}
