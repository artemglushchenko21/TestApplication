using DesktopUI.Models;
using DesktopUI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using PointModel = DesktopUI.Models.PointModel;

namespace DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PointModel canvasMaxPoint;
        private readonly double opacityOfDisabledButton = 0.2;
        private readonly double opacityOfEnabledButton = 1;
        private readonly List<AbstractFigure> figures = new();
        private readonly DispatcherTimer timer = new();

        public MainWindow()
        {
            InitializeComponent();

            DisableMoveButton(); ;
            DisableStopButton();
            SetCanvasMaxPoint();
            SetCanvasUpdateLoop();
        }

        private static List<string> GetAvailableLanguageDisplayNames()
        {
            Dictionary<string, string> result = GetAvailableLanguagesDictionary();
            return result.Values.Select(x => x.Substring(0, 1).ToUpper() + x[1..]).ToList();
        }

        private static Dictionary<string,string> GetAvailableLanguagesDictionary()
        {
            Dictionary<string, string> result = new();

            string languagesStr = Languages.AvailableLanguages;
            string[] languagesArr = languagesStr.Split(";");

            foreach (var item in languagesArr)
            {
                string languageAbbreviation = item.Split(" ")[0];
                string languageDisplayName = item.Split(" ")[1];

                result[languageAbbreviation] = languageDisplayName;
            }

            return result;
        }

        private void SetCanvasUpdateLoop()
        {
            timer.Tick += MoveAllFigures;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
        }

        private void MoveAllFigures(object sender, EventArgs e)
        {
            foreach (var figure in figures)
            {
                figure.Move(canvasMaxPoint);

                Canvas.SetLeft(figure.CanvasElement, figure.CurrentPosition.X);
                Canvas.SetTop(figure.CanvasElement, figure.CurrentPosition.Y);
            }
        }

        private void AddTriangle_Click(object sender, RoutedEventArgs e)
        {
            TriangleModel tringle = new();
            tringle.CurrentPosition = GetRandomPoint(tringle);

            ProcessAddFigure(tringle);
        }

        private void ProcessAddFigure(AbstractFigure figure)
        {
            AddToFigures(figure);
            AddToCanvas(figure);
            AddToBrowserTree(figure);
        }

        private void AddCircle_Click(object sender, RoutedEventArgs e)
        {
            CircleModel cirle = new();
            cirle.CurrentPosition = GetRandomPoint(cirle);

            ProcessAddFigure(cirle);
        }

        private void AddRectangle_Click(object sender, RoutedEventArgs e)
        {
            RectangleModel rectangle = new();
            rectangle.CurrentPosition = GetRandomPoint(rectangle);

            ProcessAddFigure(rectangle);
        }

        private void AddToFigures(AbstractFigure figure)
        {
            figures.Add(figure);
        }

        private void AddToCanvas(AbstractFigure figure)
        {
            UIElement uiElement = figure.CanvasElement;
            Canvas.SetLeft(uiElement, figure.CurrentPosition.X);
            Canvas.SetTop(uiElement, figure.CurrentPosition.Y);

            DrawingArea.Children.Add(uiElement);
        }

        private void AddToBrowserTree(AbstractFigure figure)
        {
            TreeViewItem node = new();

            string nodeHeader = GetNodeName(figure.DisplayName, BrowserTree.Items.Count + 1);
            node.Header = nodeHeader;
            node.Tag = figure;

            BrowserTree.Items.Add(node);
        }

        private PointModel GetRandomPoint(AbstractFigure figure)
        {
            double maxWidth = DrawingArea.ActualWidth;
            double maxHeight = DrawingArea.ActualHeight;

            double elementHeight = figure.MaxHeight;
            double elementWidth = figure.MaxWidth;

            PointModel point = PointModel.GetRandomPoint(maxWidth - elementWidth, maxHeight - elementHeight);
            return point;
        }

        private void SetCanvasMaxPoint()
        {
            canvasMaxPoint = new PointModel(DrawingArea.ActualWidth, DrawingArea.ActualHeight);
        }

        private void BrowserTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (BrowserTree.SelectedItem is not TreeViewItem treeViewItem)
            {
                return;
            }

            AbstractFigure selectedFigure = (AbstractFigure)treeViewItem.Tag;

            if (selectedFigure.IsStoped)
            {
                EnableMoveButton();
                DisableStopButton();
            }
            else
            {
                DisableMoveButton();
                EnableStopButton();
            }
        }

        private void EnableStopButton()
        {
            StopFigure.IsEnabled = true;
            StopFigure.Opacity = opacityOfEnabledButton;
        }

        private void EnableMoveButton()
        {
            MoveFigure.IsEnabled = true;
            MoveFigure.Opacity = opacityOfEnabledButton;
        }

        private void DisableStopButton()
        {
            StopFigure.IsEnabled = false;
            StopFigure.Opacity = opacityOfDisabledButton;
        }

        private void DisableMoveButton()
        {
            MoveFigure.IsEnabled = false;
            MoveFigure.Opacity = opacityOfDisabledButton;
        }

        private void StopFigure_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = BrowserTree.SelectedItem as TreeViewItem;
            AbstractFigure selectedFigure = (AbstractFigure)treeViewItem.Tag;
            selectedFigure.IsStoped = true;

            AddPausedPrefixToNodeHeader(treeViewItem);
            DisableStopButton();

            treeViewItem.IsSelected = false;
        }

        private static void AddPausedPrefixToNodeHeader(TreeViewItem treeViewItem)
        {
            string currentHeader = treeViewItem.Header.ToString();
            string updatedHeader = currentHeader.Insert(currentHeader.LastIndexOf(':'), GlobalStrings.PrefixStopped);
            treeViewItem.Header = updatedHeader;
        }

        private static void RemovePrefixFromHeader(TreeViewItem treeViewItem)
        {
            string currentHeader = treeViewItem.Header.ToString();
            string updatedHeader = currentHeader.Replace(GlobalStrings.PrefixStopped, "");
            treeViewItem.Header = updatedHeader;
        }

        private void MoveFigure_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = BrowserTree.SelectedItem as TreeViewItem;
            AbstractFigure selectedFigure = (AbstractFigure)treeViewItem.Tag;
            treeViewItem.IsSelected = false;

            RemovePrefixFromHeader(treeViewItem);
            DisableMoveButton();

            selectedFigure.IsStoped = false;
        }

        private void DrawingArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetCanvasMaxPoint();

            foreach (var figure in figures)
            {
                if (figure.CurrentPosition.X + figure.MaxWidth >= canvasMaxPoint.X)
                {
                    PointModel currentPosition = figure.CurrentPosition;
                    currentPosition.X = canvasMaxPoint.X - figure.MaxWidth;
                    figure.CurrentPosition= currentPosition;
                }

                if (figure.CurrentPosition.Y + figure.MaxHeight >= canvasMaxPoint.Y)
                {
                    PointModel currentPosition = figure.CurrentPosition;
                    currentPosition.Y = canvasMaxPoint.Y - figure.MaxHeight;
                    figure.CurrentPosition = currentPosition;
                }
            }
        }

        private void ComboboxLanguages_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> languages = GetAvailableLanguageDisplayNames();

            ComboboxLanguages.ItemsSource = languages;
            ComboboxLanguages.Text = languages[0];
        }

        private void ComboboxLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string currentLanguage = ComboboxLanguages.SelectedItem.ToString().ToLower();
            string currentCulture = GetCurrentCultureAbbreviation(currentLanguage);

            SetCurrentCulture(currentCulture);
            UpdateBrowserNames();
        }

        private static string GetCurrentCultureAbbreviation(string currentLanguage)
        {
            Dictionary<string, string> languageDistionary = GetAvailableLanguagesDictionary();
            string currentCulture = languageDistionary.FirstOrDefault(x => x.Value == currentLanguage).Key;
            return currentCulture;
        }

        private static void SetCurrentCulture(string currentCulture)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(currentCulture);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentCulture);
        }

        private void UpdateBrowserNames()
        {
            int nodesCounter = 1;

            foreach (TreeViewItem node in BrowserTree.Items)
            {
                AbstractFigure figure = (AbstractFigure)node.Tag;
                node.Tag = figure;

                string header = node.Header.ToString();

                bool pausedPrefixApplied = figure.IsStoped;

                string nodeHeader = GetNodeName(figure.DisplayName, nodesCounter++);
                node.Header = nodeHeader;

                if (pausedPrefixApplied)
                {
                    AddPausedPrefixToNodeHeader(node);
                }
            }
        }

        private static string GetNodeName(string figureDisplayName, int countNumber)
        {
            return $"{ figureDisplayName }:{ countNumber }";
        }
    }
}
