﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;
using NHotkey.Wpf;

namespace Journal_IO_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class entry
    {
        public DateTime Date { get; set; }
        public string UnformattedDate { get; set; }
        public string Entry { get; set; }
        public entry()
        {

        }
        public string OutputAsString()
        {
            this.UnformattedDate = this.Date.ToString("dd/MM/yyyy");
            string output = this.UnformattedDate;
            output += ":";
            output += this.Entry;
            return output;
        }
    }

    public partial class MainWindow : Window
    {
        static string filename = "Untitled";
        public List<entry> entries = new List<entry>();
        public EntryManager entryManager = new EntryManager(); //only one window of this possible and cannot open again in same session but so what? it allows the carry over of changes in the entry manager
        public bool unsaved { get; set; }
        public bool unaddedEntry { get; set; }

        public MainWindow() //Prepares for initial use
        {
            InitializeComponent();
            this.Title = "Journal I/O - " + filename;
            this.entries = new List<entry>();
            this.unsaved = false;
            this.unaddedEntry = false;
            HotkeyManager.Current.AddOrReplace("Save", Key.S, ModifierKeys.Control, HotkeySave);
            HotkeyManager.Current.AddOrReplace("SaveAs", Key.S, ModifierKeys.Control | ModifierKeys.Shift, HotkeySaveAs);
            HotkeyManager.Current.AddOrReplace("Open", Key.O, ModifierKeys.Control, HotkeyOpen);
            HotkeyManager.Current.AddOrReplace("New", Key.N, ModifierKeys.Control, HotkeyNew);
            HotkeyManager.Current.AddOrReplace("Search", Key.F, ModifierKeys.Control, HotkeySearch);
            HotkeyManager.Current.AddOrReplace("OpenManager", Key.M, ModifierKeys.Control , HotkeyManagerOpen);
            HotkeyManager.Current.AddOrReplace("CloseManager", Key.M, ModifierKeys.Control | ModifierKeys.Shift, HotkeyManagerClose);
        }

        System.Globalization.CultureInfo ukCulture = new System.Globalization.CultureInfo("en-GB");

        private void Window_1_Loaded(object sender, RoutedEventArgs e) //Sets date in date entry box to be the current date when loaded
        {
            this.dateEntry.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
            Open();
        }

        private void AddNewEntry() //formats info and turns it into entry object, then adds to list and adds *??Does this add a reference to the object will this work
        {
            entry NewEntry = new entry();
            NewEntry.Entry = NewEntryBox.Text;
            NewEntry.UnformattedDate = dateEntry.Text;
            NewEntry.Date = DateTime.Parse(NewEntry.UnformattedDate, ukCulture.DateTimeFormat);
            bool contained = false;
            string entryContainedDate = "";
            foreach (entry entry in entries)
            {
                if (entry.Date == NewEntry.Date)
                {
                    contained = true;
                    entryContainedDate = entry.UnformattedDate;
                }
            }
            if (contained)
            {
                Window1 window1 = new Window1("Do you want to overwrite this entry");
                if (window1.ShowDialog() == true)
                {
                    if (window1.DialogResult == true)
                    {
                        foreach (entry entry in entries.ToList<entry>())
                        {
                            if (entry.Date == NewEntry.Date)
                            {
                                entries.Remove(entry);
                            }
                        }
                        entries.Add(NewEntry);
                    }
                }
            }
            else
            {
                entries.Add(NewEntry);
                this.unaddedEntry = false;
                this.unsaved = true;
                Retitle();
            }

        }

        public string FormatForOutput() //Turns list into string for output
        {
            //todo sort list
            entries = entries.OrderBy(d => d.Date).ToList();
            string output = "";
            foreach (entry entry in entries)
            {
                output += entry.UnformattedDate;
                output += " : ";
                output += entry.Entry;
                output += "\n";
            }
            return output;
        }

        private void AddEntry_Click(object sender, RoutedEventArgs e) //processes a click on Add button
        {
            AddNewEntry();
            Output.Text = FormatForOutput();
        }

        public List<entry> FormatFromInput(string input)// takes string (from a file) and turns it into a List
        {
            List<entry> EntriesOutput = new List<entry>();
            string pat = "(\\d*/\\d*/\\d*) : (.*)";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(input);
            while (m.Success)
            {
                string Date = "";
                string entrystring = "";
                for (int i = 1; i <= 2; i++)
                {
                    Group g = m.Groups[i];
                    if (i == 1)
                    {
                        Date = g.Value;
                    }
                    else if (i == 2)
                    {
                        entrystring = g.Value;
                    }
                }
                entry NewEntry = new entry();
                NewEntry.Entry = entrystring;
                NewEntry.UnformattedDate = Date;
                NewEntry.Date = DateTime.Parse(NewEntry.UnformattedDate, ukCulture.DateTimeFormat);
                EntriesOutput.Add(NewEntry);

                m = m.NextMatch();
            }
            return EntriesOutput;

        }

        public void SaveAs()//Saves file As
        {
            string fileText = FormatForOutput();

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Journal I/O File (*.jour)|*.jour"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, fileText);
                filename = dialog.FileName;
            }
            this.unsaved = false;
            Retitle();
        }

        public void SaveOver()//Saves over existing file based on file name
        {
            if (filename != "Untitled")
            {
                File.WriteAllText(filename, FormatForOutput());
            }
            else
            {
                SaveAs();
            }
            this.unsaved = false;
            Retitle();
        }

        public void Open()//Opens file as does necessary related actions
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Journal I/O File (*.jour)|*.jour";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                string Text = File.ReadAllText(openFileDialog.FileName);
                filename = openFileDialog.FileName.ToString();
                entries = FormatFromInput(Text);
                Output.Text = FormatForOutput();
                this.Title = "Journal I/O " + filename;
            }
        }

        public void New()//Creates procedure for new file
        {
            NewEntryBox.Text = "";
            Output.Text = "";
        }

        public void Retitle() //updates the title
        {
            if (this.unsaved && this.unaddedEntry)
            {
                this.Title = "Journal I/O - " + filename + "*§";
            }
            else if (unsaved)
            {
                this.Title = "Journal I/O - " + filename + "*";
            }
            else if (unaddedEntry)
            {
                this.Title = "Journal I/O - " + filename + "§";
            }
            else
            {
                this.Title = "Journal I/O - " + filename;
            }
        }

        private void Window_1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Title.Substring(this.Title.Length - 1) == "*")
            {
                Window1 window1 = new Window1("Do you want to save before closing?");
                if (window1.ShowDialog() == true)
                {
                    if (window1.DialogResult == true)
                    {
                        SaveOver();
                    }
                }
            }
        } //confirms save before closing

        private void NewEntryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.unaddedEntry = true;
            Retitle();
        } //ensures knowledge of change to test for requirement to save before closing

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveOver();
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            New();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWIndow searchWIndow = new SearchWIndow();
            searchWIndow.Show();
            searchWIndow.entries = entries;
        }

        private void HotkeySave(object sender, NHotkey.HotkeyEventArgs e)
        {
            SaveOver();
        }

        private void HotkeySaveAs(object sender, NHotkey.HotkeyEventArgs e)
        {
            SaveAs();
        }

        private void HotkeyOpen(object sender, NHotkey.HotkeyEventArgs e)
        {
            Open();
        }

        private void HotkeyNew(object sender, NHotkey.HotkeyEventArgs e)
        {
            New();
        }

        private void HotkeySearch(object sender, NHotkey.HotkeyEventArgs e)
        {
            SearchWIndow searchWIndow = new SearchWIndow();
            searchWIndow.Show();
            searchWIndow.entries = entries;
        }

        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            //public EntryManager entryManager = new EntryManager();
            entryManager.entries = entries;
            entryManager.Show();
        }

        private void ManagerCloseButton_Click(object sender, RoutedEventArgs e)
        {

            entries = entryManager.entries;
            Output.Text = FormatForOutput();
            this.unsaved = true;
            Retitle();
            entryManager.closedFromMain = true;
            entryManager.Close();
        }

        private void HotkeyManagerOpen(object sender, NHotkey.HotkeyEventArgs e)
        {
            //public EntryManager entryManager = new EntryManager();
            entryManager.entries = entries;
            entryManager.Show();
        }

        private void HotkeyManagerClose(object sender, NHotkey.HotkeyEventArgs e)
        {

            entries = entryManager.entries;
            Output.Text = FormatForOutput();
            this.unsaved = true;
            Retitle();
            entryManager.closedFromMain = true;
            entryManager.Close();
        }
    }
}
