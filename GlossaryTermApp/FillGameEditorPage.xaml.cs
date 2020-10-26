﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SerializerLib;
using TermLib;
using Xceed.Wpf.Toolkit.Core.Converters;

namespace GlossaryTermApp
{
    public partial class FillGameEditorPage : Window
    {
        List<SimpleTerm> _termList = new List<SimpleTerm>();
        public FillGameEditorPage(List<SimpleTerm> list)
        {
            InitializeComponent();
            _termList = list;
            foreach (var term in list)
            {
                string wordAndDescription = term.Word + " -- ";
                TextBlock newWord = new TextBlock { Text = wordAndDescription, TextWrapping = TextWrapping.Wrap, FontSize = 20};
                WrapPanel panelForOneWord = new WrapPanel();
                CheckBox  isKey=new CheckBox();
                if (term.ReadyForFillGame)
                {
                    isKey.IsChecked = true;
                    VerticalContentAlignment = VerticalAlignment.Center;
                }
                panelForOneWord.Children.Add(isKey);
                panelForOneWord.Children.Add(newWord);
                foreach (var descriptionWord in term.DescriptionWordsAndSplittersList)
                {
                    var word = descriptionWord.Word;
                    if (word.Length > 0)
                    {
                        if (!descriptionWord.IsSplitter)
                        {
                            Button button = new Button()
                            {
                                Content = word, FontSize = 20, Tag = descriptionWord
                            };
                            if (descriptionWord.IsKeyWord)
                                button.Background = Brushes.LightGreen;
                            else
                                button.Background = Brushes.LightGray;
                            button.Click += ButtonOnClick;

                            panelForOneWord.Children.Add(button);
                        }
                        else
                        {
                            TextBlock split=new TextBlock()
                            {
                                Text = word, FontSize = 20
                            };
                            panelForOneWord.Children.Add(split);
                        }
                    }
                }
                Separator separate = new Separator();
                StackPanelForWords.Children.Add(panelForOneWord);
                StackPanelForWords.Children.Add(separate);
            }
        }
        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button) sender;
            var descriptionWord = (DescriptionWord) clickedButton.Tag;
            //var partOfDescription = clickedButton.Content.ToString();  //слово по которому кликнули
           // var panelForOneWord = (WrapPanel)clickedButton.Parent;
           // var newWord = panelForOneWord.Children.OfType<TextBlock>().First();
            //var wordAndDescr = newWord.Text;
           // Regex regexForWord = new Regex(@"(\w)+");
           // var termWord = regexForWord.Match(wordAndDescr).ToString();     //термин к которому относится слово по которому кликнули
           // var curTerm = _termList.Find(term => term.Word == termWord);
          //  var descriptionWord = curTerm.DescriptionWordsAndSplittersList.Find(w => w.Word == partOfDescription);
            if (clickedButton.Background == Brushes.LightGreen)
            {
                
                clickedButton.Background = Brushes.LightGray;
                descriptionWord.IsKeyWord = false;
            }
            else
            {
                clickedButton.Background = Brushes.LightGreen;
                descriptionWord.IsKeyWord = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BtnOk.FontFamily = new FontFamily("Segoe MDL2 Assets");
            BtnOk.Foreground = Brushes.MediumSeaGreen;
            BtnOk.FontWeight = FontWeights.Bold;
            BtnOk.Content = "\xE73E" + " ";
            BtnOk.IsEnabled = false;
            //System.Threading.Thread.Sleep(1000);
            //this.Close();
        }
    }
}