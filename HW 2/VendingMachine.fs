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
    float count * machine.price <= dollars && machine.inventory >= count

(*checks to see if the given dollars can purchase the requested count of items from the machine
returns tuple
*)
let purchase machine count dollars =
    let purchase = canPurchase machine count dollars
    let dup = {machine with inventory = machine.inventory - count}
    if purchase = true then
        printfn "Purchase Made"
        let result = (dollars - float count*machine.price, dup)
        result
    else
        printfn "Cannot Purchase"
        let result = (dollars, machine)
        result

[<EntryPoint>]
let main argv =
    //VendingMachine record instances
    let Snacks = {name = "chips"; inventory = 10; price = 1.00}
    let Snacks2 = {name = "soda"; inventory = 0; price = 1.50}//EMPTY inventory
    let Snacks3 = {name = "Fiji Water"; inventory = 4; price = 5.00}//expensive water lol
    let result = machineDescription Snacks3
    printfn "%O" result
    //how many of something person wants to buy test values
    let count = 10
    let count2 = 0
    let count3 = 4
    //how much many person has test values
    let wallet = 10.00
    let wallet2 = 0.00
    let wallet3 = 2.00
    let purchaseable = canPurchase Snacks count wallet
    printfn "Buy T/F: %b" purchaseable
    let buy = purchase Snacks count3 wallet
    printfn "Your change is %O" buy
    0 // return an integer exit code
