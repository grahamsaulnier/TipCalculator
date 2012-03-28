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
      // Constructor
      public MainPage () {
         InitializeComponent();
         totalbillblock.Visibility = Visibility.Collapsed;
         totaltipblock.Visibility = Visibility.Collapsed;
         totalperpersonblock.Visibility = Visibility.Collapsed;
         tipperperson.Visibility = Visibility.Collapsed;
      }
      private double GetTip ( double bill , double percent) {
         return bill * percent;
      }
      private double GetTipBeforeTax ( double bill, double tax, double percent ) {
         return (GetTip(bill / (1 + tax), percent));
      }
      private double GetTipPerPerson ( double tip, int people ) {
         return tip / people;
      }
      private double GetBillPerPerson ( double bill, double tip, int people ) {
         return ((bill + tip) / people);
      }
      private void calculatebtn_Click ( object sender, RoutedEventArgs e ) {
         double bill = System.Convert.ToDouble(billbox.Text);
         double tax = System.Convert.ToDouble(taxbox.Text);
         double tippercent =  System.Convert.ToDouble(tipbox.Text);
         int people = System.Convert.ToInt32(peoplebox.Text);
         
         double tip = 0;
         if ( tippercent > 0 ) {
            if ( tax == 0 ) {
               tip = GetTip(bill, tippercent);
            } else {
               tip = GetTipBeforeTax(bill, tax, tippercent);
            }
         }
         totalbillblock.Text = totalbillblock.Text + (bill + tip).ToString();
         totaltipblock.Text = totaltipblock.Text + tip.ToString();
         totalbillblock.Visibility = Visibility.Visible;
         totaltipblock.Visibility = Visibility.Visible;
         if ( people > 1 ) {
            tipperperson.Text = tipperperson.Text + (tip / people).ToString();
            totalperpersonblock.Text = totalperpersonblock.Text + (bill / people).ToString();
            totalperpersonblock.Visibility = Visibility.Visible;
            tipperperson.Visibility = Visibility.Visible;
         } else {
            totalperpersonblock.Visibility = Visibility.Collapsed;
            tipperperson.Visibility = Visibility.Collapsed;         
         }
      }

   }
}