using System;
using System.Windows;

namespace Ink_Canvas_Better.Helpers;

public static class DataTemplateHelper
{
    public static void RegisterDataTemplate(Type viewModelType, Type viewType, ResourceDictionary? resourceDictionary = null)
    {
        var dataTemplate = new DataTemplate(viewModelType)
        {
            VisualTree = new FrameworkElementFactory(viewType),
        };
        dataTemplate.Seal();
        if (resourceDictionary != null)
        {
            resourceDictionary.Add(new DataTemplateKey(viewModelType), dataTemplate);
        }
        else
        {
            Application.Current.Resources.Add(new DataTemplateKey(viewModelType), dataTemplate);
        }
    }

    public static void RegisterDataTemplate<TViewModel, TView>(ResourceDictionary? resourceDictionary = null)
        where TViewModel : class
        where TView : class
    {
        RegisterDataTemplate(typeof(TViewModel), typeof(TView), resourceDictionary);
    }
}
