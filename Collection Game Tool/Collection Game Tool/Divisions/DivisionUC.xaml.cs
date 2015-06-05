using System;
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
using Collection_Game_Tool.PrizeLevels;
using Collection_Game_Tool.Services;
using Collection_Game_Tool.GameSetup;

namespace Collection_Game_Tool.Divisions
{
    /// <summary>
    /// Interaction logic for DivisionUC.xaml
    /// </summary>
    public partial class DivisionUC : UserControl, Listener, IComparable
    {
        public DivisionModel DivModel { get; set; }
        public PrizeLevels.PrizeLevels Prizes { get; set; }
        public DivisionPanelUC SectionContainer { get; set; }

        public DivisionUC(PrizeLevels.PrizeLevels initialPrizeLevels, int number)
        {
            InitializeComponent();
            DivModel = new DivisionModel();
            setDataContextToModel();
            Prizes = initialPrizeLevels;
            DivModel.DivisionNumber = number;

            for (int i = 0; i < DivisionModel.MAX_PRIZE_BOXES; i++)
            {
                LevelBox levelBox = new LevelBox(i + 1);
                DivModel.levelBoxes.Add(levelBox);
                PrizeLevelBox box = new PrizeLevelBox(this, DivModel.levelBoxes[i]);
                if (i < initialPrizeLevels.getNumPrizeLevels()) box.levelModel.IsAvailable = true;
                prizeLevelsGrid.Children.Add(box);
            }
        }

        /// <summary>
        /// Sets the data context of the division's UI to the current division model
        /// </summary>
        public void setDataContextToModel()
        {
            totalPicksLabel.DataContext = DivModel;
            totalValueLabel.DataContext = DivModel;
            divisionNumberLabel.DataContext = DivModel;
        }

        /// <summary>
        /// If created from a save file, this will setup the division's prize level boxes
        /// </summary>
        public void setupLoadedDivision()
        {
            for (int i = 0; i < DivModel.levelBoxes.Count; i++)
            {
                ((PrizeLevelBox)prizeLevelsGrid.Children[i]).levelModel = DivModel.levelBoxes[i];
                ((PrizeLevelBox)prizeLevelsGrid.Children[i]).levelBox.DataContext = ((PrizeLevelBox)prizeLevelsGrid.Children[i]).levelModel;
                ((PrizeLevelBox)prizeLevelsGrid.Children[i]).prizeLevelLabel.DataContext = ((PrizeLevelBox)prizeLevelsGrid.Children[i]).levelModel;
            }
        }

        /// <summary>
        /// A button event that clears the division of selected prize levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            DivModel.clearPrizeLevelList();
            for (int i = 0; i < DivisionModel.MAX_PRIZE_BOXES; i++)
            {
                DivModel.levelBoxes[i].IsSelected = false;
            }

            DivModel.TotalPlayerPicks = DivModel.calculateTotalCollections();
            DivModel.TotalPrizeValue = DivModel.calculateDivisionValue();
            SectionContainer.validateDivision();
        }

        /// <summary>
        /// Clears errors and warnings related to the division
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorService.Instance.resolveWarning("005", null, DivModel.errorID);
            ErrorService.Instance.resolveWarning("007", null, DivModel.errorID);
            ErrorService.Instance.resolveError("009", null, DivModel.errorID);
            ErrorService.Instance.resolveError("010", null, DivModel.errorID);
            ErrorService.Instance.resolveError("011", null, DivModel.errorID);
            int index = getIndex();
            SectionContainer.removeDivision(index);
            SectionContainer.validateDivision();
        }

        /// <summary>
        /// Updates the total player picks and total value based on the selected prize levels
        /// </summary>
        public void updateInfo()
        {
            if (Prizes.getNumPrizeLevels() > 0)
            {
                DivModel.clearPrizeLevelList();
                for (int i = 0; i < Prizes.getNumPrizeLevels(); i++)
                {
                    if (DivModel.levelBoxes[i].IsSelected)
                    {
                        DivModel.addPrizeLevel(Prizes.getPrizeLevel(i));
                    }
                }

                DivModel.TotalPlayerPicks = DivModel.calculateTotalCollections();
                DivModel.TotalPrizeValue = DivModel.calculateDivisionValue();
            }

            SectionContainer.validateDivision();
        }

        /// <summary>
        /// Updates all of the info and prize level box data in the division
        /// </summary>
        public void updateDivision()
        {
            if (Prizes.getNumPrizeLevels() > 0)
            {
                for (int i = 0; i < DivisionModel.MAX_PRIZE_BOXES; i++)
                {
                    if (DivModel.levelBoxes[i].IsAvailable && DivModel.levelBoxes[i].IsSelected)
                    {
                        DivModel.addPrizeLevel(Prizes.getPrizeLevel(i));
                    }
                    else
                    {
                        DivModel.levelBoxes[i].IsSelected = false;
                    }
                }

                DivModel.TotalPlayerPicks = DivModel.calculateTotalCollections();
                DivModel.TotalPrizeValue = DivModel.calculateDivisionValue();
            }
        }

        /// <summary>
        /// Gets the index of this division within the UI
        /// </summary>
        /// <returns></returns>
        public int getIndex()
        {
            StackPanel divisionsPanel = (StackPanel)this.Parent;
            return divisionsPanel.Children.IndexOf(this);
        }

        /// <summary>
        /// Listens for "shouts" made by other classes, if it is subscribed to that class
        /// </summary>
        /// <param name="pass"></param>
        public void onListen(object pass)
        {
            if (pass is PrizeLevels.PrizeLevels)
            {
                Prizes = (PrizeLevels.PrizeLevels)pass;

                for (int i = 0; i < DivisionModel.MAX_PRIZE_BOXES; i++)
                {
                    DivModel.levelBoxes[i].IsAvailable = false;
                }

                for (int i = 0; i < Prizes.getNumPrizeLevels(); i++)
                {
                    DivModel.levelBoxes[i].IsAvailable = true;
                }

                DivModel.clearPrizeLevelList();
                updateDivision();
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            DivisionUC div = (DivisionUC)obj;

            return div.DivModel.CompareTo(this.DivModel);
        }
    }
}