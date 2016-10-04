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

namespace isot28092016
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        public string moneyg;
        public double finalsum;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cleanAllitems(total);
            cleanAllitems(total1);
      
            double money;
            double.TryParse(tbxinvest.Text, out money);
            if (money < 5000)
            {
                MessageBox.Show("Сумма которую вы ввели менее 5000$ или ввод не числовых значений");
            }
            else
            {
                total2.Visibility = Visibility.Visible;
                List<decimal> result = getSumOfInvetment(money);
                List<decimal> ostatok = getOstatok(money);
                List<string> accountSum = new List<string>();
                int m = getSumOfInvetment(5000).Count;
                moneyg = tbxinvest.Text;

                for (int i = 0; i < m; i++)
                {
                    string year_result = "На конец " + (i + 1) + " года сумма на счету " + result[i] + "$ чистая прибыль за год " + ostatok[i] + "$";
                    accountSum.Add(year_result);
                }

                accountSum.ForEach(x =>
                {
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = x;
                    lbi.Background = Brushes.Coral;
                    total.Children.Add(lbi);
                });                                                                                       
                                                                                                          
                ListBoxItem lbi1 = new ListBoxItem();                                                     
                double efrate = Math.Round((((double)result[9] / money - 1) * 100), 2);                   
                double efres = (double)result[9] - money;
                string efpercentage = "Абсолютный прирост за время инвестиций в " + efrate + "% или " + efres +"$"; 
                lbi1.Content = efpercentage;
                lbi1.Background = Brushes.LawnGreen;
                total.Children.Add(lbi1);
                finalsum = (double)result[9];
                scroll1.Visibility = Visibility.Visible;
                scroll2.Visibility = Visibility.Visible;
                List<ListBoxItem> lbis = getListresult(0, 11, finalsum);

                lbis.ForEach(x => {
                    total1.Children.Add(x);
                });

            }

        }

        private static List<Decimal> getSumOfInvetment(double money)
        {

            List<Decimal> result = new List<decimal>();
            double profit_of_year = 0;

            for (int i = 0; i < 10; i++)
            {
                if (profit_of_year == 0)
                {
                    profit_of_year = Math.Round(money * 1.08, 2);
                }
                else
                {
                    profit_of_year = Math.Round(profit_of_year * 1.08, 2);
                }
                result.Add((Decimal)profit_of_year);
            }
            return result;
        }


        private static List<Decimal> getOstatok(double money)
        {
            List<Decimal> ostatok = new List<decimal>();
            double ostatok_of_year = 0;

            for (int i = 0; i < 10; i++)
            {
                if (ostatok_of_year == 0)
                {
                    ostatok_of_year = Math.Round(money * 0.08, 2);
                }
                else
                {
                    ostatok_of_year = Math.Round(ostatok_of_year * 1.08, 2);
                }
                ostatok.Add((Decimal)ostatok_of_year);
            }
            return ostatok;
        }

        private static List<ListBoxItem> getListresult(int first_frame, int last_frame, double sum)
        {

            List<ListBoxItem> month_calculation = new List<ListBoxItem>();
            double next_sum = 0;
            while (first_frame < (last_frame+1))
            {
                double rate = 0.1 + 0.02 * Math.Pow((first_frame + 1), 2) + 0.5 * Math.Sin(2 * (first_frame + 1)) + Math.Cos(3 * (first_frame + 1));
                if (next_sum == 0)
                {
                    next_sum = sum * (1 + rate);
                }else
                {
                    next_sum = next_sum * (1 + rate);
                }
         
                string month_res = "Результат за " + (first_frame+1) + " месяц: 1) остаток на счету " + Math.Round(next_sum,2) + "$; 2) норма прибыли в " + Math.Round(rate*100,2) + "%; 3) чистый доход " + Math.Round(next_sum-(next_sum/(1+rate)),2)+"$;";
                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = month_res;
                lbi.Background = Brushes.LightSkyBlue;
                if (first_frame == last_frame)
                {
                    month_calculation.Add(lbi);
                    ListBoxItem lbilast = new ListBoxItem();
                    lbilast.Content = "Итого сумма на конец года " + Math.Round(next_sum, 2) + "$; 2) абсолютный прирост в % " + Math.Round((((next_sum / sum) - 1) * 100), 2) + " или " +Math.Round((next_sum - sum),2)+ "$;";
                    lbilast.Background = Brushes.LightSteelBlue;
                    month_calculation.Add(lbilast);
                }
                else
                {
                    month_calculation.Add(lbi);
                }
                first_frame++;
            }
            return month_calculation;
        }

        private static void cleanAllitems(StackPanel x)
        {
            while (x.Children.Count != 0)
            {
                int i = 0;
                x.Children.RemoveAt(0);
                i++;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mn = this;
            ForecastForm frctForm = new ForecastForm(mn);
            frctForm.Show();
        }
    }
}
