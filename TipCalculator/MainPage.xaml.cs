﻿using System;
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
      // Constructor
      public MainPage() {
         InitializeComponent();
         totalbillblock.Visibility = Visibility.Collapsed;
         totaltipblock.Visibility = Visibility.Collapsed;
         totalperpersonblock.Visibility = Visibility.Collapsed;
         tipperperson.Visibility = Visibility.Collapsed;

         var numericScope = new InputScope();
         var numericScopeName = new InputScopeName();
         numericScopeName.NameValue = InputScopeNameValue.Number;
         numericScope.Names.Add(numericScopeName);
         billbox.InputScope = numericScope;
         taxbox.InputScope = numericScope;
         tipbox.InputScope = numericScope;
         peoplebox.InputScope = numericScope;
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
      /// Gets the tip for the bill (calculates before tax)
      /// </summary>
      /// <param name="bill"></param>
      /// <param name="tax"></param>
      /// <param name="percent"></param>
      /// <returns></returns>
      private double GetTipBeforeTax(double bill, double tax, double percent) {
         return (GetTip(bill / (1 + tax), percent));
      }
      /// <summary>
      /// Gets the tip amount per person
      /// </summary>
      /// <param name="tip"></param>
      /// <param name="people"></param>
      /// <returns></returns>
      private double GetTipPerPerson(double tip, int people) {
         return tip / people;
      }
      /// <summary>
      /// Gets the tip amount as a % of the bill
      /// </summary>
      /// <param name="total"></param>
      /// <param name="tip"></param>
      /// <returns></returns>
      private double GetTipPercent(double total, double tip) {
         return tip / total;
      }
      /// <summary>
      /// This calculates the total amount each person will pay
      /// </summary>
      /// <param name="bill"></param>
      /// <param name="tip"></param>
      /// <param name="people"></param>
      /// <returns></returns>
      private double GetBillPerPerson(double bill, double tip, int people) {
         return ((bill + tip) / people);
      }
      /// <summary>
      /// This method is called when you press the calculate button
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void calculatebtn_Click(object sender, RoutedEventArgs e) {
         if ( billbox.Text == "" ) billbox.Text = "0";
         if ( taxbox.Text == "" ) taxbox.Text = "0";
         if ( tipbox.Text == "" ) tipbox.Text = "0";
         if ( peoplebox.Text == "" ) peoplebox.Text = "0";

         double bill = System.Convert.ToDouble(billbox.Text);
         double tax = System.Convert.ToDouble(taxbox.Text);
         double tippercent = System.Convert.ToDouble(tipbox.Text);
         int people = System.Convert.ToInt32(peoplebox.Text);

         double tip = 0;
         if ( tippercent > 0 ) {
            if ( tax == 0 ) {
               tip = GetTip(bill, tippercent / 100);
            } else {
               tip = GetTipBeforeTax(bill, tax / 100, tippercent / 100);
            }
         }
         totalbillblock.Text = BILL + (bill + tip).ToString("#.##");
         totaltipblock.Text = TIP + tip.ToString("#.##");
         totalbillblock.Visibility = Visibility.Visible;
         totaltipblock.Visibility = Visibility.Visible;
         if ( people > 1 ) {
            tipperperson.Text = TIPPP + (tip / people).ToString("#.##");
            totalperpersonblock.Text = BILLPP + (bill / people).ToString("#.##");
            totalperpersonblock.Visibility = Visibility.Visible;
            tipperperson.Visibility = Visibility.Visible;
         } else {
            totalperpersonblock.Visibility = Visibility.Collapsed;
            tipperperson.Visibility = Visibility.Collapsed;
         }
      }

   }
}