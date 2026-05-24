using ToDoApp.Views;

namespace ToDoApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //dziala tak jak $router->get('/form', 'FormController@index'); w php
            //teraz mozna gotoasync("FormPage") zeby otworzyc formularz
            //rejestruje to nazwe trasy i przypisuje do niej klase strony

            Routing.RegisterRoute("FormPage", typeof(FormPage));

        }
    }
}
