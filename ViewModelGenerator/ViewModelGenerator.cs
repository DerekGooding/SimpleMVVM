using Microsoft.CodeAnalysis;

namespace ViewModelGenerator;

[Generator]
public class ViewModelGenerator : IIncrementalGenerator
{
    //Get classes ViewModel attribute
    //Get methods in viewmodel with command attribute
    //Create new command class with name of method that calls method on execute. 
    //?? Figure out how to handle CanExecute cleanly ??
    //
    //Get properties in viewmodel with bind attribute
    //Create a new ObservableProperty with a get/set to the property name

    public void Initialize(IncrementalGeneratorInitializationContext context)
        => throw new System.NotImplementedException();
}
