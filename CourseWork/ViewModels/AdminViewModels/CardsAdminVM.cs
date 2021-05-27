using CourseWork.Commands;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels.AdminViewModels
{
    public class CardsAdminVM : ViewModelBase
    {
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<Card> deletedCards { get; set; }
        public CardsAdminVM()
        {
            Cards = new ObservableCollection<Card>(App.db.Cards);
            deletedCards = new ObservableCollection<Card>();
        }
        private Card selectedCard;
        public Card SelectedCard
        {
            get { return selectedCard; }
            set
            {
                selectedCard = value;
                OnPropertyChanged("SelectedCard");
            }
        }
        private Command deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      if (SelectedCard != null)
                      {
                          Card card = new Card();
                          card = selectedCard;
                          Cards.Remove(card);
                          deletedCards.Add(card);
                      }
                  }));
            }
        }
        private Command saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new Command(obj =>
                  {
                      foreach (Card i in deletedCards)
                      {
                          App.db.Cards.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedCards.Clear();
                  }));
            }
        }
    }
}
