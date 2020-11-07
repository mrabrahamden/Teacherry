﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MatchGameLib;
using TermLib;
using Color = System.Windows.Media.Color;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace GlossaryTermApp
{
    /// <summary>
    /// Логика взаимодействия для MatchGamePage.xaml
    /// </summary>
    public partial class MatchGamePage : Window
    {
        private bool _captured;
        private Point DragOffset;
        public MatchGame MatchGame;
        public MatchGamePage(MatchGame matchGame)
        {
            InitializeComponent();
            MatchGame = matchGame;
            PrepareForm();
        }

        private StackPanel WordsStackPanel;
        private List<TextBlock> listTextBlocksForCheck;
        private List<Border> listOfWordBorders;
        private void PrepareForm()
        { 
            WordsStackPanel = new StackPanel();
            listTextBlocksForCheck=new List<TextBlock>();
            listOfWordBorders=new List<Border>();
            WordsStackPanel.Background = (SolidColorBrush) new BrushConverter().ConvertFromString("#F2F3F4");
            ForStackPanelScrollViewer.Content = WordsStackPanel;
            foreach (var term in MatchGame.TermList)
            {
                StackPanel termStackPanel=new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                DockPanel dockPanel = new DockPanel()
                {
                    LastChildFill = true
                };
                WrapPanel wrapPanel = new WrapPanel();
                TextBlock descriptionTextBlock = new TextBlock()
                {
                    Text = term.Description,
                    FontSize = 20,
                    Tag = term,
                    TextAlignment = TextAlignment.Center,
                    Background = (SolidColorBrush) new BrushConverter().ConvertFromString("#F9F7A8"),
                    TextWrapping = TextWrapping.Wrap,
                    Padding = new Thickness(5, 2, 5, 2),
                    Margin = new Thickness(10, 10, 5, 0)
                };
                TextBlock placeForWordCanvas = new TextBlock()
                {
                    AllowDrop = true,
                    Tag = term,
                    Background = new SolidColorBrush(Color.FromRgb(202, 207, 210)),
                    Margin = new Thickness(30, 10, 10, 0),
                    Width = 200
                };
                listTextBlocksForCheck.Add(placeForWordCanvas);
                //placeForWordCanvas.DragEnter += PlaceForWordRectangle_DragEnter;
                placeForWordCanvas.Drop += PlaceForWordRectangle_Drop;

                wrapPanel.Children.Add(descriptionTextBlock);
                termStackPanel.Children.Add(placeForWordCanvas);
                dockPanel.Children.Add(termStackPanel);
                dockPanel.Children.Add(wrapPanel);
                WordsStackPanel.Children.Add(dockPanel);

                TextBlock wordTextBlock = new TextBlock()
                {
                    Text = term.Word,
                    FontSize = 18,
                    Tag = term,
                    Background = (SolidColorBrush) new BrushConverter().ConvertFromString("#F08080"),
                    Padding = new Thickness(5, 1, 5, 1)
                };

                Border forTextBlock = new Border()
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(202, 207, 210)),
                    Margin = new Thickness(10, 10, 0, 0),
                    Child = wordTextBlock,
                    Tag = wordTextBlock.Tag
                };

                forTextBlock.MouseDown += ForTextBlock_MouseDown;

                listOfWordBorders.Add(forTextBlock);
            }
            ShuffleList();
            foreach (var border in listOfWordBorders)
            {
                WordsWrapPanel.Children.Add(border);
            }
        }

        private void ShuffleList()
        {
            List<Border> newList=new List<Border>();
            Random random=new Random(DateTime.Now.Millisecond);
            while(listOfWordBorders.Count>0)
            {
                int count= random.Next() % listOfWordBorders.Count;
                newList.Add(listOfWordBorders[count]);
                listOfWordBorders.RemoveAt(count);
            }
            listOfWordBorders=new List<Border>(newList);
        }
        private void PlaceForWordRectangle_Drop(object sender, DragEventArgs e)
        {
            ((TextBlock)sender).Text = (string)e.Data.GetData(DataFormats.Text);
        }

        private void ForTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = (Border) sender;
            var textblock = (TextBlock) border.Child;
            DragDrop.DoDragDrop(border,textblock.Text, DragDropEffects.Copy);
        }

        //private void PlaceForWordRectangle_DragEnter(object sender, DragEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void CheckBTN_OnClick(object sender, RoutedEventArgs e)
        {
            var countTrue = 0;
            foreach (var textBlock in listTextBlocksForCheck)
            {
                var term = (SimpleTerm) textBlock.Tag;
                if ( term.Word == textBlock.Text)
                {
                    countTrue++;
                }
            }

            if (!MatchGame.TrainingMode)
            {
                FillGameResult resultWindow =
                    new FillGameResult(MatchGame.NumOfTerms - countTrue, MatchGame.NumOfTerms);
                resultWindow.ShowDialog();
                this.Close();
            }
        }
    }

}
