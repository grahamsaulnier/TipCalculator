// Nothing complicated here that couldn't just as easily be done in C#, 
// I just wanted to play around with mixing and F# project with C#
namespace TipCalculator

#light

module public TipCalFS =
   let GetTip (bill:double) (percent:double) =
      bill * percent
   
   let GetTipBeforeTax (bill:double) (tax:double) (percent:double) =
      GetTip (bill / (1.0 + tax)) percent

   let GetTipPerPerson (tip:double) people = 
      tip / people
   
   let GetTipPercent (tip:double) (bill:double) = 
      tip / bill

   let GetTipPercentBeforeTax (tip:double) (tax:double) (bill:double) = 
      tip / (bill / (1.0 + tax))

   let GetBillPerPerson (bill:double) (tip:double) (people:double) = 
      (bill + tip) / people
   
