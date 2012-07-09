using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TipCalculator {
   public partial class MainPage : PhoneApplicationPage {
      // Constants
      const String BILL = "Bill : $";
      const String TIP = "Tip : $";
      const String TIPPP = "Tip per person : $";
      const String BILLPP = "Bill per person : $";
      const double DEFAULTBILL = 40f;
      // Constructor
      bool initialized = false;
      public MainPage() {
         InitializeComponent();
         var numericScope = new InputScope();
         var numericScopeName = new InputScopeName();
         totalbillblock.Visibility = Visibility.Collapsed;
         totaltipblock.Visibility = Visibility.Collapsed;
         totalperpersonblock.Visibility = Visibility.Collapsed;
         tipperperson.Visibility = Visibility.Collapsed;
         //taxpercentlbl.Visibility = Visibility.Collapsed;
         //taxtextbox.Visibility = Visibility.Collapsed;
         taxtextbox.IsEnabled = false;
         numericScopeName.NameValue = InputScopeNameValue.Number;
         numericScope.Names.Add(numericScopeName);
         billbox.InputScope = numericScope;
         billbox.Text = DEFAULTBILL.ToString("#.##");
         taxbox.InputScope = numericScope;
         tipBox.InputScope = numericScope;
         peoplebox.InputScope = numericScope;
         taxtextbox.InputScope = numericScope;
         initialized = true;
         RecalculateEverything();
      }
      /// <summary>
      /// Pass percent as decimal
      /// </summary>
      /// <param name="bill"></param>
      /// <param name="percent"></param>
      /// <returns></returns>
      private double GetTip(double bill, double percent) {
         return bill * percent;
      }
      /// <summary>
      /// Pass percent and tax as decimal
      /// </summary>
      /// <param name="bill"></param>
      /// <param name="tax"></param>
      /// <param name="percent"></param>
      /// <returns></returns>
      private double GetTipBeforeTax(double bill, double tax, double percent) {
         return (GetTip(bill / (1 + tax), percent));
      }
      private double GetTipPerPerson(double tip, int people) {
         return tip / people;
      }
      private double GetBillPerPerson(double bill, double tip, int people) {
         return ((bill + tip) / people);
      }
      private void RecalculateEverything() {
         if(initialized) {
            if(billbox.Text == null || billbox.Text == "") billbox.Text = "0";
            if(taxbox.Text == null || taxbox.Text == "") taxbox.Text = "0";
            if(tipBox.Text == null || tipBox.Text == "") tipBox.Text = "0";
            if(peoplebox.Text == null || peoplebox.Text == "") peoplebox.Text = "0";

            double bill = System.Convert.ToDouble(billbox.Text);
            double tax = System.Convert.ToDouble(taxbox.Text);
            double tippercent = System.Convert.ToDouble(tip_slider.Value);
            int people = System.Convert.ToInt32(peoplebox.Text);

            double tip = 0;
            if(tippercent > 0) {
               if(tax == 0) {
                  tip = GetTip(bill, tippercent / 100);
               } else {
                  tip = GetTipBeforeTax(bill, tax / 100, tippercent / 100);
               }
            }
            totalbillblock.Text = BILL + (bill + tip).ToString("#.##");
            totaltipblock.Text = TIP + tip.ToString("#.##");
            totalbillblock.Visibility = Visibility.Visible;
            totaltipblock.Visibility = Visibility.Visible;
            if(people > 1) {
               tipperperson.Text = TIPPP + (tip / people).ToString("#.##");
               totalperpersonblock.Text = BILLPP + (bill / people).ToString("#.##");
               totalperpersonblock.Visibility = Visibility.Visible;
               tipperperson.Visibility = Visibility.Visible;
            } else {
               totalperpersonblock.Visibility = Visibility.Collapsed;
               tipperperson.Visibility = Visibility.Collapsed;
            }
            tipBox.Text = tip.ToString("#.##");
            taxtextbox.Text = tax.ToString();
            billbox.Text = bill.ToString("#.##");
         }
      }
      #region ButtonStuff
      
      private void calculatebtn_Click(object sender, RoutedEventArgs e) {
         //RecalculateEverything();
      }

      private void textBox1_TextChanged(object sender, TextChangedEventArgs e) {
         RecalculateEverything();
      }

      private void taxflag_Checked(object sender, RoutedEventArgs e) {
         taxtextbox.IsEnabled = true;
         RecalculateEverything();
         //taxpercentlbl.Visibility = Visibility.Visible;
         //taxtextbox.Visibility = Visibility.Visible;         
      }

      private void taxflag_Unchecked(object sender, RoutedEventArgs e) {
         taxtextbox.IsEnabled = false;
         RecalculateEverything();
         //taxpercentlbl.Visibility = Visibility.Collapsed;
         //taxtextbox.Visibility = Visibility.Collapsed; 
      }

      private void plus1_Click(object sender, GestureEventArgs e)
      {
         peoplebox.Text = (System.Convert.ToInt32(peoplebox.Text) + 1).ToString();
         RecalculateEverything();
      }

      private void minus1_Click(object sender, GestureEventArgs e)
      {
         int ppl = System.Convert.ToInt32(peoplebox.Text);
         peoplebox.Text = ppl > 0 ? (ppl - 1).ToString() : ppl.ToString();
         RecalculateEverything();
      }
      private void tip_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
         if(tipBox != null) tipBox.Text = tip_slider.Value.ToString("#.##");
         RecalculateEverything();
      }
      private void billbox_Tap(object sender, GestureEventArgs e) {
         billbox.SelectAll();
      }
      #endregion

      

      
   }

}