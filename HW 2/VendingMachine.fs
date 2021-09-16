//no prints in functions below and no input from user only from parameters
open System

type VendingMachine = {name: string; inventory: int; price: float}

(*takes VendingMachine
returns string
NO IF STATEMENTS ALLOWED
*)
let machineDescription machine =
    match machine with
    | {inventory = 0} -> printfn "An empty %s machine" machine.name
    | _ -> printfn "A machine with %i %s available for $%.2f each" machine.inventory machine.name machine.price

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
    let Snacks = {name = "chips"; inventory = 10; price = 1.00}
    let Snacks2 = {name = "soda"; inventory = 0; price = 1.50}
    let result = machineDescription Snacks2
    printfn "%O" result
    0 // return an integer exit code
