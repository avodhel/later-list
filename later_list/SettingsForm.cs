﻿using System;
using System.Windows.Forms;

namespace later_list
{
    public partial class SettingsForm : Form
    {
        #region Variables

        private string filePath;
        private SettingsViewHandler sViewHandler;

        #endregion

        #region Properties

        public GroupBox SettingsGB
        {
            get { return settings_gb; }
            set { settings_gb = value; }
        }

        public GroupBox ThemesGB
        {
            get { return themes_gb; }
            set { themes_gb = value; }
        }

        public TextBox MovieFilePathTextBox
        {
            get { return movie_path_tb; }
            set { movie_path_tb = value; }
        }

        public TextBox SerieFilePathTextBox
        {
            get { return serie_path_tb; }
            set { serie_path_tb = value; }
        }

        public TextBox BookFilePathTextBox
        {
            get { return book_path_tb; }
            set { book_path_tb = value; }
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

        public Button SaveSettingsButton
        {
            get { return save_settings_button; }
            set { save_settings_button = value; }
        }

        #endregion 

        #region Constructors

        public SettingsForm()
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            sViewHandler = new SettingsViewHandler(this);
            sViewHandler.LoadTheme();
        }

        private void SettingsLoad(object sender, EventArgs e)
        {

        }

        #endregion

        #region Settings Form Close

        private void SettingsFormClosing(object sender, FormClosingEventArgs e)
        {
            if (save_settings_button.Enabled == true)
            {
                DialogResult confirm = MessageBox.Show("Unsaved settings will be lost. Continue?", "Exit",
                                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (confirm == DialogResult.OK)
                {
                    save_settings_button.Enabled = false;
                    GetAllFilePathsFromProperties();
                    ThemeHandler.CurrrentTheme(this);
                }
                else if (confirm == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region Save Settings

        public void SaveSettings()
        {
            SetAllFilePathsToProperties();
            SetThemeOption();
            save_settings_button.Enabled = false;
            Properties.Settings.Default.Save();
            SettingsFormState();
        }

        #endregion

        #region Settings Form State

        private void SettingsFormState()
        {
            //check settings form is already open
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == this.Name)
                {
                    this.Close(); //if it's open, close it
                }
            }
        }

        #endregion

        #region Input Fields

        private void PathChanged(object sender, EventArgs e)
        {
            if (MovieFilePathTextBox.Text == string.Empty)
            {
                clear_movie_path_button.Enabled = false;
            }
            else if (MovieFilePathTextBox.Text != string.Empty)
            {
                clear_movie_path_button.Enabled = true;
            }

            if (SerieFilePathTextBox.Text == string.Empty)
            {
                clear_serie_path_button.Enabled = false;
            }
            else if (SerieFilePathTextBox.Text != string.Empty)
            {
                clear_serie_path_button.Enabled = true;
            }

            if (BookFilePathTextBox.Text == string.Empty)
            {
                clear_book_path_button.Enabled = false;
            }
            else if (BookFilePathTextBox.Text != string.Empty)
            {
                clear_book_path_button.Enabled = true;
            }
        }

        #endregion

        #region File Paths

        public void GetAllFilePathsFromProperties()
        {
            MovieFilePathTextBox.Text = Properties.Settings.Default.movie_path;
            SerieFilePathTextBox.Text = Properties.Settings.Default.serie_path;
            BookFilePathTextBox.Text = Properties.Settings.Default.book_path;
        }

        private void SetAllFilePathsToProperties()
        {
            Properties.Settings.Default.movie_path = MovieFilePathTextBox.Text;
            Properties.Settings.Default.serie_path = SerieFilePathTextBox.Text;
            Properties.Settings.Default.book_path = BookFilePathTextBox.Text;
        }

        public void SetFilePath(MainForm.Sections section, string _filePath)
        {
            if (section == MainForm.Sections.Movie) MovieFilePathTextBox.Text = _filePath;
            if (section == MainForm.Sections.Serie) SerieFilePathTextBox.Text = _filePath;
            if (section == MainForm.Sections.Book)  BookFilePathTextBox.Text = _filePath;
        }

        private void GetFilePath(MainForm.Sections section)
        {
            if (section == MainForm.Sections.Movie) filePath = @Properties.Settings.Default.movie_path;
            if (section == MainForm.Sections.Serie) filePath = @Properties.Settings.Default.serie_path;
            if (section == MainForm.Sections.Book)  filePath = @Properties.Settings.Default.book_path;
        }

        private void OpenPath(object sender, EventArgs e)
        {
            GetFilePath(DetectSectionFromButton(((Button)sender).Name));
            openfiledialog.InitialDirectory = filePath;
            openfiledialog.RestoreDirectory = true;
            openfiledialog.FileName = DetectSectionFromButton(((Button)sender).Name) + "list";
            openfiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openfiledialog.FilterIndex = 0;
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                SetFilePath(DetectSectionFromButton(((Button)sender).Name), openfiledialog.FileName);
                save_settings_button.Enabled = true;
            }
            else
            {
                GetAllFilePathsFromProperties();
                save_settings_button.Enabled = false;
            }
        }

        private void ClearPath(object sender, EventArgs e)
        {
            save_settings_button.Enabled = true;
            switch (((Button)sender).Name)
            {
                case "clear_movie_path_button":
                    MovieFilePathTextBox.Text = string.Empty;
                    break;
                case "clear_serie_path_button":
                    SerieFilePathTextBox.Text = string.Empty;
                    break;
                case "clear_book_path_button":
                    BookFilePathTextBox.Text = string.Empty;
                    break;
            }
        }

        #endregion

        #region Button Actions

        private void SaveSettingsButtonClick(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("Settings Saved!");
        }

        private void SaveSettingsButtonEnabledChanged(object sender, EventArgs e)
        {
            if (save_settings_button.Enabled)
            {
                settings_error_provider.SetError(save_settings_button, "There're unsaved settings!");
            }
            else
            {
                settings_error_provider.Clear();
            }
        }

        private void SetThemeOption()
        {
            if (LightThemeCheck)
            {
                Properties.Settings.Default.light_checked = true;
                Properties.Settings.Default.dark_checked = false;
            }

            if (DarkThemeCheck)
            {
                Properties.Settings.Default.dark_checked = true;
                Properties.Settings.Default.light_checked = false;
            }
        }

        public void ThemeRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            sViewHandler.ThemeChanged();
        }

        private MainForm.Sections DetectSectionFromButton(string buttonName)
        {
            if (buttonName == "open_movie_path_button") return MainForm.Sections.Movie;
            if (buttonName == "open_serie_path_button") return MainForm.Sections.Serie;
            if (buttonName == "open_book_path_button") return MainForm.Sections.Book;
            return MainForm.Sections.Movie;
        }

        #endregion

    }
}
