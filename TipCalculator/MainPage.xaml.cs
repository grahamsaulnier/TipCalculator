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
using Microsoft.FSharp.Core;
using Microsoft.Phone.Controls;


namespace TipCalculator {
   public partial class MainPage : PhoneApplicationPage {
      // Constants
      const String BILL = "Bill : $";
      const String TIP = "Tip : $";
      const String TIPPP = "Tip per person : $";
      const String BILLPP = "Bill per person : $";
      const String TIPSUFFIX = "";
      const double DEFAULTBILL = 40f;
      const bool REVIEW = false;
      // Variables
      bool initialized = false;
      List<List<String>> funphreases;
      List<String> tier1, tier2, tier3, tier4;

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
         taxtextbox.InputScope = numericScope;
         tipBox.InputScope = numericScope;
         peoplebox.InputScope = numericScope;
         taxtextbox.InputScope = numericScope;
         tippercentdsp.InputScope = numericScope;
         initialized = true;
         tipreviewdsp.Visibility = REVIEW ? Visibility.Visible : Visibility.Collapsed;
         funphreases = new List<List<String>>();
         PopulateTierLists();
         RecalculateEverything();
      }
      /// <summary>
      /// Fills in the tier lists with all the fun phrases we want to say! :D 
      /// </summary>
      private void PopulateTierLists() {
         //TODO add some more fun phrases!
         String[] tier1arr = { "Seriously?" };
         String[] tier2arr = { "Not so hot eh?" };
         String[] tier3arr = { "You are a good person" };
         String[] tier4arr = { "Marry me!" };
         
         tier1 = new List<String>(tier1arr);
         tier2 = new List<String>(tier2arr);
         tier3 = new List<String>(tier3arr);
         tier4 = new List<String>(tier4arr);
      }
      private void PickSomethingToSay(int tier) {
         //TODO implement
      }

      private void RecalculateEverything() {
         if(initialized) {
            if(billbox.Text == null || billbox.Text == "") billbox.Text = "0";
            if(taxtextbox.Text == null || taxtextbox.Text == "") taxtextbox.Text = "0";
            if(tipBox.Text == null || tipBox.Text == "") tipBox.Text = "0";
            if(peoplebox.Text == null || peoplebox.Text == "" || peoplebox.Text == "0")
               peoplebox.Text = "1";

            double bill = System.Convert.ToDouble(billbox.Text);
            double tax = System.Convert.ToDouble(taxtextbox.Text);
            double tippercent = System.Convert.ToDouble(tip_slider.Value);
            int people = System.Convert.ToInt32(peoplebox.Text);

            double tip = 0;
            if(tippercent > 0) {
               if(tax == 0) {
                  tip = TipCalFS.GetTip(bill, tippercent / 100);
               } else {
                  tip = TipCalFS.GetTipBeforeTax(bill, tax / 100, tippercent / 100);
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
      #region Simple calculations      
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
      #endregion

      #region GUI interaction
      private void GUIEventTextChangedCallRecalculateEverything(object sender, TextChangedEventArgs e) {
         RecalculateEverything();
      }
      private void GUIEventRoutedEventArgsCallRecalculateEverything(object sender, RoutedEventArgs e) {
         RecalculateEverything();
      }
      private void GUIEventRoutedPropertyChangedEventArgsCallRecalculateEverything(object sender, 
         RoutedPropertyChangedEventArgs<double> e){
            RecalculateEverything();
      }
     
      private void taxflag_Checked(object sender, RoutedEventArgs e) {
         taxtextbox.IsEnabled = true;
         RecalculateEverything();
      }
      private void taxflag_Unchecked(object sender, RoutedEventArgs e) {
         taxtextbox.IsEnabled = false;
         taxtextbox.Text = "0";
         RecalculateEverything();
      }
      private void plus1_Click(object sender, GestureEventArgs e)
      {
         peoplebox.Text = (System.Convert.ToInt32(peoplebox.Text) + 1).ToString();
         RecalculateEverything();
      }
      private void minus1_Click(object sender, GestureEventArgs e)
      {
         int ppl = System.Convert.ToInt32(peoplebox.Text);
         peoplebox.Text = ppl > 1 ? (ppl - 1).ToString() : ppl.ToString();
         RecalculateEverything();
      }
      private void tip_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
         if(tipBox != null) tipBox.Text = tip_slider.Value.ToString("#.##");
         if(tippercentdsp != null) tippercentdsp.Text = Math.Round(tip_slider.Value,2).ToString() + TIPSUFFIX;
         RecalculateEverything();
      }
      private void billbox_Tap(object sender, GestureEventArgs e) {
         billbox.SelectAll();
      }
      private void UpdateTipPercentBox() {
         //if(tippercentdsp.Text.Contains(TIPSUFFIX) == false) tippercentdsp.Text = tippercentdsp.Text + TIPSUFFIX;
         if(tip_slider != null) tip_slider.Value = System.Convert.ToDouble(tippercentdsp.Text.Substring(0, tippercentdsp.Text.Length - TIPSUFFIX.Length));
         if(tipBox != null) tipBox.Text = tip_slider.Value.ToString("#.##");
         RecalculateEverything();
      }
      private void tippercentdsp_TextChanged(object sender, TextChangedEventArgs e) {
         UpdateTipPercentBox();
      }
      private void tippercentdsp_LostFocus(object sender, RoutedEventArgs e) {
         UpdateTipPercentBox();
      }
      private void tippercentdsp_Tap(object sender, GestureEventArgs e) {
         tippercentdsp.SelectAll();
      }
      private void taxtextbox_Tap(object sender, GestureEventArgs e) {
         if(taxtextbox.IsEnabled) taxtextbox.SelectAll();
      }
     
      private void peoplebox_Tap(object sender, GestureEventArgs e) {
         peoplebox.SelectAll();
      }
      private void tipBox_Tap(object sender, GestureEventArgs e) {
         tipBox.SelectAll();
      }
     
      private void tipBox_TextChanged(object sender, TextChangedEventArgs e) {
         if(tip_slider != null) tip_slider.Value = System.Convert.ToDouble(tippercentdsp.Text.Substring(0, tippercentdsp.Text.Length - TIPSUFFIX.Length));
         if(tippercentdsp != null) tippercentdsp.Text = Math.Round(tip_slider.Value, 2).ToString() + TIPSUFFIX;
         RecalculateEverything();
      }
      #endregion
      
   }

}