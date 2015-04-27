﻿using Collection_Game_Tool.Main;
using Collection_Game_Tool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Collection_Game_Tool.PrizeLevels
{
    /// <summary>
    /// Interaction logic for UserControlPrizeLevel.xaml
    /// </summary>
    public partial class UserControlPrizeLevel : UserControl, Teller, IComparable
    {
        public PrizeLevel plObject;
        List<Listener> listenerList = new List<Listener>();

        public UserControlPrizeLevel()
        {
            InitializeComponent();
            plObject = new PrizeLevel();
            Level.DataContext = plObject;
            TextBoxValue.DataContext = plObject;
            CollectionBoxValue.DataContext = plObject;
            InstantWinCheckBox.DataContext = plObject;
            plObject.isInstantWin = false;
            plObject.numCollections = 1;
            plObject.prizeValue = 0;

            this.Loaded += new RoutedEventHandler(MainView_Loaded);
        }

        void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this.Parent);
            plObject.addListener((Window1)parentWindow);
        }

        public void Close_Prize_Level(object sender, RoutedEventArgs e)
        {
            shout(this);
        }

        private void boxChangedEventHandler(object sender, RoutedEventArgs args)
        {
            shout("Update");
            boxSelected();
        }

        public void shout(object pass)
        {
            foreach (UserControlPrizeLevels ucpls in listenerList)
            {
                ucpls.onListen(pass);
            }
        }

        private void boxSelected()
        {
            LevelGrid.Background = Brushes.Orange;
        }

        public void addListener(Listener list)
        {
            listenerList.Add(list);
        }

        private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void textBoxCollection_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CollectionBoxTextAllowed(e.Text);
        }

        private bool TextBoxTextAllowed(string p)
        {
            return Array.TrueForAll<Char>(p.ToCharArray(), delegate(Char c) { return Char.IsDigit(c) || Char.IsControl(c) || c.Equals('.'); });
        }

        private bool CollectionBoxTextAllowed(string p)
        {
            return Array.TrueForAll<Char>(p.ToCharArray(), delegate(Char c) { return Char.IsDigit(c) || Char.IsControl(c); });
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            UserControlPrizeLevel compare = obj as UserControlPrizeLevel;

            return compare.plObject.prizeValue.CompareTo(plObject.prizeValue);
        }

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            plObject.shout("validate");
        }
    }
}
