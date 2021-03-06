﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public static class UiManager
    {
        private static List<SpreadSheet> spreadsheets = new List<SpreadSheet>();
        private static List<TreeView> navigators = new List<TreeView>();
        public static MainForm mainForm { get; set; }

        // creates new SpreadSheet instance
        public static void CreateSpreadSheet(DataModel dataModel)
        {
            SpreadSheet spreadSheet = new SpreadSheet(dataModel);
            spreadSheet.TopLevel = false;
            mainForm.panel1.Controls.Add(spreadSheet);
            spreadSheet.Show();
            spreadSheet.Dock = DockStyle.Right;

            spreadSheet.RefreshData();
            spreadsheets.Add(spreadSheet);
        }

        // refreshes given all spreadsheets
        public static void RefreshSpreadsheets()
        {
            foreach (var spreadsheet in spreadsheets) spreadsheet.RefreshData();
        }

        // refreshes given spreadsheet
        public static void RefreshSpreadsheets(SpreadSheet spreadsheet)
        {
            spreadsheet.RefreshData();
        }

        // closes and removes spreadsheet
        public static void CloseSpreadSheet(SpreadSheet spreadsheet)
        {
            spreadsheets.Remove(spreadsheet);
            spreadsheet.Close();
        }

        // creates new TreeView instance (Navigator)
        public static void CreateNavigator()
        {
            var navigator = new TreeView();
            navigator.TopLevel = false;
            mainForm.panel1.Controls.Add(navigator);
            navigator.Show();
            navigator.Dock = DockStyle.Left; // docks the form in to panel (temporary)
            navigator.RefreshData();

            navigators.Add(navigator);
        }

        // refreshes all navigators
        public static void RefreshNavigators()
        {
            foreach (var navigator in navigators) navigator.RefreshData();
        }

        // refreshes given navigator
        public static void RefreshNavigators(TreeView navigator)
        {
            navigator.RefreshData();
        }

        // closes and removes navigator
        public static void CloseNavigator(TreeView navigator)
        {
            navigators.Remove(navigator);
            navigator.Close();
        }

        public static void Create3DView()
        {
            var view = new View3D();
            view.TopLevel = true;
            view.Show();
        }
    }
}