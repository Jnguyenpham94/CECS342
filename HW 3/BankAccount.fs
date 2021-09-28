
open System

type AccountStatus = 
    | Empty of int
    | Balance of int
    | OverDrawn of int

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

//TODO: withdraw function
let withdraw bankAccount (requestInt : int) =
    match bankAccount.account with
    | OverDrawn o -> (requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})
    | Empty e -> (requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})
    | Balance b -> (requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})

[<EntryPoint>]
let main argv =
    let m_burns = {name = "Montgomery Burns"; account = Balance 100000; creditLimit = Some 10000}
    let neal = {name = "Neal Terrell"; account = OverDrawn 900; creditLimit = None}
    let bob = {name = "Robert Dugalle"; account = Empty 0; creditLimit = Some 1000}
    let result = withdraw neal 100
    printfn "%O" result
    0 // return an integer exit code