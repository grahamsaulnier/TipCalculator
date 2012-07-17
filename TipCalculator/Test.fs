// First implement a foldl function, with the signature (a->b->a) -> a -> [b] -> a
// Now use your foldl function to implement a map function, with the signature (a->b) -> [a] -> [b]
// Finally use your map function to convert an array of strings to upper case
//
// Test cases are in TestFoldMapUCase.cs
//
// Note: F# provides standard implementations of the fold and map operations, but the 
// exercise here is to build them up from primitive elements...

module TipCalculator.Zumbro
#light


let AlwaysTwo =
   2

let rec foldl fn seed vals = 
   match vals with
   | head :: tail -> foldl fn (fn seed head) tail
   | _ -> seed


let map fn vals =
   let gn lst x =
      fn( x ) :: lst
   List.rev (foldl gn [] vals)


let ucase vals =
   map String.uppercase vals