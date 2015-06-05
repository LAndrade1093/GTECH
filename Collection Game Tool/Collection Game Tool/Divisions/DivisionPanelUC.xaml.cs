using Collection_Game_Tool.GameSetup;
using Collection_Game_Tool.Main;
using Collection_Game_Tool.Services;
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

namespace Collection_Game_Tool.Divisions
{
    /// <summary>
    /// Interaction logic for DivisionPanelUC.xaml
    /// </summary>
    [Serializable]
    public partial class DivisionPanelUC : UserControl, Listener, Teller
    {
        List<Listener> listenerList = new List<Listener>();
        public DivisionsModel divisionsList;
        private int allottedPlayerPicks;
        private double marginAmount;
        public PrizeLevels.PrizeLevels prizes { get; set; }
        private const int MAX_DIVISIONS = 30;
        private string dpucID;

        public DivisionPanelUC()
        {
            InitializeComponent();
            allottedPlayerPicks = 1;
            divisionsList = new DivisionsModel();
            marginAmount = 10;
            divisionsScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.Loaded += new RoutedEventHandler(DivisionPanelUC_Loaded);
            divisionCounterLabel.Content = divisionsHolderPanel.Children.Count;
        }

        /// <summary>
        /// Called once, after the DivisionPanelUC is loaded
        /// Gets the parent window of the this user control and adds it to this class' list of listeners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivisionPanelUC_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this.Parent);
            addListener((Window1)parentWindow);
            addDivision();
        }

        /// <summary>
        /// Adds a new division to the GUI and the list of divisions,
        /// if the list hasn't exceeded 30 divisions
        ///     If it has, the add button is disabled and grayed out
        /// </summary>
        public void addDivision()
        {
            if (divisionsList.getSize() < MAX_DIVISIONS)
            {
                DivisionUC divUC = new DivisionUC(prizes, divisionsList.getSize() + 1);
                divUC.DivModel.DivisionNumber = divisionsList.getSize() + 1;
                divUC.updateDivision();
                divUC.Margin = new Thickness(marginAmount, marginAmount, 0, 0);
                divUC.SectionContainer = this;

                divisionsHolderPanel.Children.Add(divUC);
                divisionsList.addDivision(divUC.DivModel);
                this.addListener(divUC);
                validateDivision();
            }

            if (divisionsList.getSize() >= MAX_DIVISIONS)
            {
                addDivisionButton.IsEnabled = false;
                addDivisionButton.Opacity = 0.3;
            }

            divisionCounterLabel.Content = divisionsHolderPanel.Children.Count;
            isSectionEmpty();
        }

        /// <summary>
        /// Takes in an existing or loaded division and adds it to the list of available divisions
        /// </summary>
        /// <param name="div">The existing division, mostl likely loaded from a saved file</param>
        public void loadInDivision(DivisionModel div)
        {
            if (divisionsHolderPanel.Children.Count < MAX_DIVISIONS)
            {
                int divNumber = divisionsHolderPanel.Children.Count + 1;
                DivisionUC division = new DivisionUC(prizes, divNumber);
                division.DivModel = div;
                division.setDataContextToModel();
                division.DivModel.DivisionNumber = divNumber;
                division.DivModel.levelBoxes = div.levelBoxes;
                division.Margin = new Thickness(marginAmount, marginAmount, 0, 0);
                division.SectionContainer = this;

                division.setupLoadedDivision();
                division.updateDivision();

                divisionsHolderPanel.Children.Add(division);
                this.addListener(division);
                validateDivision();
            }

            if (divisionsHolderPanel.Children.Count >= MAX_DIVISIONS)
            {
                addDivisionButton.IsEnabled = false;
                addDivisionButton.Opacity = 0.3;
            }
            isSectionEmpty();
            divisionCounterLabel.Content = divisionsHolderPanel.Children.Count;
        }

        /// <summary>
        /// Removes a division from the list and the UI at the specified index
        /// </summary>
        /// <param name="index">the index location of the division to remove</param>
        public void removeDivision(int index)
        {
            for (int i = index; i < divisionsList.getSize(); i++)
            {
                DivisionUC div = (DivisionUC)divisionsHolderPanel.Children[i];
                div.DivModel.DivisionNumber = (int)div.DivModel.DivisionNumber - 1;
            }

            ErrorService.Instance.resolveWarning("005", new List<string> { ((DivisionUC)divisionsHolderPanel.Children[index]).DivModel.DivisionNumber.ToString() }, ((DivisionUC)divisionsHolderPanel.Children[index]).DivModel.errorID);
            listenerList.Remove((DivisionUC)divisionsHolderPanel.Children[index]);
            divisionsList.removeDivision(index);
            divisionsHolderPanel.Children.RemoveAt(index);

            if (divisionsList.getSize() < MAX_DIVISIONS)
            {
                addDivisionButton.IsEnabled = true;
                addDivisionButton.Opacity = 1.0;
            }

            isSectionEmpty();
            divisionCounterLabel.Content = divisionsHolderPanel.Children.Count;
        }

        /// <summary>
        /// A button cliked event for when the user wants to add a division
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            addDivision();
            divisionsScroll.ScrollToBottom();
        }

        /// <summary>
        /// Validates each division
        ///     -Determines if the division is empty (has no selected prize levels)
        ///     -Validates a division's uniqueness from other divisions
        ///     -Validates whether a division's total collections is less than the total picks set
        ///     -Validates whether or not a division can have an instant win prize level
        /// </summary>
        public void validateDivision()
        {
            checkDivisionsPicks();

            for (int index = 0; index < divisionsHolderPanel.Children.Count; index++)
            {
                DivisionModel divToCompare = ((DivisionUC)divisionsHolderPanel.Children[index]).DivModel;
                bool empty = true;
                for (int i = 0; i < DivisionModel.MAX_PRIZE_BOXES && empty; i++)
                {
                    if (divToCompare.levelBoxes[i].IsSelected)
                    {
                        empty = false;
                    }
                }

                if (!empty)
                {
                    ErrorService.Instance.resolveWarning("005", new List<string> { divToCompare.DivisionNumber.ToString() }, divToCompare.errorID);
                    bool valid = true;
                    for (int i = 0; i < divisionsHolderPanel.Children.Count && valid; i++)
                    {
                        DivisionModel currentDiv = ((DivisionUC)divisionsHolderPanel.Children[i]).DivModel;
                        if (divToCompare.DivisionNumber != currentDiv.DivisionNumber)
                        {
                            bool isUnique = false;
                            for (int prizeIndex = 0; prizeIndex < prizes.getNumPrizeLevels() && !isUnique; prizeIndex++)
                            {
                                if (divToCompare.levelBoxes[prizeIndex].IsSelected != currentDiv.levelBoxes[prizeIndex].IsSelected)
                                {
                                    isUnique = true;
                                }
                            }

                            if (!isUnique)
                            {
                                divToCompare.errorID = ErrorService.Instance.reportError("009", new List<string>{
                                divToCompare.DivisionNumber.ToString()
                            }, divToCompare.errorID);

                                valid = false;
                            }
                        }
                    }

                    if (valid)
                        ErrorService.Instance.resolveError("009", null, divToCompare.errorID);
                }
                else
                {
                    ErrorService.Instance.resolveError("009", null, divToCompare.errorID);
                    divToCompare.errorID = ErrorService.Instance.reportWarning("005", new List<string> { divToCompare.DivisionNumber.ToString() }, divToCompare.errorID);
                }

                int maxCollections = 0;
                for (int i = 0; i < divToCompare.getPrizeLevelsAtDivision().Count; i++)
                {
                    if (divToCompare.getPrizeLevel(i).isInstantWin)
                    {
                        maxCollections += 1;
                    }
                    else
                    {
                        maxCollections += divToCompare.getPrizeLevel(i).numCollections;
                    }
                }
                maxCollections -= GameSetupUC.pickCheck;
                for (int i = 0; i < prizes.getNumPrizeLevels(); i++)
                {
                    if(!divToCompare.getPrizeLevelsAtDivision().Contains(prizes.getPrizeLevel(i)))
                        maxCollections-=(prizes.getPrizeLevel(i).numCollections-1);
                }


                if (0 < maxCollections)
                {
                    divToCompare.errorID = ErrorService.Instance.reportError("011", new List<string> { divToCompare.DivisionNumber.ToString() }, divToCompare.errorID);
                }
                else
                    ErrorService.Instance.resolveError("011", null, divToCompare.errorID);

                //Check if a Division can have an Instant Win
                int minimumCollections = GameSetupUC.pickCheck;
                for (int i = 0; i < divToCompare.getPrizeLevelsAtDivision().Count; i++)
                {
                    if (divToCompare.getPrizeLevel(i).isInstantWin)
                    {
                        minimumCollections -= 1;
                    }
                    else
                    {
                        minimumCollections -= divToCompare.getPrizeLevel(i).numCollections;
                    }
                }
                for (int i = 0; i < prizes.getNumPrizeLevels(); i++)
                {
                    if (!divToCompare.getPrizeLevelsAtDivision().Contains(prizes.getPrizeLevel(i)))
                    {
                        minimumCollections -= prizes.getPrizeLevel(i).numCollections - 1;
                    }
                }
                if (minimumCollections > 0)
                {
                    divToCompare.errorID=ErrorService.Instance.reportWarning("007", new List<string> { divToCompare.DivisionNumber.ToString() }, divToCompare.errorID);
                }
                else
                {
                    ErrorService.Instance.resolveWarning("007", null, divToCompare.errorID);
                }
            }

            int allCollections = 0;
            for (int i = 0; i < prizes.getNumPrizeLevels(); i++)
            {
                allCollections += prizes.getPrizeLevel(i).numCollections - 1;
            }

            if (GameSetupUC.pickCheck > allCollections)
            {
                dpucID = ErrorService.Instance.reportError("012", new List<string> { }, dpucID);
            }
            else
                ErrorService.Instance.resolveError("012", null, dpucID);
        }

        /// <summary>
        /// Checks to see if there are no divisions in the list
        ///     If it there aren't any, this will send an error
        /// </summary>
        private void isSectionEmpty()
        {
            if (divisionsList.getSize() <= 0)
            {
                dpucID = ErrorService.Instance.reportWarning("006", new List<string>(), dpucID);
            }
            else
            {
                ErrorService.Instance.resolveWarning("006", null, dpucID);
            }
        }

        /// <summary>
        /// Validates that each division's total player picks is less than or equal to the set number of player picks
        /// </summary>
        private void checkDivisionsPicks()
        {
            for (int index = 0; index < divisionsHolderPanel.Children.Count; index++)
            {
                DivisionModel currentDivision = ((DivisionUC)divisionsHolderPanel.Children[index]).DivModel;
                if (currentDivision.TotalPlayerPicks <= GameSetupUC.pickCheck)
                {
                    ErrorService.Instance.resolveError("010", null, currentDivision.errorID);
                }
                else
                {
                    currentDivision.errorID = ErrorService.Instance.reportError("010", new List<string> { currentDivision.DivisionNumber.ToString() }, currentDivision.errorID);                    
                }
            }
        }

        /// <summary>
        /// Gets called when the class is registered to another class' shout
        /// </summary>
        /// <param name="pass"></param>
        public void onListen(object pass)
        {
            if (pass is PrizeLevels.PrizeLevels)
            {
                prizes = (PrizeLevels.PrizeLevels)pass;
            }
            else if (pass is int)
            {
                allottedPlayerPicks = (int)pass;
                checkDivisionsPicks();
            }
            shout(pass);
        }

        /// <summary>
        /// Calls of the listeners in the listener list
        /// </summary>
        /// <param name="pass"></param>
        public void shout(object pass)
        {
            foreach (Listener list in listenerList)
            {
                list.onListen(pass);
            }
        }

        /// <summary>
        /// Adds a listener to the list
        /// </summary>
        /// <param name="list"></param>
        public void addListener(Listener list)
        {
            listenerList.Add(list);
        }
    }
}
