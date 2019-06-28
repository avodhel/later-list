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
    public partial class later_list : Form
    {
        #region variables
        string whichSection = "movie";
        string data;
        string getInfo;
        string namePart;
        string authorPart;
        string genrePart;
        string savePath;
        string loadPath;
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
        ListBox listBox = new ListBox();
        settings settingsForm = new settings();
        #endregion

        #region start app
        public later_list()
        {
            FormManager.registerForm(this);
            InitializeComponent();
            chooseListbox();
            loadGenres();
            if(whichSection == "movie" && Properties.Settings.Default.movie_path != "")
            {
                loadList();
            }
            settingsForm.getSettings();
            themeControl();
        }
        #endregion

        #region exit from app
        private void later_list_FormClosing(object sender, FormClosingEventArgs e)
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

        private void later_list_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormManager.unRegisterForm(this);
        }
        #endregion

        #region choose section
        private void rbCheckedChanged(object sender, EventArgs e)
        {
            if (movie_rb.Checked && !series_rb.Checked && !books_rb.Checked)
            {
                whichSection = "movie";
                //panel scroll
                input_fields_panel.VerticalScroll.Value = 0;
                //labels
                movie_name_lbl.Visible = true;
                serie_name_lbl.Visible = false;
                book_name_lbl.Visible = false;
                author_lbl.Visible = false;
                genre_lbl.Location = new Point(37, 37);
                //checkbox
                genre_cb.Location = new Point(85, 35);
                //textbox
                author_tb.Visible = false;
                //listbox
                movie_listbox.Visible = true;
                serie_listbox.Visible = false;
                book_listbox.Visible = false;
                //load list
                chooseListbox();
                if (Properties.Settings.Default.movie_path != "")
                {
                    loadList();
                }
                //load genres
                loadGenres();
                //refresh input fields
                refreshInputFields();
                //enable buttons
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
                save_button.Enabled = false;
            }
            if (!movie_rb.Checked && series_rb.Checked && !books_rb.Checked)
            {
                whichSection = "serie";
                //panel scroll
                input_fields_panel.VerticalScroll.Value = 0;
                //labels
                movie_name_lbl.Visible = false;
                serie_name_lbl.Visible = true;
                book_name_lbl.Visible = false;
                author_lbl.Visible = false;
                genre_lbl.Location = new Point(37, 37);
                //checkbox
                genre_cb.Location = new Point(85, 35);
                //textbox
                author_tb.Visible = false;
                //listbox
                movie_listbox.Visible = false;
                serie_listbox.Visible = true;
                book_listbox.Visible = false;
                //load list
                chooseListbox();
                if (Properties.Settings.Default.serie_path != "")
                {
                    loadList();
                }
                //load genres
                loadGenres();
                //refresh input fields
                refreshInputFields();
                //enable buttons
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
                save_button.Enabled = false;
            }
            if (!movie_rb.Checked && !series_rb.Checked && books_rb.Checked)
            {
                whichSection = "book";
                //panel scroll
                input_fields_panel.VerticalScroll.Value = 0;
                //labels
                movie_name_lbl.Visible = false;
                serie_name_lbl.Visible = false;
                book_name_lbl.Visible = true;
                author_lbl.Visible = true;
                genre_lbl.Location = new Point(37, 69);
                //checkbox
                genre_cb.Location = new Point(85, 67);
                //textbox
                author_tb.Visible = true;
                //listbox
                movie_listbox.Visible = false;
                serie_listbox.Visible = false;
                book_listbox.Visible = true;
                //load list
                chooseListbox();
                if (Properties.Settings.Default.book_path != "")
                {
                    loadList();
                }
                //load genres
                loadGenres();
                //refresh input fields
                refreshInputFields();
                //enable buttons
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
                save_button.Enabled = false;
            }
        }
        #endregion

        #region input fields
        private void input_fields_Click(object sender, EventArgs e)
        {
            if (name_tb.Text == "" && genre_cb.Text == "")
            {
                add_button.Enabled = true;
                remove_button.Enabled = false;
                edit_button.Enabled = false;
            }
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            refreshInputFields();
            //enable buttons
            add_button.Enabled = true;
            remove_button.Enabled = false;
            edit_button.Enabled = false;
        }

        private void refreshInputFields()
        {
            name_tb.Text = "";
            genre_cb.Text = "";
            author_tb.Text = "";
        }

        private void loadGenres()
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
        private void add_button_Click(object sender, EventArgs e)
        {
            save_button.Enabled = true;

            if (name_tb.Text == "")
            {
                MessageBox.Show("Please add a " + whichSection + " name", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(whichSection != "book")
            {
                data = name_tb.Text + " (" + genre_cb.Text + ")";
                addToList(data);
                refreshInputFields();
            }
            else if(whichSection == "book")
            {
                data = name_tb.Text + " - " + author_tb.Text + " (" + genre_cb.Text + ")";
                addToList(data);
                refreshInputFields();
            }
        }

        private void addToList(string info)
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
        private void remove_button_Click(object sender, EventArgs e)
        {
            save_button.Enabled = true;

            listBox.Items.Remove(listBox.SelectedItem);
            //refresh input fields
            refreshInputFields();
        }
        #endregion

        #region edit
        private void selectedIndexChanged(object sender, EventArgs e)
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

        private void edit_button_Click(object sender, EventArgs e)
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
            refreshInputFields();
        }
        #endregion

        #region save
        private void save_button_Click(object sender, EventArgs e)
        {
            #region movie
            if (whichSection == "movie" && Properties.Settings.Default.movie_path == "")
            {
                savefiledialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                savefiledialog.RestoreDirectory = true;
                savefiledialog.FileName = "movielist";
                savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (savefiledialog.ShowDialog() == DialogResult.OK)
                {
                    settingsForm.MovieTextBoxText = savefiledialog.FileName;
                    settingsForm.saveSettings();
                    savePath = Properties.Settings.Default.movie_path;
                    loadPath = Properties.Settings.Default.movie_path;
                    saveList(true);
                }
                else
                {
                    settingsForm.getSettings();
                }
            }
            else if(whichSection == "movie" && Properties.Settings.Default.movie_path != "")
            {
                saveList(true);
            }
            #endregion

            #region serie
            if (whichSection == "serie" && Properties.Settings.Default.serie_path == "")
            {
                savefiledialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                savefiledialog.RestoreDirectory = true;
                savefiledialog.FileName = "serielist";
                savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (savefiledialog.ShowDialog() == DialogResult.OK)
                {
                    settingsForm.SerieTextBoxText = savefiledialog.FileName;
                    settingsForm.saveSettings();
                    savePath = Properties.Settings.Default.serie_path;
                    loadPath = Properties.Settings.Default.serie_path;
                    saveList(true);
                }
                else
                {
                    settingsForm.getSettings();
                }
            }
            else if (whichSection == "serie" && Properties.Settings.Default.serie_path != "")
            {
                saveList(true);
            }
            #endregion

            #region book
            if (whichSection == "book" && Properties.Settings.Default.book_path == "")
            {
                savefiledialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                savefiledialog.RestoreDirectory = true;
                savefiledialog.FileName = "booklist";
                savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (savefiledialog.ShowDialog() == DialogResult.OK)
                {
                    settingsForm.BookTextBoxText = savefiledialog.FileName;
                    settingsForm.saveSettings();
                    savePath = Properties.Settings.Default.book_path;
                    loadPath = Properties.Settings.Default.book_path;
                    saveList(true);
                }
                else
                {
                    settingsForm.getSettings();
                }
            }
            else if (whichSection == "book" && Properties.Settings.Default.book_path != "")
            {
                saveList(true);
            }
            #endregion
        }

        private void save_button_EnabledChanged(object sender, EventArgs e)
        {
            if (save_button.Enabled == true)
            {
                error_provider.SetError(save_button, "There're unsaved changes in " + whichSection + " list!");
            }
            if (save_button.Enabled == false)
            {
                error_provider.Clear();
            }
        }
        #endregion

        #region save and load system
        private void chooseListbox()
        {
            listBox.Items.Clear();
            switch (whichSection)
            {
                case "movie":
                    listBox = movie_listbox;
                    savePath = Properties.Settings.Default.movie_path;
                    loadPath = Properties.Settings.Default.movie_path;
                    break;
                case "serie":
                    listBox = serie_listbox;
                    savePath = Properties.Settings.Default.serie_path;
                    loadPath = Properties.Settings.Default.serie_path;
                    break;
                case "book":
                    listBox = book_listbox;
                    savePath = Properties.Settings.Default.book_path;
                    loadPath = Properties.Settings.Default.book_path;
                    break;
            }
        }
        
        private void saveList(bool show_message)
        {
            StreamWriter saveFile = new StreamWriter(savePath);
            foreach (var item in listBox.Items)
            {
                saveFile.WriteLine(item);
            }

            saveFile.Close();

            if (show_message)
            {
                MessageBox.Show(whichSection + " list saved!");
                show_message = false;
                save_button.Enabled = false;
            }
        }
        
        private void loadList()
        {
            try
            {
                using (StreamReader loadFile = new StreamReader(loadPath))
                {
                    string line;
                    while ((line = loadFile.ReadLine()) != null)
                    {
                        listBox.Items.Add(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("There isn't a " + which_section + " list.", "File Not Found",
                //                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region settings screen
        private void settings_button_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();
        }

        private void themeControl()
        {
            if (Properties.Settings.Default.theme == "Light")
            {
                FormManager.setAllBackcolors(SystemColors.InactiveBorder);
                settingsForm.LightThemeCheck = true;
                settingsForm.DarkThemeCheck = false;
            }
            if (Properties.Settings.Default.theme == "Dark")
            {
                FormManager.setAllBackcolors(SystemColors.InactiveCaptionText);
                settingsForm.LightThemeCheck = false;
                settingsForm.DarkThemeCheck = true;
            }
        }
        #endregion
    }
}
