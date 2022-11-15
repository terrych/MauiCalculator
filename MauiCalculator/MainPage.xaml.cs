using MauiCalculator.Lib;
using Microsoft.Maui.Accessibility;

namespace MauiCalculator;

public partial class MainPage : ContentPage
{
    string toCompute = "";

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnButtonClick(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        toCompute += pressed;
        InputCalculation.Text = toCompute;

        SemanticScreenReader.Announce(InputCalculation.Text);
    }

    private void OnClear(object sender, EventArgs e)
    {
        toCompute = "";
        InputCalculation.Text = toCompute;

        SemanticScreenReader.Announce(InputCalculation.Text);
    }

    private void OnCalculate(object sender, EventArgs e)
    {
        var calc = new Calculator();
        calc.Append(toCompute);
        var result = calc.Calculate();

        InputCalculation.Text = toCompute;
        OutputCalculation.Text = result;

        SemanticScreenReader.Announce(InputCalculation.Text);
        SemanticScreenReader.Announce(OutputCalculation.Text);
    }

    private void OnBackspace(object sender, EventArgs e)
    {
        if(toCompute.Any()) 
            toCompute = toCompute.Remove(toCompute.Length - 1);
        InputCalculation.Text = toCompute;

        SemanticScreenReader.Announce(InputCalculation.Text);
    }
}

