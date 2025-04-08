using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Pages;

public partial class ChapterPage : ContentPage
{
    private ChapterPageVM _vm;


    public ChapterPage(ChapterPageVM vm)
    {
        _vm = vm;
        this.BindingContext = vm;
        InitializeComponent();
    }
}