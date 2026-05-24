using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Models;

//opakowanie jednego zadania 
//w php po zmienieniu danych strona sie odswierza i pokazuje nowe dane
//w mvvm ekran sam sie nie odsiwierzy wiec tzreba go powiadomic
//do tego sluzy [ObservableProperty] zmeinna ktora krzyczy do ekranu ze sie zmienila i strona ma sie odswierzyc


namespace ToDoApp.ViewModels
{
    //ObservableObject — dziedziczy po tej klasie żeby mieć możliwość powiadamiania ekranu
    //dziala tak jak  extends w PHP
    public partial class ToDoItemViewModel : ObservableObject
    {
        private readonly ToDoItem model;
        //trzyma orginalne dane i jest tylko do doczytu 

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private bool isDone;



        //to sa eventy gdy cos sie stanie np uzytkownik kliknie usun to wywolujesz ten event i mainvewmodel reaguje on sam nic w sb nie robi tylko wywoluje cos co ma zrobic 
        public event Action<ToDoItemViewModel> OnCompleted;
        public event Action<ToDoItemViewModel> OnDeleted;
        public event Action<ToDoItemViewModel> OnEdit;
    



    
        //konstruktor dziala tak jak __construct w PHP
        //dostaje ToDoItem i kopiuje dane z niego

    public ToDoItemViewModel(ToDoItem toDoItem)
        {
            model = toDoItem;
            title = toDoItem.Title;   // kopiuje dane z modelu
            isDone = toDoItem.IsDone;
        }


        //ta metoda wywoluje sie automatycznie gdy Title się zmieni
        // dla tego ze zostalo uzyte  [ObservableProperty]
        // to ktualizuje oryginalny model
        partial void OnTitleChanged(string value)
        {
            model.Title = value; // synchronizuje z modelem
        }

        //[RelayCommand] — to jak przycisk który można podpiąć w XAML. Zamiast pisać osobną klasę dla każdego przycisku, dajesz ten atrybut i MAUI automatycznie tworzy
        //MarkAsDoneCommand, RemoveTaskCommand itd. — i właśnie te nazwy wpisujesz w XAML.
        //OnCompleted?.Invoke(this) = "jeśli ktoś nasłuchuje na ten event, wywołaj go i przekaż mnie (this) jako argument". Znak ?. = "tylko jeśli nie jest null" — jak isset() w PHP.

        [RelayCommand]
        public void MarkAsDone()
        {
            IsDone = true;
            OnCompleted?.Invoke(this); // wywołaj event, przekaż siebie
        }

        [RelayCommand]
        public void RemoveTask()
        {
            OnDeleted?.Invoke(this);
        }

        [RelayCommand]
        public void EditTask()
        {
            OnEdit?.Invoke(this);
        }
    }









}


