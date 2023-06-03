using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string display;
        private string lastNumber;
        private string currentNumber;
        private string arithmeticOperator;
        public MainWindow()
        {
            InitializeComponent();

            arithmeticOperator = "0";
            display = "0";
            currentNumber = "0";
            lastNumber = "0";

            // Clear button: C, AC
            acButton.Click      += AcButton_Click;

            // Number buttons: 0 - 9
            number0Button.Click += NumberButton_Click;
            number1Button.Click += NumberButton_Click;
            number2Button.Click += NumberButton_Click;
            number3Button.Click += NumberButton_Click;
            number4Button.Click += NumberButton_Click;
            number5Button.Click += NumberButton_Click;
            number6Button.Click += NumberButton_Click;
            number7Button.Click += NumberButton_Click;
            number8Button.Click += NumberButton_Click;
            number9Button.Click += NumberButton_Click;

            // Dot button: .
            dotButton.Click     += DotButton_Click;

            // Negative button: +/-
            negativeButton.Click += NegativeButton_Click;

            // Percentage button: %
            percentageButton.Click += PercentageButton_Click;

            // Arithmetic Operator: + - * /
            additionButton.Click        += ArithmeticOperator_Click;
            subtractionButton.Click     += ArithmeticOperator_Click;
            multiplicationButton.Click  += ArithmeticOperator_Click;
            divisionButton.Click        += ArithmeticOperator_Click;

            // Equal button: =
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if ("ERROR!" == currentNumber) return;
            if ("NO_INPUT" == currentNumber) currentNumber = lastNumber;

            double num1 = double.Parse(lastNumber);
            double num2 = double.Parse(currentNumber);
            double result = 0;

            switch (arithmeticOperator)
            {
                case "+" :
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "x":
                    result = num1 * num2;
                    break;
                case "/":
                    if (0 == num2) currentNumber = "ERROR!";
                    else result = num1 / num2;
                    break;
                default:
                    break;
            }

            if("ERROR!" != currentNumber) currentNumber = result.ToString();

            UpdateDisplay();
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if ("ERROR!" == currentNumber) return;

            double number = double.Parse(currentNumber);
            number /= 100;
            currentNumber = number.ToString();

            UpdateDisplay();
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if ("ERROR!" == currentNumber) return;

            if (currentNumber == "0")
            { 
                currentNumber = "-0";
            } 
            else
            {
                double number = double.Parse(currentNumber);
                number *= -1;
                currentNumber = number.ToString();
            }

            UpdateDisplay();
        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            if ("ERROR!" == currentNumber) return;

            if (currentNumber.Contains(".")) {}
            else currentNumber += ".";

            UpdateDisplay();
        }

        private void ArithmeticOperator_Click(object sender, RoutedEventArgs e)
        {
            arithmeticOperator = (sender as Button).Content.ToString();

            lastNumber = currentNumber;
            currentNumber = "0";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            string number = (sender as Button).Content.ToString();

            if ("0" == currentNumber || "ERROR!" == currentNumber) currentNumber = number;
            else currentNumber += number;          

            UpdateDisplay();
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = "0";

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            operationLabel.Content = lastNumber + " " + arithmeticOperator + " " + currentNumber;
            resultLabel.Content = currentNumber;
        }
    }
}
