using ToDoApp.ViewModels;

namespace ToDoApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {

            // w php przekazuje sie dane do widoku za pomoca
            //include 'view.php'; 
            // i dzieki temu w view.php masz dostęp do $data

            //w maui przekazuje sie dane do widoku czyli viewmodel za pomoca BindingContext = new MainViewModel();
            //dzieki czeu w xaml ma sie dostep do wszystkich wlasciowosci z viewmodelu
            InitializeComponent();
            BindingContext = new MainViewModel();
        }


        //binding w xaml wyglada w nastepujacy sposob
        //<Label Text="{Binding Title}" />

        //znaczy to ze bierze wlasciwosc Title z bindingcontext czyli z viewmodelu
        //i wyswietla je w labelu

        //w php wygladaloby to tak <?= $viewModel->title ?>

    }
}
