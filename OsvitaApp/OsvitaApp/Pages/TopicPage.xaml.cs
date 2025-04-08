using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Pages;

public partial class TopicPage : ContentPage
{
    private readonly TopicPageVM _vm;
    
    public TopicPage(TopicPageVM vm)
    {
        _vm = vm;
        this.BindingContext = vm;
        InitializeComponent();
    }
}