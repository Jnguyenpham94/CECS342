//no prints in functions below and no input from user only from parameters
open System

type VendingMachine = {name: string; inventory: int; price: float}

(*takes VendingMachine
returns string
NO IF STATEMENTS ALLOWED
*)
let machineDescription machine =
    ()

(* returns true if person with dollars amount is able to purchase count items from given machine
NEEDS TO BE DONE IN 1 STATEMENT!!!
*)
let canPurchase machine count dollars =
    ()

(*checks to see if the given dollars can purchase the requested count of items from the machine
returns tuple
*)
let purchase machine count dollars =
    ()

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
