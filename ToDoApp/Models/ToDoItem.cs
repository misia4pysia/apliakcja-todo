using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class ToDoItem
    {

        //dodwanie tabeli w bazie, tytul i zaznaczanie ze ma byc pusty 
        //i bool czy jest zrobione jesli tak to zrobione jesli nie to nie zrobione
        //klasa w php reprezentujaca wiersz z tabeli jak class ToDoItem{public $title = ""; public $isDone = false;}
        //to jest tylko kontener na dane bez zadnej logiki ani metod tylko same pola
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
