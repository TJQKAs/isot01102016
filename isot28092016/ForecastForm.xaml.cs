using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace isot28092016
{
    /// <summary>
    /// Interaction logic for ForecastForm.xaml
    /// </summary>
    public partial class ForecastForm : Window
    {
        public double sum;
        private MainWindow mn;

        public ForecastForm(MainWindow mn)
        {
            this.mn = mn;

            InitializeComponent();
            string money = mn.finalsum.ToString();
            //MessageBox.Show(money);
            Double.TryParse(money, out sum);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void sld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ForecastViewModel frcvmdl = (ForecastViewModel)this.Resources["forecst"];
            int y;
            Int32.TryParse(tbl1.Text, out y);
            frcvmdl.Month = y.ToString();
            Int32.TryParse(frcvmdl.Month, out y);
            frcvmdl.ListofRes = getForms(12, y, sum);
        }

        private static ObservableCollection<TextBlock> getForms(int first_frame, int last_frame, double sum)
        {

            ObservableCollection<TextBlock> month_calculation = new ObservableCollection<TextBlock>();
            double next_sum = 0;
            while (first_frame < (last_frame))
            {
                double rate = 0.1 + 0.02 * Math.Pow((first_frame + 1), 2) + 0.5 * Math.Sin(2 * (first_frame + 1)) + Math.Cos(3 * (first_frame + 1));
                if (next_sum == 0)
                {
                    next_sum = sum * (1 + rate);
                }
                else
                {
                    next_sum = next_sum * (1 + rate);
                }

                string month_res = "Результат за " + (first_frame + 1) + " месяц: 1) остаток на счету " + Math.Round(next_sum, 2) + "$; 2) норма прибыли в " + Math.Round(rate * 100, 2) + "%; 3) чистый доход " + Math.Round(next_sum - (next_sum / (1 + rate)), 2) + "$;";
                TextBlock lbi = new TextBlock();
                lbi.Text = month_res;
                lbi.Background = Brushes.LightSkyBlue;
                if (first_frame == last_frame)
                {
                    month_calculation.Add(lbi);
                    TextBlock lbilast = new TextBlock();
                    lbilast.Text = "Итого сумма на конец года " + Math.Round(next_sum, 2) + "$; 2) абсолютный прирост в % " + Math.Round((((next_sum / sum) - 1) * 100), 2) + " или " + Math.Round((next_sum - sum), 2) + "$;";
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

    }

    public class ForecastViewModel : INotifyPropertyChanged
    {

        private string month;
        public string Month
        {
            get { return month; }
            set { month = value; OnPropertyChanged("Month"); }
        }

        private ObservableCollection<TextBlock> listofRes;

        public ObservableCollection<TextBlock> ListofRes
        {
            get { return listofRes; }
            set { listofRes = value; OnPropertyChanged("ListofRes"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

