
open System

type AccountStatus = 
    | Empty of int
    | Balance of int
    | OverDrawn of int

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

//TODO: withdraw function overdrawn done EMPTY and Balance
let withdraw bankAccount requestInt =
    match bankAccount.account with
    | OverDrawn o -> (requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})
    | Empty e -> (requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})
    | Balance b -> (b - requestInt,{name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})

[<EntryPoint>]
let main argv =
    //let m_burns = {name = "Montgomery Burns"; account = Balance 100000; creditLimit = Some 10000}
    //let bob = {name = "Robert Dugalle"; account = Empty 0; creditLimit = Some 1000}
    let neal = {name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    let dave = {name = "Dave Davidson"; account = OverDrawn 200; creditLimit = None}
    let tom = {name = "Tom Thompson"; account = Balance 100; creditLimit = Some 500}
    //let result1 = withdraw m_burns 100
    let result2 = withdraw neal 50
    let result2_1 = withdraw neal 1000
    let result2_2 = withdraw neal 100
    //let result3 = withdraw bob 100
    let result4 = withdraw dave 300
    let result5 = withdraw tom 1000
    //printfn "%O" result1
    printfn "%O" result2
    printfn "%O" result2_1
    printfn "%O" result2_2
    //printfn "%O" result3
    printfn "%O" result4
    printfn "%O" result5
    0 // return an integer exit code