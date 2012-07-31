// First implement a foldl function, with the signature (a->b->a) -> a -> [b] -> a
// Now use your foldl function to implement a map function, with the signature (a->b) -> [a] -> [b]
// Finally use your map function to convert an array of strings to upper case
//
// Test cases are in TestFoldMapUCase.cs
//
// Note: F# provides standard implementations of the fold and map operations, but the 
// exercise here is to build them up from primitive elements...
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
   
