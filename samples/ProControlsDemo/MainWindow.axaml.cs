using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProControlsDemo.Models;
using ProControlsDemo.ViewModels;
using System;
using System.Collections.Generic;

namespace ProControlsDemo
{
    public class MainWindow : Window
    {
        private readonly TabControl _tabs;

        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
            Renderer.DrawFps = true;
            DataContext = new MainWindowViewModel();
            this.Find<Button>("my").Click += MainWindow_Click;
            _tabs = this.FindControl<TabControl>("tabs");
            this.Find<TreeDataGrid>("countries").CellPrepared += MainWindow_CellPrepared;
            this.Find<Button>("remove").Click += MainWindow_Click1;
            this.Find<Button>("add").Click += MainWindow_Click2;
            //DispatcherTimer.Run(() =>
            //{
            //    UpdateRealizedCount();
            //    return true;
            //}, TimeSpan.FromMilliseconds(500));

            Activated += MainWindow_Activated;
        }

        private void MainWindow_Click2(object? sender, RoutedEventArgs e)
        {
            this.Find<DockPanel>("ss").Children.Add(this.Find<TreeDataGrid>("countries"));
        }

        private void MainWindow_Click1(object? sender, RoutedEventArgs e)
        {
            this.Find<DockPanel>("ss").Children.Remove(this.Find<TreeDataGrid>("countries"));
        }

        private void MainWindow_CellPrepared(object? sender, TreeDataGridCellEventArgs e)
        {
            var tt = (sender as TreeDataGrid).Rows[e.RowIndex];
            var tt2 = (sender as TreeDataGrid).Columns[e.ColumnIndex];
        }

        private void MainWindow_Click(object? sender, RoutedEventArgs e)
        {
            (this.Find<TreeDataGrid>("countries").Source as FlatTreeDataGridSource<Country>).Items = new List<Country>();
        }

        private void MainWindow_Activated(object? sender, EventArgs e)
        {
            Program.Stopwatch!.Stop();
            System.Diagnostics.Debug.WriteLine("Startup time: " + Program.Stopwatch.Elapsed);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void AddCountryClick(object sender, RoutedEventArgs e)
        {
            var tb = this.Find<TextBox>("countryTextBox");
            if (tb.Text.Contains("-"))
            {
                var res = tb.Text.Split('-');
                for (int i = Convert.ToInt32(res[1]); i >= Convert.ToInt32(res[0]); i--)
                {
                    ((MainWindowViewModel)DataContext!).Countries._data.RemoveAt(i);
                }

            }
            else
            {
                ((MainWindowViewModel)DataContext!).Countries._data.RemoveAt(Convert.ToInt32(tb.Text));
            }

        }

        //private void UpdateRealizedCount()
        //{
        //    var tabItem = (TabItem)_tabs.SelectedItem!;
        //    var treeDataGrid = (TreeDataGrid)((Control)tabItem.Content).GetLogicalDescendants()
        //        .FirstOrDefault(x => x is TreeDataGrid tl);
        //    var textBlock = (TextBlock)((Control)tabItem.Content).GetLogicalDescendants()
        //        .FirstOrDefault(x => x is TextBlock tb && tb.Classes.Contains("realized-count"));
        //    var rows = treeDataGrid.RowsPresenter!;
        //    var realizedRowCount = rows.RealizedElements.Count;
        //    var unrealizedRowCount = ((ILogical)rows).LogicalChildren.Count - realizedRowCount;
        //    textBlock.Text = $"{realizedRowCount} rows realized ({unrealizedRowCount} unrealized)";
        //}
    }
}
