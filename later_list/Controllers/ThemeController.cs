﻿using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using later_list.Data;

namespace later_list.Controllers
{
    public static class ThemeController
    {
        //FORM
        private static List<Form> formList = new List<Form>();

        public static void RegisterForm(Form form)
        {
            if (!formList.Contains(form)) formList.Add(form);
        }

        public static void UnRegisterForm(Form form)
        {
            if (formList.Contains(form)) formList.Remove(form);
        }

        //GROUP BOX
        private static List<GroupBox> groupBoxList = new List<GroupBox>();

        public static void RegisterGroupBox(GroupBox gb)
        {
            if (!groupBoxList.Contains(gb)) groupBoxList.Add(gb);
        }

        public static void UnRegisterGroupBox(GroupBox gb)
        {
            if (groupBoxList.Contains(gb)) groupBoxList.Remove(gb);
        }

        //TEXTBOX
        private static List<TextBox> textBoxList = new List<TextBox>();

        public static void RegisterTextBox(TextBox tb)
        {
            if (!textBoxList.Contains(tb)) textBoxList.Add(tb);
        }

        public static void UnRegisterTextBox(TextBox tb)
        {
            if (textBoxList.Contains(tb)) textBoxList.Remove(tb);
        }

        //COMBOBOX
        private static List<ComboBox> comboBoxList = new List<ComboBox>();

        public static void RegisterComboBox(ComboBox cb)
        {
            if (!comboBoxList.Contains(cb)) comboBoxList.Add(cb);
        }

        public static void UnRegisterComboBox(ComboBox cb)
        {
            if (comboBoxList.Contains(cb)) comboBoxList.Remove(cb);
        }

        //LISTVIEW
        private static List<ListView> listViewList = new List<ListView>();

        public static void RegisterListView(ListView lv)
        {
            if (!listViewList.Contains(lv)) listViewList.Add(lv);
        }

        public static void UnRegisterListView(ListView lb)
        {
            if (listViewList.Contains(lb)) listViewList.Remove(lb);
        }

        //BUTTON
        private static List<Button> buttonList = new List<Button>();

        public static void RegisterButton(Button btn)
        {
            if (!buttonList.Contains(btn)) buttonList.Add(btn);
        }

        public static void UnRegisterButton(Button btn)
        {
            if (buttonList.Contains(btn)) buttonList.Remove(btn);
        }

        //SET ALL THEME COLORS
        public static void SetAllThemeColors(Color BackColor, Color TextColor, Color ButtonTextColor)
        {
            foreach (Form     f  in formList    ) if (f  != null) f.BackColor  = BackColor;
            foreach (GroupBox gb in groupBoxList) if (gb != null) gb.ForeColor = TextColor;
            foreach (TextBox  tb in textBoxList ) if (tb != null) tb.ForeColor = TextColor;
            foreach (TextBox  tb in textBoxList ) if (tb != null) tb.BackColor = BackColor;
            foreach (ComboBox cb in comboBoxList) if (cb != null) cb.ForeColor = TextColor;
            foreach (ComboBox cb in comboBoxList) if (cb != null) cb.BackColor = BackColor;
            foreach (ListView lv in listViewList) if (lv != null) lv.ForeColor = TextColor;
            foreach (ListView lv in listViewList) if (lv != null) lv.BackColor = BackColor;
            foreach (Button   b  in buttonList  ) if (b  != null) b.ForeColor  = ButtonTextColor;
        }

        //CURRENT THEME
        public static void CurrrentTheme(SettingsForm settingsForm)
        {
            if (Properties.Settings.Default.light_checked == true)
            {
                SetAllThemeColors(Colors.LightThemeBackgroundColor, Color.Black, Colors.DarkThemeTextColor);
                settingsForm.LightThemeCheck = true;
                settingsForm.DarkThemeCheck = false;
            }
            if (Properties.Settings.Default.dark_checked == true)
            {
                SetAllThemeColors(Colors.DarkThemeBackgroundColor, Color.White, Colors.DarkThemeTextColor);
                settingsForm.LightThemeCheck = false;
                settingsForm.DarkThemeCheck = true;
            }
        }
    }
}
