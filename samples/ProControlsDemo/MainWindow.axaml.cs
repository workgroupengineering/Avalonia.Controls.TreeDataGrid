using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;
using ProControlsDemo.Models;
using ProControlsDemo.ViewModels;

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
            this.Find<TreeDataGrid>("fileViewer").CellPrepared += MainWindow_CellPrepared;
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
            this.Find<DockPanel>("ss").Children.Add(this.Find<TreeDataGrid>("fileViewer"));
        }

        private void MainWindow_Click1(object? sender, RoutedEventArgs e)
        {
            this.Find<DockPanel>("ss").Children.Remove(this.Find<TreeDataGrid>("fileViewer"));
        }

        private void MainWindow_CellPrepared(object? sender, TreeDataGridCellEventArgs e)
        {
            var tt = (sender as TreeDataGrid).Rows[e.RowIndex];
            var tt2 = (sender as TreeDataGrid).Columns[e.ColumnIndex];
        }

        private void MainWindow_Click(object? sender, RoutedEventArgs e)
        {
            (this.Find<TreeDataGrid>("fileViewer").Source as HierarchicalTreeDataGridSource<FileTreeNodeModel>).Items = new List<FileTreeNodeModel>();
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
            var countryTextBox = this.FindControl<TextBox>("countryTextBox");
            var regionTextBox = this.FindControl<TextBox>("regionTextBox");
            var populationTextBox = this.FindControl<TextBox>("populationTextBox");
            var areaTextBox = this.FindControl<TextBox>("areaTextBox");
            var gdpTextBox = this.FindControl<TextBox>("gdpTextBox");

            var country = new Country(
                countryTextBox.Text,
                regionTextBox.Text,
                int.TryParse(populationTextBox.Text, out var population) ? population : 0,
                int.TryParse(areaTextBox.Text, out var area) ? area : 0,
                0,
                0,
                null,
                null,
                int.TryParse(gdpTextBox.Text, out var gdp) ? gdp : 0,
                null,
                null,
                null,
                null);
            ((MainWindowViewModel)DataContext!).Countries.AddCountry(country);
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
