﻿using System;
using System.IO;
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
    public partial class MainForm : Form
    {
        #region variables
        string whichSection = "movie";
        string data;
        string getInfo;
        string namePart;
        string authorPart;
        string genrePart;
        string listPath;
        string[] movieGenres = {"Action",
                                 "Adventure",
                                 "Animation",
                                 "Biography",
                                 "Comedy",
                                 "Crime",
                                 "Documentary",
                                 "Drama",
                                 "Family",
                                 "Fantasy",
                                 "Film Noir",
                                 "History",
                                 "Horror",
                                 "Music",
                                 "Musical",
                                 "Mystery",
                                 "Romance",
                                 "Sci-Fi",
                                 "Short",
                                 "Sitcom",
                                 "Sport",
                                 "Superhero",
                                 "Thriller",
                                 "War",
                                 "Western"};
        string[] serieGenres = {"Action",
                                 "Adventure",
                                 "Animation",
                                 "Biography",
                                 "Comedy",
                                 "Crime",
                                 "Documentary",
                                 "Drama",
                                 "Family",
                                 "Fantasy",
                                 "Film Noir",
                                 "History",
                                 "Horror",
                                 "Music",
                                 "Musical",
                                 "Mystery",
                                 "Romance",
                                 "Sci-Fi",
                                 "Short",
                                 "Sitcom",
                                 "Sport",
                                 "Superhero",
                                 "Thriller",
                                 "War",
                                 "Western"};
        string[] bookGenres = {"Action and Adventure",
                                "Anthology",
                                "Biography/Autobiography",
                                "Classic",
                                "Comic and Graphic Novel",
                                "Crime and Detective",
                                "Drama",
                                "Essay",
                                "Fable",
                                "Fairy Tale",
                                "Fan-Fiction",
                                "Fantasy",
                                "Historical Fiction",
                                "Horror",
                                "Humor",
                                "Legend",
                                "Magical Realism",
                                "Memoir",
                                "Mystery",
                                "Mythology",
                                "Narrative Nonfiction",
                                "Periodicals",
                                "Realistic Fiction",
                                "Reference Books",
                                "Romance",
                                "Satire",
                                "Self-help Book",
                                "Sci-Fi",
                                "Short Story",
                                "Speech",
                                "Suspense/Thriller",
                                "Textbook",
                                "Poetry"};
        bool canAddListControl = true;
        ListBox listBox = new ListBox();
        SettingsForm settingsForm = new SettingsForm();
        #endregion

        #region start app
        public MainForm()
        {
            this.CenterToScreen();
            InitializeComponent();
            ChooseListbox();
            LoadGenres();
            if(whichSection == "movie" && Properties.Settings.Default.movie_path != "")
            {
                SaveLoadManager.LoadList(whichSection, settingsForm, listBox);
            }
            settingsForm.GetSettings();
            
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            PrepareSection(whichSection);

            ThemeManager.RegisterForm(this);
            ThemeManager.RegisterGroupBox(list_operations_gb);
            ThemeManager.RegisterTextBox(name_tb);
            ThemeManager.RegisterTextBox(author_tb);
            ThemeManager.RegisterCheckBox(genre_cb);
            ThemeManager.RegisterListBox(movie_listbox);
            ThemeManager.RegisterListBox(serie_listbox);
            ThemeManager.RegisterListBox(book_listbox);
            ThemeManager.RegisterButton(clear_button);
            ThemeManager.RegisterButton(add_button);
            ThemeManager.RegisterButton(remove_button);
            ThemeManager.RegisterButton(edit_button);
            ThemeManager.RegisterButton(save_button);
            ThemeManager.RegisterButton(discard_button);
            ThemeManager.ThemeControl(settingsForm);
        }
        #endregion

        #region exit from app
        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (save_button.Enabled == true)
            {
                DialogResult confirm = MessageBox.Show("Unsaved changes will be lost. Continue?", "Exit",
                                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (confirm == DialogResult.OK)
                {

                }
                else if (confirm == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MainFormClosed(object sender, FormClosedEventArgs e)
        {
            ThemeManager.UnRegisterForm(this);
        }
        #endregion

        #region choose section
        private void ChooseSection(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "movieSectionBtn":
                    PrepareSection("movie");
                    break;
                case "serieSectionBtn":
                    PrepareSection("serie");
                    break;
                case "bookSectionBtn":
                    PrepareSection("book");
                    break;
            }
        }

        private void PrepareSection(string section)
        {
            if (section == "movie")
            {
                whichSection = "movie";
                list_operations_gb.Text = "Movies";
                //section buttons
                movieSectionBtn.BackColor = SystemColors.ButtonFace;
                serieSectionBtn.BackColor = SystemColors.Highlight;
                bookSectionBtn.BackColor = SystemColors.Highlight;
                //panel scroll
                input_fields_panel.VerticalScroll.Value = 0;
                //labels
                movie_name_lbl.Visible = true;
                serie_name_lbl.Visible = false;
                book_name_lbl.Visible = false;
                author_lbl.Visible = false;
                genre_lbl.Location = new Point(43, 47);
                //checkbox
                genre_cb.Location = new Point(97, 41);
                //textbox
                author_tb.Visible = false;
                //listbox
                movie_listbox.Visible = true;
                serie_listbox.Visible = false;
                book_listbox.Visible = false;
                //load list
                ChooseListbox();
                if (Properties.Settings.Default.movie_path != "")
                {
                    SaveLoadManager.LoadList(whichSection, settingsForm, listBox);
                }
                //load genres
                LoadGenres();
                //refresh input fields
                RefreshInputFields();
                //enable buttons
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
                save_button.Enabled = false;
            }

            if (section == "serie")
            {
                whichSection = "serie";
                list_operations_gb.Text = "Series";
                //section buttons
                movieSectionBtn.BackColor = SystemColors.Highlight;
                serieSectionBtn.BackColor = SystemColors.ButtonFace;
                bookSectionBtn.BackColor = SystemColors.Highlight;
                //panel scroll
                input_fields_panel.VerticalScroll.Value = 0;
                //labels
                movie_name_lbl.Visible = false;
                serie_name_lbl.Visible = true;
                book_name_lbl.Visible = false;
                author_lbl.Visible = false;
                genre_lbl.Location = new Point(43, 47);
                //checkbox
                genre_cb.Location = new Point(97, 41);
                //textbox
                author_tb.Visible = false;
                //listbox
                movie_listbox.Visible = false;
                serie_listbox.Visible = true;
                book_listbox.Visible = false;
                //load list
                ChooseListbox();
                if (Properties.Settings.Default.serie_path != "")
                {
                    SaveLoadManager.LoadList(whichSection, settingsForm, listBox);
                }
                //load genres
                LoadGenres();
                //refresh input fields
                RefreshInputFields();
                //enable buttons
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
                save_button.Enabled = false;
            }

            if (section == "book")
            {
                whichSection = "book";
                list_operations_gb.Text = "Books";
                //section buttons
                movieSectionBtn.BackColor = SystemColors.Highlight;
                serieSectionBtn.BackColor = SystemColors.Highlight;
                bookSectionBtn.BackColor = SystemColors.ButtonFace;
                //panel scroll
                input_fields_panel.VerticalScroll.Value = 0;
                //labels
                movie_name_lbl.Visible = false;
                serie_name_lbl.Visible = false;
                book_name_lbl.Visible = true;
                author_lbl.Visible = true;
                genre_lbl.Location = new Point(43, 82);
                //checkbox
                genre_cb.Location = new Point(97, 76);
                //textbox
                author_tb.Visible = true;
                //listbox
                movie_listbox.Visible = false;
                serie_listbox.Visible = false;
                book_listbox.Visible = true;
                //load list
                ChooseListbox();
                if (Properties.Settings.Default.book_path != "")
                {
                    SaveLoadManager.LoadList(whichSection, settingsForm, listBox);
                }
                //load genres
                LoadGenres();
                //refresh input fields
                RefreshInputFields();
                //enable buttons
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
                save_button.Enabled = false;
            }
        }
        #endregion

        #region input fields
        private void InputFieldsClick(object sender, EventArgs e)
        {
            if (name_tb.Text == "" && genre_cb.Text == "")
            {
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
            }
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            movie_listbox.ClearSelected();
            serie_listbox.ClearSelected();
            book_listbox.ClearSelected();
            RefreshInputFields();
            //enable buttons
            add_button.Enabled = true;
            remove_button.Enabled = false;
            edit_button.Enabled = false;
        }

        private void RefreshInputFields()
        {
            name_tb.Text = "";
            genre_cb.Text = "";
            author_tb.Text = "";
        }

        private void LoadGenres()
        {
            genre_cb.Items.Clear();
            switch (whichSection)
            {
                case "movie":
                    genre_cb.Items.AddRange(movieGenres);
                    break;
                case "serie":
                    genre_cb.Items.AddRange(serieGenres);
                    break;
                case "book":
                    genre_cb.Items.AddRange(bookGenres);
                    break;
            }
        }
        #endregion

        #region add
        private void AddButtonClick(object sender, EventArgs e)
        {
            if(name_tb.Text != "")
            {
                save_button.Enabled = true;
                canAddListControl = true;
            }
            else if (name_tb.Text == "")
            {
                MessageBox.Show("Please add a " + whichSection + " name", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                save_button.Enabled = false;
                canAddListControl = false;
            }

            if (whichSection != "book" && canAddListControl)
            {
                data = name_tb.Text + " (" + genre_cb.Text + ")";
                AddToList(data);
                RefreshInputFields();
            }
            else if(whichSection == "book" && canAddListControl)
            {
                data = name_tb.Text + " - " + author_tb.Text + " (" + genre_cb.Text + ")";
                AddToList(data);
                RefreshInputFields();
            }
        }

        private void AddToList(string info)
        {
            switch (whichSection)
            {
                case "movie":
                    movie_listbox.Items.Add(info);
                    break;
                case "serie":
                    serie_listbox.Items.Add(info);
                    break;
                case "book":
                    book_listbox.Items.Add(info);
                    break;
            }
        }
        #endregion

        #region remove
        private void RemoveButtonClick(object sender, EventArgs e)
        {
            save_button.Enabled = true;

            listBox.Items.Remove(listBox.SelectedItem);
            //refresh input fields
            RefreshInputFields();
        }
        #endregion

        #region edit
        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            add_button.Enabled = false;
            remove_button.Enabled = true;
            edit_button.Enabled = true;

            try
            {
                getInfo = listBox.SelectedItem.ToString();        
                if (whichSection != "book")
                {
                    namePart = getInfo.Split('(')[0];
                }
                else if (whichSection == "book")
                {
                    namePart = getInfo.Split('-')[0];
                    authorPart = getInfo.Split('-')[1];
                    authorPart = authorPart.TrimStart(' ');
                }
                namePart = namePart.TrimEnd(' ');

                if (whichSection == "book")
                {
                    try
                    {
                        authorPart = authorPart.Split('(')[0];
                        authorPart = authorPart.TrimEnd(' ');
                    }
                    catch
                    {
                        authorPart = "";
                    }
                }

                try
                {
                    genrePart = getInfo.Split('(')[1];
                    genrePart = genrePart.TrimEnd(')');
                }
                catch
                {
                    genrePart = "";
                }

                name_tb.Text = namePart;
                if (whichSection == "book")
                {
                    author_tb.Text = authorPart;
                }
                genre_cb.Text = genrePart;
            }
            catch
            {

            }
        }

        private void EditButtonClick(object sender, EventArgs e)
        {
            save_button.Enabled = true;

            int index = listBox.SelectedIndex;
            listBox.Items.RemoveAt(index);
            if(whichSection != "book")
            {
                listBox.Items.Insert(index, name_tb.Text + " (" + genre_cb.Text + ")");
            }
            else if (whichSection == "book")
            {
               listBox.Items.Insert(index, name_tb.Text + " - " + author_tb.Text + " (" + genre_cb.Text + ")");
            }
            //refresh input fields
            RefreshInputFields();
        }
        #endregion

        #region save
        private void SetListPath()
        {
            switch (whichSection)
            {
                case "movie":
                    listPath = Properties.Settings.Default.movie_path;
                    break;
                case "serie":
                    listPath = Properties.Settings.Default.serie_path;
                    break;
                case "book":
                    listPath = Properties.Settings.Default.book_path;
                    break;
            }
        }

        private void SetFileName(string fileName)
        {
            switch (whichSection)
            {
                case "movie":
                    settingsForm.MovieTextBoxText = fileName;
                    break;
                case "serie":
                    settingsForm.SerieTextBoxText = fileName;
                    break;
                case "book":
                    settingsForm.BookTextBoxText = fileName;
                    break;
            }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            SetListPath();

            if ( listPath == "")
            {
                savefiledialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                savefiledialog.RestoreDirectory = true;
                savefiledialog.FileName = whichSection + "list";
                savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (savefiledialog.ShowDialog() == DialogResult.OK)
                {
                    SetFileName(savefiledialog.FileName);
                    settingsForm.SaveSettings();
                    SetListPath();
                    SaveLoadManager.savePath = listPath;
                    SaveLoadManager.loadPath = listPath;
                    SaveLoadManager.SaveList(true, save_button, whichSection, listBox);
                }
                else
                {
                    settingsForm.GetSettings();
                }
            }
            else if (listPath != "")
            {
                SaveLoadManager.SaveList(true, save_button, whichSection, listBox);
            }
        }

        private void SaveButtonEnabledChanged(object sender, EventArgs e)
        {
            if (save_button.Enabled == true)
            {
                switch (whichSection)
                {
                    case "movie":
                        serieSectionBtn.Enabled = false;
                        bookSectionBtn.Enabled = false;
                        break;
                    case "serie":
                        movieSectionBtn.Enabled = false;
                        bookSectionBtn.Enabled = false;
                        break;
                    case "book":
                        movieSectionBtn.Enabled = false;
                        serieSectionBtn.Enabled = false;
                        break;
                }
                error_provider.SetError(save_button, "There're unsaved changes in " + whichSection + " list!");
                discard_button.Visible = true;
            }
            if (save_button.Enabled == false)
            {
                movieSectionBtn.Enabled = true;
                serieSectionBtn.Enabled = true;
                bookSectionBtn.Enabled = true;
                error_provider.Clear();
                discard_button.Visible = false;
            }
        }
        #endregion

        #region discard
        private void DiscardButtonClick(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("All changes will discard, Continue ?", "Discard",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                switch (whichSection)
                {
                    case "movie":
                        movie_listbox.Items.Clear();
                        if (Properties.Settings.Default.movie_path != "")
                        {
                            SaveLoadManager.LoadList(whichSection, settingsForm, listBox);
                        }
                        break;
                    case "serie":
                        serie_listbox.Items.Clear();
                        if (Properties.Settings.Default.serie_path != "")
                        {
                            SaveLoadManager.LoadList(whichSection, settingsForm, listBox);
                        }
                        break;
                    case "book":
                        book_listbox.Items.Clear();
                        if (Properties.Settings.Default.book_path != "")
                        {
                            SaveLoadManager.LoadList(whichSection, settingsForm, listBox);
                        }
                        break;
                }
                save_button.Enabled = false;
            }
            else if (confirm == DialogResult.Cancel)
            {
               
            }
        }
        #endregion

        #region choose and load listbox
        private void ChooseListbox()
        {
            listBox.Items.Clear();
            switch (whichSection)
            {
                case "movie":
                    listBox = movie_listbox;
                    SaveLoadManager.savePath = Properties.Settings.Default.movie_path;
                    SaveLoadManager.loadPath = Properties.Settings.Default.movie_path;
                    break;
                case "serie":
                    listBox = serie_listbox;
                    SaveLoadManager.savePath = Properties.Settings.Default.serie_path;
                    SaveLoadManager.loadPath = Properties.Settings.Default.serie_path;
                    break;
                case "book":
                    listBox = book_listbox;
                    SaveLoadManager.savePath = Properties.Settings.Default.book_path;
                    SaveLoadManager.loadPath = Properties.Settings.Default.book_path;
                    break;
            }
        }
        #endregion

        #region settings screen
        private void SettingsButtonClick(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();
        }
        #endregion
    }
}