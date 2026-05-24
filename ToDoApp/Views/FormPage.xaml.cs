using Microsoft.Maui.Controls;

namespace ToDoApp.Views;


//[QueryProperty] jest to dokaldnie to samo co $_GET['title'] w php
//[QueryProperty] odbiera tytuł z URL gdy jest otwierany formularz do edycji
[QueryProperty(nameof(InitialTitle), "title")]

public partial class FormPage : ContentPage
{

    public FormPage()
    {
        InitializeComponent();
    }


    //InitialTitle to wlasciwosc ktora odbiera tytul z URL gdy jest otwierany formularz do edycji
    public string InitialTitle
    {
        set
        {

            //ustawia tytul w formularzu na ten z URL
            //TaskTitle to nazwa pola tekstowego zdefiniowana w xaml dziala tak jak id w html
            //gdy jest edytowant przycisk zmienia etykiete
            TaskTitle.Text = value;  // wpisz tekst do pola
            AddBtn.IsVisible = false; // ukryj przycisk dodaj
            EditBtn.IsVisible = true; // pokaz przycisk edytuj
        }
    }

    private async void OnCancel(object sender, EventArgs e)
    {
        //.. znaczy ze ma wrocic do poprzedniej strony
        //dziala tak jak history.back() w js

        await Shell.Current.GoToAsync("..");
    }

    private async void OnAdd(object sender, EventArgs e)
    {
        string title = TaskTitle.Text;

        //.. znaczy ze ma wrocic do poprzedniej strony
        //dziala tak jak  history.back() w js
        //?title={title} ma przekazac dane z powrotem
        // odbierze je mainviewmodel przez [QueryProperty].

        await Shell.Current.GoToAsync($"..?title={title}");
    }

    private async void OnEdit(object sender, EventArgs e)
    {
        string title = TaskTitle.Text;
        await Shell.Current.GoToAsync($"..?title={title}");
    }
}