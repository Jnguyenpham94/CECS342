
open System

type AccountStatus = 
    | Empty of int
    | Balance of int
    | OverDrawn of int

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

//TODO: withdraw function overdrawn done EMPTY and Balance
let withdraw bankAccount (requestInt : int) =
    match bankAccount.account with
    | OverDrawn o -> (requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})
    | Empty e -> (requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})
    | Balance b -> (b - requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})

[<EntryPoint>]
let main argv =
    let m_burns = {name = "Montgomery Burns"; account = Balance 100000; creditLimit = Some 10000}
    let neal = {name = "Neal Terrell"; account = OverDrawn 900; creditLimit = None}
    let bob = {name = "Robert Dugalle"; account = Empty 0; creditLimit = Some 1000}
    let result1 = withdraw m_burns 100
    let result2 = withdraw neal 100
    let result3 = withdraw bob 100
    printfn "%O" result1
    printfn "%O" result2
    printfn "%O" result3
    0 // return an integer exit code