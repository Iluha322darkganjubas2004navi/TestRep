using Microsoft.Maui.Controls.Platform.Compatibility;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Calculator;

public partial class Calculator : ContentPage
{
    public Calculator()
    {
        InitializeComponent();

    }

    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";

    private void OnNumberClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;
        if (result.Text == "не число")
        {
            result.Text = "0";

        }
        if ((this.result.Text == "0")
            || (result.Text == "0")
            || currentState < 0)
        {
            this.result.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "," && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.result.Text += pressed;
    }
    void OnOperatorClicked(object sender, EventArgs e)
    {
        LockNumberValue(result.Text);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        mathOperator = pressed;
    }
    public static double Calculate(double value1, double value2, string mathOperator)
    {
        double result = 0;

        switch (mathOperator)
        {
            case "/":
                result = value1 / value2;
                break;
            case "*":
                result = value1 * value2;
                break;
            case "+":
                result = value1 + value2;
                break;
            case "-":
                result = value1 - value2;
                break;
        }

        return result;
    }
    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }
        }
    }
    
    void OnCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            if (secondNumber == 0)
                LockNumberValue(result.Text); 

            double resultnum = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

            this.result.Text = resultnum.ToString();
            firstNumber = resultnum;
            secondNumber = 0;
            currentState = 1;
        }
    }
    private void OnSwapClicked(object sender, EventArgs e)
    {
        double.TryParse(result.Text, out firstNumber);
        firstNumber *= -1;
        result.Text = firstNumber.ToString("#.##");
    }
    private void Backspace(object sender, EventArgs e)
    {
        if (result.Text.Length > 0)
            result.Text = result.Text.Substring(0, result.Text.Length - 1);
    }
    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.result.Text = "0";
    }
    void OnRadiusClicked(object sender, EventArgs e)
    {
        double number;
        double.TryParse(result.Text, out number);
        number = number * double.Pi * number;
        result.Text = number.ToString("#.##");
    }
    void OnCEClicked(object sender, EventArgs e)
    {
        result.Text = "";
    }
    void OnonexClicked(object sender, EventArgs e)
    {
        double number;
        double.TryParse(result.Text, out number);
        number = 1 / number;
        result.Text = number.ToString("#.##");
    }
    void OnxpowClicked(object sender, EventArgs e)
    {
        double number;
        double.TryParse(result.Text, out number);
        number = number*number;
        result.Text = number.ToString("#.##");
    }
    void OnsqrtClicked(object sender, EventArgs e)
    {
        double number;
        double.TryParse(result.Text, out number);
        number = Math.Sqrt(number);
        result.Text = number.ToString("#.##");
    }
}
