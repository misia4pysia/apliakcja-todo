using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{

    //[QueryProperty] jest to dokaldnie to samo co $_GET['title'] w php
    // w php wygladaloby to tak $title = $_GET['title'];
    //w maui [QueryProperty(nameof(Title), "title")]
    // gdy URL to: "FormPage?title=Kupic mleko"
    // to automatycznie ustawia wlasciwosc Title = "Kupic mleko"
    [QueryProperty(nameof(Title), "title")]

    public partial class MainViewModel : ObservableObject
    {
        //ObservableCollection to lista ktora powiadamia ekran gdy dodasz/usuniesz element
        //zwyklu znacznik listy List<> tego nie robi wtedy ekran by sie nie odswierzyl
        //w php bylaby tablica $tasks = [] ktora po każdej zmianie automatycznie odswierzalaby strone 
        public ObservableCollection<ToDoItemViewModel> TasksToDo { get; set; } = new();
        public ObservableCollection<ToDoItemViewModel> TasksDone { get; set; } = new();



        //isempty sprawedzza czy lista jest pusta,
        //[NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        //oznacz ze gdy isempty sie zmieni to ma powiadomic ekran tez o isnotempty
        //dla tego ze isnotempty nie ma [ObservableProperty] wiec trzeba ja recznie obudzic

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        private bool isEmpty;

        public bool IsNotEmpty => !IsEmpty;


        //odbiera dane z formpage poprzez [QueryProperty]
        //i ustawia tytul gdy wraca sie z formularza 
        //posiadajac  ?title = cos w URLu
        [ObservableProperty]
        private string title = string.Empty;


        //przechowuje zadanie ktore aktualnie edytujemy
        private ToDoItemViewModel? taskEditing;


        //konstruktor ktory wywoluje sie przy starcie aplikacji
        public MainViewModel()
        {
            EmptyState();
        }


        

        //ta metoda wywoluje sie automatycznie
        //gdy title sue zmieni
        //czyli gdy wroce z formularza 
        //to jest cale serce calej apliakcji
        partial void OnTitleChanged(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (taskEditing != null)
                {

                    //edycja
                    // edytuje i aktualizuje ustniejace zadanie
                    taskEditing.Title = value;
                    taskEditing = null;
                }
                else
                {
                    //dodawanie
                    // dodaje i twoerzy nowe zadanie
                    ToDoItem surowy = new ToDoItem { Title = value, IsDone = false };
                    ToDoItemViewModel taskViewModel = new ToDoItemViewModel(surowy);

                    // podpiecie eventow


                    //taskViewModel.OnCompleted += OnTaskCompleted
                    //kaze nasluchiwac na event oncopleted
                    //i gdy wydarzt sie to on comleted to ma wywolac metode ontaskcomplete
                    //dziala tak jak addEventListener('click', handler) w js

                    taskViewModel.OnCompleted += OnTaskCompleted;
                    taskViewModel.OnDeleted += RemoveTask;
                    taskViewModel.OnEdit += EditTask;

                    TasksToDo.Add(taskViewModel);
                    EmptyState();
                    Title = string.Empty;
                }
                Title = string.Empty;
            }


        }

        //gotoasync jest nawigacja shell.current
        //dziala tak jak router
        //await dziala tak ze program ma zaczekac az to sie skonczy

        [RelayCommand]
        private async Task AddTask()
        {
            await Shell.Current.GoToAsync("FormPage");
        }


        //gdy zadanie zostanie oznaczone jako zrobione
        //ma usunac z listy do zrobienia i dodac do listy zrobione
        private void OnTaskCompleted(ToDoItemViewModel removeTask)
        {
            TasksToDo.Remove(removeTask);
            TasksDone.Add(removeTask);
            EmptyState();

        }

        //usuwa zadanie z obydwu list i sprawdza czy lista jest pusta
        private void RemoveTask(ToDoItemViewModel removeTask)
        {
            TasksDone.Remove(removeTask);
            TasksToDo.Remove(removeTask);
            EmptyState();
        }



        //zapisuje ktore zadanie edytuje za to odpowiada taskediting
        //nasyepnie otwiera formularz z akltualnym tytulem w URL
        //w php dziala to tak ze header("Location: form.php?title=Kupic mleko")
        private async void EditTask(ToDoItemViewModel editTask)
        {
            taskEditing = editTask;
            await Shell.Current.GoToAsync($"FormPage?title={editTask.Title}");
        }


        //spraedza czy obydwie listy sa puste i ustawia isempty
        private void EmptyState()
        {
            IsEmpty = (TasksToDo.Count == 0 && TasksDone.Count == 0);
        }


    }



}

//jak dziala po kolei

//uzytkownik klika dodaj zadanie
//mainviewmodel.addtask() otwiera formpage poprzez gotoasync("formpage")
//uzytkownik wpisuje tytul i klika dodaj
//formpage.onadd() gotoasync("..?title=Kupic mleko")
//mainviewmodel.title() ustawia title = "Kupic mleko" poprzez queryproperty
//ontitlechanged() wywoluje si eautomatycznie
//tworzy todoitem i todoitemviewmodel
//podpina eventy do todoitemviewmodel
//dodaje do listy todo taskstodo poprzez observablecollection
//ekran automatycznie si eodsiwerza i pokazuje nowe zadanie

//uzytkownik klika ze zadanie zrobione
//todoitemviewmodel.markasdone() ustawia isdone = true
//wywoluje event oncompleted?.invoke(this)
//mainviewmodel.ontaskcompleted() reaguje na event
//usuwa z listy todo i dodaje do listy done

//uzytkownik klika ze chce edytowac zadanie
//todoitemviewmodel.edittask() wywoluje event onedit?.invoke(this)
//mainviewmodel.edit() ustawia taskediting na aktualne zadanie i otwiera formpage z tytulem w URL
//uzytkownik zmienia tytul i klika dodaj
//formpage.onadd() gotoasync("..?title=Kupic chleb")
//formpage.onadd() gotoasync("..?title=Kupic chleb")
//mainviewmodel.title() ustawia title = "Kupic chleb" poprzez queryproperty
//ontitlechanged() wywoluje si eautomatycznie
//sprawdza ze taskediting nie jest null
//edytuje tytul aktualnego zadania i ustawia taskediting na null

//uzytkownik klika ze chce usunac zadanie
//todoitemviewmodel.ondelete() wywoluje event ondelete
//mainviewmodel.removetask() usuwa zadanie z obydwu list czyli z listy tasksdone i z listy taskstodo



